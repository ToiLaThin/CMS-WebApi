using CMS.Base;
using CMS.Helper;
namespace CMS.Post.Service
{
    using CMS.DataModel;
    using CMS.Post.Repo;

    public class PostService: BaseService<Post>, IPostService
    {
        private IPostRepository _postRepository;
        public PostService(IUnitOfWork<Post> iUoW) : base(iUoW)
        {
            //this._unitOfWork = iUoW; //already set this in base dependency injection
            if (_unitOfWork.EntityRepository<PostRepository>() is IBaseRepository<Post> postRepository)
                this._postRepository = postRepository as IPostRepository;
        }

        public IEnumerable<Post> GetAllCustom()
        {
            var allPosts = base.GetAll();
            return allPosts;
        }

        public Post AddCustom(Post post)
        {
            int? postId = post.Id;
            Post? postFinded = this._postRepository.Find(p => p.Id == postId).FirstOrDefault();
            if(postFinded != null)
            {
                return null;
            }
            else
            {
                PostBuilder postBuilder = new PostBuilder();
                Post postToAdd = postBuilder.SetPostPropsWithOutId(post).Build(); //only for swagger the frontend post won't have id with a value;

                this._postRepository.Add(postToAdd); //must use another post to cut the id since it is auto increase
                this.UnitOfWork.SaveChanges();
                return post;
            }
        }
    }
}
