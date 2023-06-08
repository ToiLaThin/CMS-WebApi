using Microsoft.AspNetCore.Mvc;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
using CMS.Base;
namespace CMS.Post.Api
{
    using CMS.DataModel;
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

        
    }
}
