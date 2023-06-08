using Microsoft.AspNetCore.Mvc;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
using CMS.Base;
namespace CMS.Post.Api
{
    using CMS.DataModel;
    using CMS.Post.Service;
    using System.Runtime.CompilerServices;

    [Route("Post")]
    [ApiController]
    public class PostController : BaseController<Post>
    {
        public PostController(IBaseService<Post> iPostService) : base(iPostService) {}

        [HttpGet]
        [Route("Get")]
        public Post Get(int id)
        {
            var result = this._service.FindById(id);
            return result;

        }

        [HttpGet]
        [Route("GetCustom")]
        public Post GetCustom(int id) {
            IPostService postService = this._service as PostService;
            var result = postService.GetCustom(id);
            return result;
        }

        [HttpPost]
        [Route("UpdateThenGetAll")]
        public IEnumerable<Post> UpdateThenGetAll(string newTitle) {
            IPostService postService = this._service as PostService;
            var result = postService.UpdateThenGetAll(newTitle);
            return result;
        }
        
    }
}
