using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer;
public class ProductCategory : BaseEntity
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }

    // رابطه‌ی خودارجاعی
    public int? ParentId { get; set; }
    public ProductCategory? Parent { get; set; }
    public ICollection<ProductCategory> Children { get; set; } = [];

    public ICollection<ProductCategoryProduct> ProductCategoryProducts { get; set; } = [];
}

public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).IsRequired();
        builder.Property(x => x.Slug).IsRequired();
        builder.HasIndex(x => x.Slug).IsUnique();

        builder.HasOne(x => x.Parent)
            .WithMany(x => x.Children)  // رابطه‌ی یک به چند
            .HasForeignKey(x => x.ParentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}