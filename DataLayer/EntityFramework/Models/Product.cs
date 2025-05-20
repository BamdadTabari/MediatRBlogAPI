using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace DataLayer;
public class Product : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public List<string> Images { get; set; }
    public double Price { get; set; }
    public double FinalPrice { get; set; }
    public double BulkPrice { get; set; }
    public int SellCount { get; set; }
    [Range(1, 5)]
    public double Star { get; set; }
    public bool IsActive { get; set; }
	public int Count { get; set; }
	public int? BrandId { get; set; }
    public Brand Brand { get; set; }
    public ICollection<ProductCategoryProduct> ProductCategoryProducts { get; set; } = [];
    public ICollection<ProductPropertyValueProduct> ProductPropertyValueProducts { get; set; } = [];
    public ICollection<ProductComment> ProductComments { get; set; } = [];

    public ICollection<Cart> Carts { get; set; }

}
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).IsRequired();
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.Images).IsRequired();
        builder.Property(x => x.Price).IsRequired();
        builder.Property(x => x.FinalPrice).IsRequired();
        builder.Property(x => x.BulkPrice).IsRequired();
        builder.Property(x => x.Slug).IsRequired();
        builder.HasIndex(x => x.Slug).IsUnique();

        builder.HasOne(x => x.Brand).WithMany(x => x.products).HasForeignKey(x => x.BrandId);
        builder.HasMany(x => x.ProductComments).WithOne(x => x.Product).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Cascade);
    }
}
