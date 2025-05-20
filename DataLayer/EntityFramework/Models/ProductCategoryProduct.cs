using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer;
public class ProductCategoryProduct : BaseEntity
{
    public int ProductId { get; set; }
    public int ProductCategoryId { get; set; }

    public Product Product { get; set; }
    public ProductCategory ProductCategory { get; set; }
}
public class ProductCategoryProductConfiguration : IEntityTypeConfiguration<ProductCategoryProduct>
{
    public void Configure(EntityTypeBuilder<ProductCategoryProduct> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ProductId).IsRequired();
        builder.Property(x => x.ProductCategoryId).IsRequired();
        builder.Property(x => x.Slug).IsRequired();
        builder.HasIndex(x => x.Slug).IsUnique();

        builder.HasOne(x => x.ProductCategory).WithMany(x => x.ProductCategoryProducts).HasForeignKey(x => x.ProductCategoryId);
        builder.HasOne(x => x.Product).WithMany(x => x.ProductCategoryProducts).HasForeignKey(x => x.ProductId);
    }
}