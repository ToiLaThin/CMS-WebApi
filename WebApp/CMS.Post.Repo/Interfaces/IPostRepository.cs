using System;
using System.Collections.Generic;
using System.Linq;
namespace CMS.Post.Repo
{
    using CMS.Base;
    using CMS.DataModel; // ? why

    public interface IPostRepository: IBaseRepository<Post>
    {
        Post HelloWorld(int id);
    }
}
