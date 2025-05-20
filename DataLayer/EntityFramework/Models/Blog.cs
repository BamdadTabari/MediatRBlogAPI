
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer;
public class Blog : BaseEntity
{
    public string Name { get; set; }
    public string ShortDescription { get; set; }
    public string Image { get; set; }
    public string ImageAlt { get; set; }
    public string BlogText { get; set; }
    public bool ShowBlog { get; set; }
    public string KeyWords { get; set; }
	public string MetaDescription { get; set; }
	public int EstimatedReadTime { get; set; } 

	public int BlogCategoryId { get; set; }
    public BlogCategory BlogCategory { get; set; }
}
public class BlogConfiguration : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ShortDescription).IsRequired();
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Slug).IsRequired();
        builder.Property(x => x.Image).IsRequired();
        builder.Property(x => x.BlogText).IsRequired();
        builder.Property(x => x.Slug).IsRequired();
        builder.Property(x => x.KeyWords).IsRequired();
        builder.HasIndex(x => x.Slug).IsUnique();
        builder.HasIndex(x => x.ShowBlog);

        builder.HasOne(x => x.BlogCategory).WithMany(x => x.Blogs).HasForeignKey(x => x.BlogCategoryId).OnDelete(DeleteBehavior.Cascade);

    }
}