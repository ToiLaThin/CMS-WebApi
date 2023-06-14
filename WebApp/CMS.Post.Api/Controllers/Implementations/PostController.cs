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

        //public ActionResult Delete(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public ActionResult Edit(Post post)
        //{
        //    throw new NotImplementedException();
        //}

        //public ActionResult GetOne(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //[HttpGet]
        //[Route("Get")]
        //public Post Get(int id)
        //{
        //    var result = _service.FindById(id);
        //    return result;

        //}



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
    }
}
