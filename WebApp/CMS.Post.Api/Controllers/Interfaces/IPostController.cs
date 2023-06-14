using Microsoft.AspNetCore.Mvc;

namespace CMS.Post.Api.Controllers
{
    using CMS.DataModel;
    public interface IPostController
    {
        ActionResult<IEnumerable<Post>> GetAll();

        //ActionResult GetOne(int id);

        ActionResult<Post> Add(Post post);

        //ActionResult Delete(int id);

        //ActionResult Edit(Post post);


    }
}
