using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer;
public class ProductComment : BaseEntity
{
    public string Email { get; set; }
    public string Name { get; set; }
    public int Star { get; set; }
    public string Comment { get; set; }
    public bool IsActive { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }
}

public class ProductCommentConfiguration : IEntityTypeConfiguration<ProductComment>
{
    public void Configure(EntityTypeBuilder<ProductComment> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Star).IsRequired();
        builder.Property(x => x.Comment).IsRequired();
        builder.Property(x => x.ProductId).IsRequired();
        builder.Property(x => x.Slug).IsRequired();
        builder.HasIndex(x => x.Slug).IsUnique();

    }
}