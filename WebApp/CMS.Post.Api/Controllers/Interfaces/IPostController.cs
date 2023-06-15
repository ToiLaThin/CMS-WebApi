using Microsoft.AspNetCore.Mvc;

namespace CMS.Post.Api.Controllers
{
    using CMS.DataModel;
    public interface IPostController
    {
        ActionResult<IEnumerable<Post>> GetAll();

        ActionResult<Post> GetOne(int id);

        ActionResult<Post> Add(Post post); //the reuturn type must match the frontend, otherwise, the controller won't receive from fe

        ActionResult<Post> Delete(int id);

        ActionResult<Post> Update(Post post);


    }
}
