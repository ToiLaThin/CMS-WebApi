using CMS.Base;

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

        public IEnumerable<Post> UpdateThenGetAll(string title)
        {
            var post = _postRepository.Find(p => p.Id == -1).First();
            post.Title = title;
            //now if you save the context, the change will persist
            //i tested without this below line and do UpdateThenGetAll Controller Action and then GetAll Controller Action => the change not persists
            this.UnitOfWork.SaveChanges();
            var result = _postRepository.GetAll();
            return result;
        }

        public Post GetCustom(int id)
        {
            var result = _postRepository.HelloWorld(id);
            return result;
        }
    }
}
