using CMS.Base;

namespace CMS.Post.Service
{
    using CMS.DataModel;
    public class PostService: BaseService<Post>, IPostService
    {
        public PostService(IUnitOfWork<Post> unitOfWork) : base(unitOfWork)
        {
        }
    }
}
