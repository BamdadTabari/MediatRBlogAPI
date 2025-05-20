using Microsoft.EntityFrameworkCore;

namespace DataLayer;
public interface IProductPropertyProductRepository : IRepository<ProductPropertyValueProduct>
{
    Task<ProductPropertyValueProduct?> Get(string slug);
}
public class ProductPropertyProductRepository : Repository<ProductPropertyValueProduct>, IProductPropertyProductRepository
{
    private readonly IQueryable<ProductPropertyValueProduct> _queryable;


    public ProductPropertyProductRepository(ApplicationDbContext context) : base(context)
    {
        _queryable = DbContext.Set<ProductPropertyValueProduct>();
    }

    public async Task<ProductPropertyValueProduct?> Get(string slug)
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