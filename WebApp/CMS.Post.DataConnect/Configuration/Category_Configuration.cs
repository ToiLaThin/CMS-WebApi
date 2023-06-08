using CMS.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Post.DataConnect
{
    internal class Category_Configuration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("CategoryId").UseIdentityColumn(0, 1);
            builder.Property(x => x.Name).HasColumnName("CategoryName").HasMaxLength(500).IsRequired(true);
            builder.Property(x => x.Description).HasColumnName("CategoryDescription").IsRequired(true);
            builder.Property(o => o.CreateDate).HasColumnName("CategoryCreateDate").IsRequired(true).HasDefaultValueSql("getdate()");
            builder.Property(o => o.CreateBy).HasColumnName("CategoryCreateBy").IsRequired(true).HasDefaultValue(0);

            //builder.HasMany(c => c.Posts).WithOne(p => p.Category).HasForeignKey(p => p.CategoryId).IsRequired(true);

            builder.HasData(
                new { Id = -1, Name = "Default", Description = "Unknown" },
                new { Id = -2, Name = "Sport", Description = "Physical Activitis and all kind of outdoor activities" }
            );
        }
    }
}
