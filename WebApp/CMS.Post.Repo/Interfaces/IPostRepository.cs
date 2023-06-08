using System;
using System.Collections.Generic;
using System.Linq;
namespace CMS.Post.Repo
{
    using CMS.DataModel; // ? why

    public interface IPostRepository
    {
        Post HelloWorld(int id);
    }
}
