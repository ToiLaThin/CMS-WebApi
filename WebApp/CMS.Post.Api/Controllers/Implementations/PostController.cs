// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
using CMS.Base;
using Microsoft.AspNetCore.Mvc;
using CMS.Helper;

namespace CMS.Post.Api
{
    using CMS.DataModel;
    using CMS.Helper.NewFolder;
    using CMS.Post.Service;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;

    [Route("Post")]
    [ApiController]
    public class PostController : BaseController<Post, Post_DTO>, IPostController
    {
        public PostController(IBaseService<Post, Post_DTO> iPostService) : base(iPostService) { }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("Get")]
        public ActionResult<Post_DTO> GetOne(int id)
        {
            IPostService postService = this._service as PostService;
            Post_DTO postApi = postService.GetCustom(id);
            if (postApi != null)
            {
                var result = new OkObjectResult(postApi); //okObjectResult can have not only an status but the object
                return result;
            }
            return new NotFoundResult(); //only status code
        }


        [Authorize(Policy = PolicyNames.AdminPolicy, Roles = $"{RoleType.Admin},{RoleType.AuthenticatedUser}")]
        [HttpGet]
        [Route("GetAll")]
        ActionResult<IEnumerable<Post_DTO>> IPostController.GetAll()
        {
            IPostService postService = this._service as PostService;
            var result = postService.GetAllCustom();
            if (result != null)
            {
                return new OkObjectResult(result);
            }
            else
                return new NotFoundResult();
        }

        [Authorize(Policy = PolicyNames.AdminPolicy, Roles = $"{RoleType.Admin}")]
        [HttpPost]
        [Route("Add")]
        public ActionResult<Post_DTO> Add([FromBody] Post_DTO postApi)
        {
            IPostService postService = this._service as PostService;
            var result = postService.AddCustom(postApi);
            if (result != null)
            {
                return new OkObjectResult(result);
            }
            else
                return new ConflictObjectResult(postApi);
        }

        [Authorize(Policy = PolicyNames.AuthenticatedUserPolicy, Roles = $"{RoleType.AuthenticatedUser}")]
        [HttpDelete]
        [Route("Delete")]
        public ActionResult<Post_DTO> Delete([FromQuery] int id)
        {
            IPostService postService = this._service as PostService;
            var result = postService.DeleteCustom(id);
            if (result != null)
            {
                return new OkObjectResult(result);
            }
            return new NotFoundResult();
        }

        [HttpPut]
        [Route("Update")]
        public ActionResult<Post_DTO> Update([FromBody] Post_DTO postApi)
        {
            IPostService postService = this._service as PostService;
            var result = postService.EditCustom(postApi);
            if (result != null)
            {
                return new OkObjectResult(result);
            }
            return new NotFoundResult();
        }
    }
}
