using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer;
public class BulkOrder:BaseEntity
{
    public string PhoneNumber { get; set; }
    public string FullName { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public bool IsRead { get; set; } = false;
}
public class BulkOrderConfiguration : IEntityTypeConfiguration<BulkOrder>
{
    public void Configure(EntityTypeBuilder<BulkOrder> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Slug).IsUnique();
        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x=>x.FullName).IsRequired();
        builder.Property(x => x.PhoneNumber).IsRequired();
    }
}
