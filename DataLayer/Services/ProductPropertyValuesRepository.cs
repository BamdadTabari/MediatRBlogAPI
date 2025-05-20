using Microsoft.EntityFrameworkCore;

namespace DataLayer;
public interface IProductPropertyValuesRepository : IRepository<ProductPropertyValues>
{
    Task<ProductPropertyValues?> Get(string slug);
    Task<ProductPropertyValues?> Get(int id);
    Task<List<ProductPropertyValues>?> GetAll();
    PaginatedList<ProductPropertyValues> GetPaginated(DefaultPaginationFilter filter);
}
public class ProductPropertyValuesRepository : Repository<ProductPropertyValues>, IProductPropertyValuesRepository
{
    private readonly IQueryable<ProductPropertyValues> _queryable;


    public ProductPropertyValuesRepository(ApplicationDbContext context) : base(context)
    {
        _queryable = DbContext.Set<ProductPropertyValues>();
    }

    public async Task<ProductPropertyValues?> Get(string slug)
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

    public async Task<ProductPropertyValues?> Get(int id)
    {
        try
        {
            return await _queryable.SingleOrDefaultAsync(x => x.Id == id);
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<ProductPropertyValues>?> GetAll()
    {
        try
        {
            return await _queryable.Include(x=>x.ProductProperty).AsNoTracking().ToListAsync();
        }
        catch
        {
            return null;
        }
    }

    public PaginatedList<ProductPropertyValues> GetPaginated(DefaultPaginationFilter filter)
    {
        try
        {
            var query = _queryable.Include(x => x.ProductProperty).AsNoTracking().Skip((filter.Page - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ApplyFilter(filter).ApplySort(filter.SortBy);
            var dataTotalCount = _queryable.Count();
            return new PaginatedList<ProductPropertyValues>([.. query], dataTotalCount, filter.Page, filter.PageSize);
        }
        catch
        {
            return new PaginatedList<ProductPropertyValues>([], 0, filter.Page, filter.PageSize);
        }
    }
}