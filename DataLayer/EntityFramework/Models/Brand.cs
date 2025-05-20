using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer;
public class Brand : BaseEntity
{
    public string Logo { get; set; }
    public string Name { get; set; }
    public string LogoAlt { get; set; }

    public List<Product> products { get; set; }
}
public class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Logo).IsRequired();
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.LogoAlt).IsRequired();
        builder.HasIndex(x => x.Slug).IsUnique();
    }
}