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
    public class PostController : BaseController<Post>, IPostController
    {
        public PostController(IBaseService<Post> iPostService) : base(iPostService) { }

        [HttpGet]
        [Route("Get")]
        public ActionResult<Post> GetOne(int id)
        {
            IPostService postService = this._service as PostService;
            Post post = postService.GetCustom(id);
            if (post != null)
            {
                var result = new OkObjectResult(post); //okObjectResult can have not only an status but the object
                return result;
            }
            return new NotFoundResult(); //only status code
        }



        [HttpGet]
        [Route("GetAll")]
        ActionResult<IEnumerable<Post>> IPostController.GetAll()
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
        public ActionResult<Post> Add([FromBody] Post post)
        {
            IPostService postService = this._service as PostService;
            var result = postService.AddCustom(post);
            if (result != null)
            {
                return new OkObjectResult(result);
            }
            else
                return new ConflictObjectResult(post);
        }

        [HttpDelete]
        [Route("Delete")]
        public ActionResult<Post> Delete([FromQuery] int id)
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
        public ActionResult<Post> Update([FromBody] Post post)
        {
            IPostService postService = this._service as PostService;
            var result = postService.EditCustom(post);
            if (result != null)
            {
                return new OkObjectResult(result);
            }
            return new NotFoundResult();
        }
    }
}
