namespace CMS.Post.Service
{
    using CMS.Base;
    using CMS.DataModel;
    public interface IPostService: IBaseService<Post>
    {
        Post GetCustom(int id);

        IEnumerable<Post> UpdateThenGetAll(string title);
    }
}
