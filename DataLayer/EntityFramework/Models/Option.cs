using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataLayer;
public class Option : BaseEntity
{
	public string OptionKey { get; set; }
	public string OptionValue { get; set; }
}
public class OptionConfiguration : IEntityTypeConfiguration<Option>
{
	public void Configure(EntityTypeBuilder<Option> builder)
	{
		builder.HasKey(x => x.Id);
		builder.Property(x => x.OptionKey).IsRequired();
		builder.Property(x => x.OptionValue).IsRequired();
		builder.Property(x => x.Slug).IsRequired();
		builder.HasIndex(x => x.Slug).IsUnique();
	}
}