using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer;
public class ProductPropertyValues : BaseEntity
{
    public string PropertyValue { get; set; }

    public int ProductPropertyId { get; set; }
    public ProductProperty ProductProperty { get; set; }
    public ICollection<ProductPropertyValueProduct> ProductPropertyValueProducts { get; set; } = [];
}
public class ProductPropertyValuesConfiguration : IEntityTypeConfiguration<ProductPropertyValues>
{
    public void Configure(EntityTypeBuilder<ProductPropertyValues> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.PropertyValue).IsRequired();
        builder.Property(x => x.Slug).IsRequired();
        builder.HasIndex(x => x.Slug).IsUnique();

        builder.HasOne(x => x.ProductProperty).WithMany(x => x.ProductPropertyValuesList).HasForeignKey(x => x.ProductPropertyId);
    }
}