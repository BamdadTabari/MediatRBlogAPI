using Microsoft.EntityFrameworkCore;

namespace DataLayer;

public interface IProductPropertyRepository : IRepository<ProductProperty>
{
    Task<ProductProperty?> Get(string slug);
    Task<ProductProperty?> Get(int id);
    PaginatedList<ProductProperty> GetPaginated(DefaultPaginationFilter filter);
    Task<List<ProductProperty>> GetAll();
}
public class ProductPropertyRepository : Repository<ProductProperty>, IProductPropertyRepository
{
    private readonly IQueryable<ProductProperty> _queryable;


    public ProductPropertyRepository(ApplicationDbContext context) : base(context)
    {
        _queryable = DbContext.Set<ProductProperty>();
    }

    public async Task<ProductProperty?> Get(string slug)
    {
        try
        {
            return await _queryable.Include(x => x.ProductPropertyValuesList).SingleOrDefaultAsync(x => x.Slug == slug);
        }
        catch
        {
            return null;
        }
    }

    public async Task<ProductProperty?> Get(int id)
    {
        try
        {
            return await _queryable.Include(x => x.ProductPropertyValuesList).SingleOrDefaultAsync(x => x.Id == id);
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<ProductProperty>> GetAll()
    {
        try
        {
            return await _queryable.Include(x => x.ProductPropertyValuesList).AsNoTracking().AsNoTracking().ToListAsync();
        }
        catch
        {
            return [];
        }
    }

    public PaginatedList<ProductProperty> GetPaginated(DefaultPaginationFilter filter)
    {
        try
        {
            var query = _queryable.Include(x => x.ProductPropertyValuesList).AsNoTracking().Skip((filter.Page - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ApplyFilter(filter).ApplySort(filter.SortBy);
            var dataTotalCount = _queryable.Count();
            return new PaginatedList<ProductProperty>([.. query], dataTotalCount, filter.Page, filter.PageSize);
        }
        catch
        {
            return new PaginatedList<ProductProperty>([], 0, filter.Page, filter.PageSize);
        }
    }
}