using CMS.Base;
using CMS.Helper;
namespace CMS.Post.Service
{
    using CMS.DataModel;
    using CMS.Post.Repo;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

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

        public Post? GetCustom(int id)
        {
            Post result = this._postRepository.Find(p => p.Id.Equals(id)).FirstOrDefault();
            return result ?? null;
        }

        public Post? DeleteCustom(int id)
        {
            Post? postToFind = this._postRepository.Get<int>(id);
            if(postToFind != null)
            {
                this._postRepository.Remove(postToFind);
                try
                {
                    this.UnitOfWork.SaveChanges();
                    return postToFind;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return null;
        }

        public Post? EditCustom(Post post)
        {
            int postId = post.Id;
            Post? postToFind = this._postRepository.Get<int>(postId);
            if (postToFind != null)
            {
                PostBuilder postBuilder = new PostBuilder(postToFind);
                Post postToUpdate = postBuilder.SetPostPropsWithOutId(post).Build();
                //nếu dùng this._postRepository.Update(post); -> lỗi đã có entity được track -> TODO tìm hiểu thêm
                //dùng post builder với các setter để override lại các prop của postToFind theo post(post là giá trị cần edit)
                
                this._postRepository.Update(postToUpdate);
                try
                {
                    this.UnitOfWork.SaveChanges();
                    return post;
                }
                catch (DbUpdateConcurrencyException ex) {
                    throw new Exception(ex.Message);
                }
            }
            return null;
        }
    }
}
