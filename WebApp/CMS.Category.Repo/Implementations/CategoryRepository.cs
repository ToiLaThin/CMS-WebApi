using CMS.Base;

namespace CMS.Category.Repo
{
    using CMS.DataModel;
    using Microsoft.EntityFrameworkCore;

    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository() { }
        public CategoryRepository(DbContext iContext) : base(iContext) {}
    }
}
