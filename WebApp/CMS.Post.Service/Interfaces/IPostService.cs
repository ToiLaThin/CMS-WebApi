namespace CMS.Post.Service
{
    using CMS.Base;
    using CMS.DataModel;

    public interface IPostService: IBaseService<Post, Post_DTO>
    {
        IEnumerable<Post_DTO> GetAllCustom();

        Post_DTO AddCustom(Post_DTO postApi);

        Post_DTO? GetCustom(int id);

        Post_DTO? DeleteCustom(int id);

        Post_DTO? EditCustom(Post_DTO postApi);
        
    }
}
