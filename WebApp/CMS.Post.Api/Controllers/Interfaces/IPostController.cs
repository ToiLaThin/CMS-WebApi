using Microsoft.AspNetCore.Mvc;

namespace CMS.Post.Api
{
    using CMS.DataModel;
    public interface IPostController
    {
        ActionResult<IEnumerable<Post_DTO>> GetAll();

        ActionResult<Post_DTO> GetOne(int id);

        ActionResult<Post_DTO> Add(Post_DTO post); //the reuturn type must match the frontend, otherwise, the controller won't receive from fe

        ActionResult<Post_DTO> Delete(int id);

        ActionResult<Post_DTO> Update(Post_DTO post);


    }
}
