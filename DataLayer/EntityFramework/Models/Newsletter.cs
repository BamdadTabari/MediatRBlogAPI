using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer;
public class Newsletter : BaseEntity
{
    public string PhoneNumber { get; set; }
}
public class NewsletterConfiguration : IEntityTypeConfiguration<Newsletter>
{
    public void Configure(EntityTypeBuilder<Newsletter> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Slug).IsRequired();
        builder.Property(x => x.PhoneNumber).IsRequired();
        builder.HasIndex(x => x.Slug).IsUnique();
    }
}