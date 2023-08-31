using CMS.Helper.UtilsClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Helper.SharedServices
{
    internal interface IEmailSenderService
    {
        Task<bool> SendMail(MailRequest mailContent);
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
