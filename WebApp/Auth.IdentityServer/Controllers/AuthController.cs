using IdentityServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CMS.Helper;
using CMS.Helper.NewFolder;
using CMS.Helper.SharedServices;
using CMS.Helper.UtilsClass;
using System.Net.WebSockets;
using System.Runtime.Intrinsics.Arm;
using IdentityServer.Models;

namespace IdentityServer.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private EmailSenderService _emailSenderService;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, EmailSenderService emailSenderService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSenderService = emailSenderService;
        }

        [HttpGet]
        //from query params
        public async Task<IActionResult> Login(string returnUrl = "https://localhost:7134/Auth/Register") //URL of identity server controller register, change this if necessary
        {
            var externalProviders = await _signInManager.GetExternalAuthenticationSchemesAsync();
            TempData["returnUrl"] = returnUrl;
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalProviders = externalProviders
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            //check if model is valid
            var result = await _signInManager.PasswordSignInAsync(vm.Username, vm.Password, true, true);
            if (result.Succeeded)
            {
                return Redirect(vm.ReturnUrl);
            }
            else if (result.IsNotAllowed) //not confirm email
            {
                IdentityUser user = await _userManager.FindByNameAsync(vm.Username);
                if (user != null)
                {
                    string userEmail = user.Email;
                    return RedirectToAction(nameof(NotifyConfirmEmail), new { email = userEmail, returnUrl = vm.ReturnUrl });
                }
            }
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View(new RegisterViewModel
            {
                ReturnUrl = TempData["returnUrl"].ToString()
            });
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmEmail(NotifyConfirmEmailViewModel nvm)
        {
            //get form data using traditional aproach
            string inputActivationToken = Request.Form["inputActivationToken"].ToString();
            IdentityUser user = await _userManager.FindByEmailAsync(nvm.Email);
            if (user != null) { 
                if (inputActivationToken.Equals(nvm.AccountActivationToken))
                {
                    var result = await _userManager.ConfirmEmailAsync(user, nvm.AccountActivationToken);
                    if (result.Succeeded)
                    {
                        await _userManager.AddClaimAsync(user, new Claim(MyClaimType.Role, RoleType.AuthenticatedUser));
                        await _signInManager.SignInAsync(user, false);
                        return Redirect(nvm.ReturnUrl);
                    }
                }
                else { //retype input until it match
                    return View("VerifyActivationTokenMatch", nvm);
                }
            }
            return RedirectToAction(nameof(Register));
        }

        [HttpGet]
        public async Task<IActionResult> NotifyConfirmEmail(string email, string returnUrl)
        {
            return View("NotifyConfirmEmail", new NotifyConfirmEmailViewModel
            {
                Email = email,
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> NotifyConfirmEmail(NotifyConfirmEmailViewModel nvm)
        {
            string email = nvm.Email; string returnUrl = nvm.ReturnUrl;
            IdentityUser user = await _userManager.FindByEmailAsync(email);

            var accountActivationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            MailRequest mailContent = new MailRequest();
            mailContent.Subject = $"Confirmation email for {user.UserName}";
            string mailBody = $"Here 's your activation code: <b>{accountActivationToken}</b>";
            mailContent.ToEmail = user.Email;
            mailContent.Body = mailBody;
            bool activationEmailSendingTaskSuccess = await _emailSenderService.SendMail(mailContent);
            if (activationEmailSendingTaskSuccess == true)
            {
                nvm.AccountActivationToken = accountActivationToken;
                return View("VerifyActivationTokenMatch",nvm);
                //send email successfully
            }
            else
            {
                return View(nvm);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel rvm)
        {
            if (rvm.Password == rvm.PasswordConfirmed)
            {
                //phai cos await neu ko se redirect trc khi tao user
                var user = new IdentityUser {
                    UserName = rvm.Username,
                    Email = rvm.Email,
                };

                var result = await _userManager.CreateAsync(user, rvm.Password);
                if (result.Succeeded) {
                    return RedirectToAction(nameof(NotifyConfirmEmail), new { email = rvm.Email, returnUrl = rvm.ReturnUrl });
                }

            }
            return View();
        }
        public async Task<IActionResult> ExternalLogin(string provider, string returnUrl)
        {
            var redirectUri = Url.Action(nameof(ExternalLoginCallback), "Auth", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUri);
            return Challenge(properties, provider);
        }

        public async Task<IActionResult> ExternalLoginCallback(string returnUrl)
        {
            //check if we authen with facebook successfully
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                //if not go to login page again
                return RedirectToAction("Login");
            }

            //else sign in get claims and set to ctx.User and may save those claims to db?
            var result = await _signInManager
                .ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false); //ì this succeed will set identity.External cookie

            //if that facebook user is already exist in our db redirect
            if (result.Succeeded)
            {
                return Redirect(returnUrl);
            }

            var ctx = HttpContext;
            //else create that facebook user in db using ExternalRegister
            var username = info.Principal.FindFirst(ClaimTypes.Name).Value; //get info.principle.Name represent the face book username
            //cannot modify the claim so we extract the value then modify the value
            return View("ExternalRegister", new ExternalRegisterViewModel
            {
                Username = username.Replace(" ", ""),
                ReturnUrl = returnUrl
            });
        }

        public async Task<IActionResult> ExternalRegister(ExternalRegisterViewModel vm)
        {
            //check again?
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction("Login");
            }

            var user = new IdentityUser(vm.Username);
            var claimToAdd = new Claim(MyClaimType.Country, "VN");
            var claimToAddToAccessToken = new Claim(MyClaimType.Role, RoleType.Admin);
            var result = await _userManager.CreateAsync(user);
            await _userManager.AddClaimAsync(user, claimToAdd); //TO TEST
            await _userManager.AddClaimAsync(user, claimToAddToAccessToken); //TO TEST
            

            if (!result.Succeeded)
            {
                return View(vm);
            }

            //if succeed, add login method facebook associated with newly created user
            result = await _userManager.AddLoginAsync(user, info);

            if (!result.Succeeded)
            {
                return View(vm);
            }

            await _signInManager.SignInAsync(user, false); //will set identity.Cooki and asp.netcore.cookie
            var ctx = HttpContext;
            return Redirect(vm.ReturnUrl);
        }
    }

}
