using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer;
public class ProductPropertyValueProduct : BaseEntity
{
    public int ProductId { get; set; }
    public int ProductPropertyValueId { get; set; }

    public Product Product { get; set; }
    public ProductPropertyValues ProductPropertyValues { get; set; }
}

public class ProductPropertyProductConfiguration : IEntityTypeConfiguration<ProductPropertyValueProduct>
{
    public void Configure(EntityTypeBuilder<ProductPropertyValueProduct> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ProductId).IsRequired();
        builder.Property(x => x.ProductPropertyValueId).IsRequired();
        builder.Property(x => x.Slug).IsRequired();
        builder.HasIndex(x => x.Slug).IsUnique();


        builder.HasOne(x => x.ProductPropertyValues).WithMany(x => x.ProductPropertyValueProducts).HasForeignKey(x => x.ProductPropertyValueId);
        builder.HasOne(x => x.Product).WithMany(x => x.ProductPropertyValueProducts).HasForeignKey(x => x.ProductId);
    }
}