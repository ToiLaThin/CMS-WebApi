namespace CMS.Post.DataConnect
{
    //using inside namespace to avoid conflict 
    using CMS.DataModel;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class Post_Configuration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).HasColumnName("PostId").UseIdentityColumn(0, 1);
            builder.Property(o => o.Title).HasColumnName("PostTitle").HasMaxLength(500).IsRequired(true);
            builder.Property(o => o.Content).HasColumnName("PostContent").IsRequired(true);
            builder.Property(o => o.Status).HasColumnName("PostStatus").IsRequired(true);
            builder.Property(o => o.Image).HasColumnName("PostImageUrl").IsRequired(true);
            builder.Property(o => o.CreateDate).HasColumnName("PostCreateDate").IsRequired(true).HasDefaultValueSql("getdate()");
            builder.Property(o => o.CreateBy).HasColumnName("PostCreateBy").IsRequired(true).HasDefaultValue(0);

            builder.HasOne(p => p.Category)
                   .WithMany(c => c.Posts)
                   .HasForeignKey(p => p.CategoryId)
                   .IsRequired(true);

            builder.HasData(
                new { Id = -1, Title = "Title 1", Content = "Content 1", Status = StatusEnum.Draft, Image = "None", CategoryId = -1 },
                new { Id = -2, Title = "Title 2", Content = "Content 2", Status = StatusEnum.Draft, Image = "None", CategoryId = -2 },
                new { Id = -3, Title = "Title 3", Content = "Content 3", Status = StatusEnum.Draft, Image = "None", CategoryId = -1 }
                );
        }
    }
}
