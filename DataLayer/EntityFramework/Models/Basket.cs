using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer;
public class Basket : BaseEntity
{
    public int? UserId { get; set; }
    public string? SessionId { get; set; }
    public double FinalPrice { get; set; }
    public ICollection<Cart> Carts { get; set; }
    public User User { get; set; }
    public bool IsPaymentDone { get; set; }
    public string UserAddress { get; set; }
    public string UserPhone { get; set; }
    public string UserPostalCode { get; set; }

    public string UserFirstName { get; set; }
    public string UserLastName { get; set; }
    public string UserCity { get; set; }
    public string UserProvince { get; set; }

	public bool UseWallet { get; set; }

	public bool IsSended { get; set; }
	public bool IsRead { get; set; } = false;
}

public class BasketConfiguration : IEntityTypeConfiguration<Basket>
{
    public void Configure(EntityTypeBuilder<Basket> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Slug).IsUnique();

        builder.HasOne(x => x.User).WithMany(x => x.Baskets).HasForeignKey(x => x.UserId);
        builder.HasMany(x => x.Carts).WithOne(x => x.Basket).HasForeignKey(x => x.BasketId);
    }
}
