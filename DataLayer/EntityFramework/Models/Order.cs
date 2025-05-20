using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer;
public class Order : BaseEntity
{
    public long Amount { get; set; }
    public int Status { get; set; }
    public string? respmsg { get; set; }
    public string? Authority { get; set; }
    public string? payload { get; set; }
    public string? tracenumber { get; set; }
    public DateTime datepaid { get; set; }
    public string? digitalreceipt { get; set; }
    public string? cardnumber { get; set; }
	public string? PhoneNumber { get; set; }
	public int UserId { get; set; }
    public User? User { get; set; }
}
public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Amount).IsRequired();
        //builder.Property(x => x.Status).IsRequired();
        //builder.Property(x => x.Authority).IsRequired();
        //builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.Slug).IsRequired();
        builder.HasIndex(x => x.Slug).IsUnique();

    }
}