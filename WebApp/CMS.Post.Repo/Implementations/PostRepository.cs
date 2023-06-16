using CMS.Base;
using Microsoft.EntityFrameworkCore;
namespace CMS.Post.Repo
{
    using CMS.DataModel; // ? why

    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository() { }
        public PostRepository(DbContext iContext): base(iContext) { }
        
    }
}
