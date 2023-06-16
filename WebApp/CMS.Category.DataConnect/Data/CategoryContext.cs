using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CMS.Category.DataConnect
{
    public partial class CategoryContext : DbContext 
    {
        public CategoryContext() : base() { }

        public CategoryContext(DbContextOptions<CategoryContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
