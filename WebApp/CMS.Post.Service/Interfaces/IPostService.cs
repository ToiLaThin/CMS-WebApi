namespace CMS.Post.Service
{
    using CMS.Base;
    using CMS.DataModel;

    public interface IPostService: IBaseService<Post>
    {
        IEnumerable<Post> GetAllCustom();

        Post AddCustom(Post post);

        Post? GetCustom(int id);

        Post? DeleteCustom(int id);

        Post? EditCustom(Post post);
        
    }
}
