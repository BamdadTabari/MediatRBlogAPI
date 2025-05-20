using Microsoft.EntityFrameworkCore;

namespace DataLayer;
public interface IUserWalletRepository : IRepository<UserWallet>
{
    Task<UserWallet?> Get(string slug);
    Task<UserWallet?> Get(int userId);
    PaginatedList<UserWallet> GetPaginated(DefaultPaginationFilter filter);
    Task<List<UserWallet>> GetAll();
}
public class UserWalletRepository : Repository<UserWallet>, IUserWalletRepository
{
    private readonly IQueryable<UserWallet> _queryable;


    public UserWalletRepository(ApplicationDbContext context) : base(context)
    {
        _queryable = DbContext.Set<UserWallet>();
    }

    public async Task<UserWallet?> Get(string slug)
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

    public async Task<UserWallet?> Get(int userId)
    {
        try
        {
            return await _queryable.Include(x => x.User).SingleOrDefaultAsync(x => x.UserId == userId);
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<UserWallet>> GetAll()
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

    public PaginatedList<UserWallet> GetPaginated(DefaultPaginationFilter filter)
    {
        try
        {
            var query = _queryable.Include(x => x.User).AsNoTracking().Skip((filter.Page - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ApplyFilter(filter).ApplySort(filter.SortBy);
            var dataTotalCount = _queryable.Count();
            return new PaginatedList<UserWallet>([.. query], dataTotalCount, filter.Page, filter.PageSize);
        }
        catch
        {
            return new PaginatedList<UserWallet>([], 0, filter.Page, filter.PageSize);
        }
    }
}