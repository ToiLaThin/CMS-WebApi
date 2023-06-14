using Microsoft.AspNetCore.Mvc;

namespace CMS.Post.Api.Controllers
{
    using CMS.DataModel;
    public interface IPostController
    {
        ActionResult<IEnumerable<Post>> GetAll();

        ActionResult<Post> GetOne(int id);

        ActionResult<Post> Add(Post post);

        ActionResult Delete(int id);

        ActionResult Update(Post post);


    }
}
