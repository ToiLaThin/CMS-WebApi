using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CMS.Post.DataConnect
{
    //definition of class is split (same class name is used multiple times)
    public partial class PostContext : DbContext
    {
        public PostContext() : base() { }

        public PostContext(DbContextOptions<PostContext> options) : base(options) { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //}

        #region ignore this part
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //modelBuilder.ApplycationDefaultEntityTypeConfiguration();

            //foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            //{
            //    entityType.SetTableName(entityType.GetTableName());
            //}
        }
        //public override int SaveChanges()
        //{
        //    var now = DateTime.UtcNow;
        //    foreach (var changeEntity in ChangeTracker.Entries())
        //    {
        //        if (changeEntity.Entity is IBaseDataModel entity)
        //        {
        //            switch (changeEntity.State)
        //            {
        //                case EntityState.Added:
        //                    entity.CreatedDate = now;
        //                    break;

        //                case EntityState.Modified:
        //                    entity.ModifiedDate = now;
        //                    break;
        //            }
        //        }
        //    }
        //    return base.SaveChanges();
        //}
        #endregion
    }
}
