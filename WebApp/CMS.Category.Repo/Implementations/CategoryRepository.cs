using CMS.Base;
using Microsoft.EntityFrameworkCore;

namespace CMS.Category.Repo
{
    using CMS.DataModel;

    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository() { }
        public CategoryRepository(DbContext iContext) : base(iContext) {}
    }
}
