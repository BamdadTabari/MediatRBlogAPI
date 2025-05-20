using Microsoft.EntityFrameworkCore;

namespace DataLayer;
public interface IBrandRepository : IRepository<Brand>
{
    Task<Brand?> Get(string slug);
    Task<Brand?> Get(int id);
    PaginatedList<Brand> GetPaginated(DefaultPaginationFilter filter);
    Task<List<Brand>> GetAll();
}
public class BrandRepository : Repository<Brand>, IBrandRepository
{
    private readonly IQueryable<Brand> _queryable;


    public BrandRepository(ApplicationDbContext context) : base(context)
    {
        _queryable = DbContext.Set<Brand>();
    }

    public async Task<Brand?> Get(string slug)
    {
        try
        {
            return await _queryable.Include(x => x.products).SingleOrDefaultAsync(x => x.Slug == slug);
        }
        catch
        {
            return null;
        }
    }

    public async Task<Brand?> Get(int id)
    {
        try
        {
            return await _queryable.Include(x => x.products).SingleOrDefaultAsync(x => x.Id == id);
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<Brand>> GetAll()
    {
        try
        {
            return await _queryable.AsNoTracking().ToListAsync();
        }
        catch
        {
            return [];
        }
    }

    public PaginatedList<Brand> GetPaginated(DefaultPaginationFilter filter)
    {
        try
        {
            var query = _queryable.Include(x => x.products).AsNoTracking().Skip((filter.Page - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ApplyFilter(filter).ApplySort(filter.SortBy);
            var dataTotalCount = _queryable.Count();
            return new PaginatedList<Brand>([.. query], dataTotalCount, filter.Page, filter.PageSize);
        }
        catch
        {
            return new PaginatedList<Brand>([], 0, filter.Page, filter.PageSize);
        }
    }
}