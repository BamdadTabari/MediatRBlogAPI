using Microsoft.EntityFrameworkCore;

namespace DataLayer;
public interface IBulkOrderRepository : IRepository<BulkOrder>
{
    Task<BulkOrder?> Get(string slug);
    Task<BulkOrder?> Get(int id);
    PaginatedList<BulkOrder> GetPaginated(DefaultPaginationFilter filter);
    Task<List<BulkOrder>> GetAll();
}
public class BulkOrderRepository : Repository<BulkOrder>, IBulkOrderRepository
{
    private readonly IQueryable<BulkOrder> _queryable;


    public BulkOrderRepository(ApplicationDbContext context) : base(context)
    {
        _queryable = DbContext.Set<BulkOrder>();
    }

    public async Task<BulkOrder?> Get(string slug)
    {
        try
        {
            return await _queryable.Include(x => x.User).SingleOrDefaultAsync(x => x.Slug == slug);
        }
        catch
        {
            return null;
        }
    }

    public async Task<BulkOrder?> Get(int id)
    {
        try
        {
            return await _queryable.Include(x => x.User).SingleOrDefaultAsync(x => x.Id == id);
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<BulkOrder>> GetAll()
    {
        try
        {
            return await _queryable.Include(x => x.User).AsNoTracking().ToListAsync();
        }
        catch
        {
            return [];
        }
    }

    public PaginatedList<BulkOrder> GetPaginated(DefaultPaginationFilter filter)
    {
        try
        {
            var query = _queryable.Include(x => x.User).Skip((filter.Page - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ApplyFilter(filter).ApplySort(filter.SortBy);
            var dataTotalCount = _queryable.Count();
            return new PaginatedList<BulkOrder>([.. query], dataTotalCount, filter.Page, filter.PageSize);
        }
        catch
        {
            return new PaginatedList<BulkOrder>([], 0, filter.Page, filter.PageSize);
        }
    }
}