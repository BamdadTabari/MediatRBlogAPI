using Microsoft.EntityFrameworkCore;

namespace DataLayer;
public interface IProductCategoryProductRepository : IRepository<ProductCategoryProduct>
{
    Task<ProductCategoryProduct?> Get(string slug);
}
public class ProductCategoryProductRepository : Repository<ProductCategoryProduct>, IProductCategoryProductRepository
{
    private readonly IQueryable<ProductCategoryProduct> _queryable;


    public ProductCategoryProductRepository(ApplicationDbContext context) : base(context)
    {
        _queryable = DbContext.Set<ProductCategoryProduct>();
    }

    public async Task<ProductCategoryProduct?> Get(string slug)
    {
        try
        {
            return await _queryable.SingleOrDefaultAsync(x => x.Slug == slug);
        }
        catch
        {
            return null;
        }
    }
}