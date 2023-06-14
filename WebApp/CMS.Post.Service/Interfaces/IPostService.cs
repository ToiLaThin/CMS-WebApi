namespace CMS.Post.Service
{
    using CMS.Base;
    using CMS.DataModel;

    public interface IPostService: IBaseService<Post>
    {
        IEnumerable<Post> GetAllCustom();

        Post AddCustom(Post post);
        

    }
}
