using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer;
public class Otp : BaseEntity
{
    public int OtpCode { get; set; }
    public string Phone { get; set; }
    public DateTime ExpireDate { get; set; } = DateTime.Now.AddMinutes(2);
}
public class OtpConfiguration : IEntityTypeConfiguration<Otp>
{
    public void Configure(EntityTypeBuilder<Otp> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Phone).IsRequired();
    }
}
