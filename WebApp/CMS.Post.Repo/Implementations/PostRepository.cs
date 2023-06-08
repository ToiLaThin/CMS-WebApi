using CMS.Base;
namespace CMS.Post.Repo
{
    using CMS.DataModel; // ? why
    using Microsoft.EntityFrameworkCore;

    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository() { }
        public PostRepository(DbContext iContext): base(iContext) { }

        public Post HelloWorld(int id)
        {
            var result = this.Get(id);
            return result;
        }
    }
}
