using CMS.DataModel;

namespace CMS.Helper
{
    public class PostBuilder
    {
        private Post _post;
        public PostBuilder() { 
            this._post = new Post() { };
        }

        public PostBuilder(Post post)
        {
            this._post = post;
        }

        public PostBuilder SetPostTitle(string postTitle)
        {
            this._post.Title = postTitle;
            return this;
        }

        public PostBuilder SetPostImage(string postImage)
        {
            this._post.Image = postImage;
            return this;
        }

        public PostBuilder SetPostContent(string postContent)
        {
            this._post.Content = postContent;
            return this;
        }

        public PostBuilder SetPostStatus(StatusEnum postStatus)
        {
            this._post.Status = postStatus;
            return this;
        }

        public PostBuilder SetPostCategoryId(int categoryId)
        {
            this._post.CategoryId = categoryId;
            return this;
        }

        public PostBuilder SetPostPropsWithOutId(Post postWithId)
        {
            this.SetPostTitle(postWithId.Title)
                .SetPostContent(postWithId.Content)
                .SetPostImage(postWithId.Image)
                .SetPostStatus(postWithId.Status)
                .SetPostCategoryId(postWithId.CategoryId);
            return this;
        }

        public Post Build()
        {
            return this._post;
        }
    }
}
