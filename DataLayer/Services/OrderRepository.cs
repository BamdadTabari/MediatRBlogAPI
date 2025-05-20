using Microsoft.EntityFrameworkCore;

namespace DataLayer;

public interface IOrderRepository : IRepository<Order>
{
    Task<Order?> Get(string slug);
	Task<Order?> GetByAuthority(string authority);
	PaginatedList<Order> GetPaginated(DefaultPaginationFilter filter);
    Task<List<Order>> GetAll();
}
public class OrderRepository : Repository<Order>, IOrderRepository
{
    private readonly IQueryable<Order> _queryable;


    public OrderRepository(ApplicationDbContext context) : base(context)
    {
        _queryable = DbContext.Set<Order>();
    }

    public async Task<Order?> Get(string slug)
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

    public async Task<List<Order>> GetAll()
    {
        try
        {
            return await _queryable.Include(x => x.User).AsNoTracking().AsNoTracking().ToListAsync();
        }
        catch
        {
            return [];
        }
    }

	public async Task<Order?> GetByAuthority(string authority)
	{
		try
		{
			return await _queryable.Include(x => x.User).SingleOrDefaultAsync(x => x.Authority == authority);
		}
		catch
		{
			return null;
		}
	}

	public PaginatedList<Order> GetPaginated(DefaultPaginationFilter filter)
    {
        try
        {
            var query = _queryable.Include(x => x.User).AsNoTracking().Skip((filter.Page - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ApplyFilter(filter).ApplySort(filter.SortBy);
            var dataTotalCount = _queryable.Count();
            return new PaginatedList<Order>([.. query], dataTotalCount, filter.Page, filter.PageSize);
        }
        catch
        {
            return new PaginatedList<Order>([], 0, filter.Page, filter.PageSize);
        }
    }
}