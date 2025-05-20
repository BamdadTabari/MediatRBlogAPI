using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer;
public class ProductProperty : BaseEntity
{
    public string PropertyKey { get; set; }

    public ICollection<ProductPropertyValues> ProductPropertyValuesList { get; set; } = [];
}
public class ProductPropertyConfiguration : IEntityTypeConfiguration<ProductProperty>
{
    public void Configure(EntityTypeBuilder<ProductProperty> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.PropertyKey).IsRequired();
        builder.Property(x => x.Slug).IsRequired();
        builder.HasIndex(x => x.Slug).IsUnique();
    }
}