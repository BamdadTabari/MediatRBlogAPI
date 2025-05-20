using Microsoft.EntityFrameworkCore;

namespace DataLayer;
public interface IBasketRepository : IRepository<Basket>
{
    Task<Basket?> Get(string slug);
    Task<Basket?> GetByUserId(int userId);
	PaginatedList<Basket> GetListByUserId(int userId, DefaultPaginationFilter filter);
	Task<int> GetAllByUserId(int userId, bool? isDone);
	Task<Basket?> Get(int id);
    Task<Basket?> GetBySessionIdOrUserId(int? userId, string? sessionId);
    PaginatedList<Basket> GetPaginated(DefaultPaginationFilter filter);
    Task<List<Basket>> GetAll();
}
public class BasketRepository : Repository<Basket>, IBasketRepository
{
    private readonly IQueryable<Basket> _queryable;


    public BasketRepository(ApplicationDbContext context) : base(context)
    {
        _queryable = DbContext.Set<Basket>();
    }

    public async Task<Basket?> Get(string slug)
    {
        try
        {
            return await _queryable.Include(x => x.Carts).Include(x => x.User).SingleOrDefaultAsync(x => x.Slug == slug);
        }
        catch
        {
            return null;
        }
    }

    public async Task<Basket?> Get(int id)
    {
        try
        {
            return await _queryable.Include(x => x.Carts).Include(x => x.User).SingleOrDefaultAsync(x => x.Id == id);
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<Basket>> GetAll()
    {
        try
        {
            return await _queryable.Include(x => x.Carts).Include(x => x.User).AsNoTracking().ToListAsync();
        }
        catch
        {
            return [];
        }
    }

	public async Task<int> GetAllByUserId(int userId, bool? isDone)
	{
		if (isDone != null)
        {
            return await _queryable.CountAsync(x=>x.IsSended == isDone && x.UserId == userId);
        }
        else
        {
			return await _queryable.CountAsync(x => x.UserId == userId);
		}
	}

	public async Task<Basket?> GetBySessionIdOrUserId(int? userId, string? sessionId)
    {
        try
        {
            Basket? basket;
            if (userId != null)
            {
                basket = await _queryable.Include(x=>x.Carts).SingleOrDefaultAsync(x => x.UserId == userId && x.IsPaymentDone == false);
                if (basket != null)
                    return basket;
            }
            
            if (sessionId != null)
            {
                basket = await _queryable.Include(x => x.Carts).SingleOrDefaultAsync(x => x.SessionId == sessionId && x.IsPaymentDone == false);
                if (basket != null)
                    return basket;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }

    public async Task<Basket?> GetByUserId(int userId)
    {
        try
        {
            return await _queryable.Include(x => x.Carts).ThenInclude(i=>i.Product).Include(x => x.User).SingleOrDefaultAsync(x => x.UserId == userId && x.IsPaymentDone == false);
        }
        catch
        {
            return null;
        }
    }

	public PaginatedList<Basket> GetListByUserId(int userId, DefaultPaginationFilter filter)
	{
		try
		{
			var query = _queryable.Include(x => x.Carts).Include(x => x.User).AsNoTracking().Where(x=>x.UserId == userId).Skip((filter.Page - 1) * filter.PageSize)
						.Take(filter.PageSize)
						.ApplyFilter(filter).ApplySort(filter.SortBy);
			var dataTotalCount = _queryable.Count();
			return new PaginatedList<Basket>([.. query], dataTotalCount, filter.Page, filter.PageSize);
		}
		catch
		{
			return new PaginatedList<Basket>([], 0, filter.Page, filter.PageSize);
		}
	}

	public PaginatedList<Basket> GetPaginated(DefaultPaginationFilter filter)
    {
        try
        {
            var query = _queryable.Include(x => x.Carts).Include(x => x.User).Skip((filter.Page - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ApplyFilter(filter).ApplySort(filter.SortBy);
            var dataTotalCount = _queryable.Count();
            return new PaginatedList<Basket>([.. query], dataTotalCount, filter.Page, filter.PageSize);
        }
        catch
        {
            return new PaginatedList<Basket>([], 0, filter.Page, filter.PageSize);
        }
    }
}