using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer;
public class Cart : BaseEntity
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductImage { get; set; }
    public double UnitPrice { get; set; }
    public int Quantity { get; set; }
	public int CountInStore { get; set; }

	public int BasketId { get; set; }
    public Product Product { get; set; }
    public Basket Basket { get; set; }
}

public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Slug).IsUnique();
        builder.Property(x => x.ProductId).IsRequired();
        builder.Property(x => x.ProductName).IsRequired();
        builder.Property(x => x.UnitPrice).IsRequired();
        builder.Property(x => x.Quantity).IsRequired();
		builder.Property(x => x.CountInStore).IsRequired();

		builder.HasOne(x => x.Product).WithMany(x => x.Carts).HasForeignKey(x => x.ProductId);
    }
}
