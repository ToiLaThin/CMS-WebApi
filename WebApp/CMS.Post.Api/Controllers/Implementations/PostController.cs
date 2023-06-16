using Microsoft.AspNetCore.Mvc;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
using CMS.Base;

namespace CMS.Post.Api.Controllers
{
    using CMS.DataModel;
    using CMS.Post.Service;
    using Microsoft.AspNetCore.Http.HttpResults;
    using System.Runtime.CompilerServices;

    [Route("Post")]
    [ApiController]
    public class PostController : BaseController<Post, Post_DTO>, IPostController
    {
        public PostController(IBaseService<Post, Post_DTO> iPostService) : base(iPostService) { }

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
