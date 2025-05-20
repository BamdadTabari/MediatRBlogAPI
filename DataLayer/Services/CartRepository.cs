using Microsoft.EntityFrameworkCore;

namespace DataLayer;
public interface ICartRepository : IRepository<Cart>
{
    Task<Cart?> Get(string slug);
    Task<Cart?> Get(int id);
    PaginatedList<Cart> GetPaginated(DefaultPaginationFilter filter);
    Task<List<Cart>> GetAll();
    Task<Cart> GetNoTracking(int id);
}
public class CartRepository : Repository<Cart>, ICartRepository
{
    private readonly IQueryable<Cart> _queryable;


    public CartRepository(ApplicationDbContext context) : base(context)
    {
        _queryable = DbContext.Set<Cart>();
    }

	public async Task<Cart> GetNoTracking(int id)
	{
		return await _queryable.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
	}
	public async Task<Cart?> Get(string slug)
    {
        try
        {
            return await _queryable.Include(x => x.Product).Include(x => x.Basket).SingleOrDefaultAsync(x => x.Slug == slug);
        }
        catch
        {
            return null;
        }
    }

    public async Task<Cart?> Get(int id)
    {
        try
        {
            return await _queryable.Include(x => x.Product).Include(x => x.Basket).SingleOrDefaultAsync(x => x.Id == id);
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<Cart>> GetAll()
    {
        try
        {
            return await _queryable.Include(x => x.Product).Include(x => x.Basket).AsNoTracking().ToListAsync();
        }
        catch
        {
            return [];
        }
    }

    public PaginatedList<Cart> GetPaginated(DefaultPaginationFilter filter)
    {
        try
        {
            var query = _queryable.Include(x => x.Product).Include(x => x.Basket).AsNoTracking().Skip((filter.Page - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ApplyFilter(filter).ApplySort(filter.SortBy);
            var dataTotalCount = _queryable.Count();
            return new PaginatedList<Cart>([.. query], dataTotalCount, filter.Page, filter.PageSize);
        }
        catch
        {
            return new PaginatedList<Cart>([], 0, filter.Page, filter.PageSize);
        }
    }
}