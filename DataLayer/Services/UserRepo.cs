using Microsoft.EntityFrameworkCore;

namespace DataLayer;
public interface IUserRepo : IRepository<User>
{
    Task<User?> GetUser(string usernameOrEmail);
    Task<User?> Get(string slug);
    Task<User?> GetUserByPhone(string phone);
    Task<User?> GetUser(int Id);
    Task<bool> AnyExistUserName(string username);
    Task<bool> AnyExistEmail(string email);
    PaginatedList<User> GetPaginated(DefaultPaginationFilter filter);
}
public class UserRepo : Repository<User>, IUserRepo
{
    private readonly IQueryable<User> _queryable;


    public UserRepo(ApplicationDbContext context) : base(context)
    {
        _queryable = DbContext.Set<User>();
    }

    /// <summary>
    /// Check if user exist, in case not exist return false, in case query error return false, in case exist return true
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public async Task<bool> AnyExistEmail(string email)
    {
        try
        {
            return await _queryable.AnyAsync(x => x.Email == email);
        }
        catch
        {
            return await Task.FromResult(false);
        }
    }

    /// <summary>
    /// Check if user exist, in case not exist return false, in case query error return false, in case exist return true
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    public async Task<bool> AnyExistUserName(string username)
    {
        try
        {
            return await _queryable.AnyAsync(x => x.Username == username);
        }
        catch
        {
            return await Task.FromResult(false);
        }
    }

    public async Task<User?> Get(string slug)
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

    public PaginatedList<User> GetPaginated(DefaultPaginationFilter filter)
    {
        try
        {
            var query = _queryable.Skip((filter.Page - 1) * filter.PageSize)
                        .Take(filter.PageSize).AsNoTracking().ApplyFilter(filter).ApplySort(filter.SortBy);
            var dataTotalCount = _queryable.Count();
            return new PaginatedList<User>([.. query], dataTotalCount, filter.Page, filter.PageSize);
        }
        catch
        {
            return new PaginatedList<User>([], 0, filter.Page, filter.PageSize);
        }
    }

    /// <summary>
    /// Get User by username Or Email, in case not exist return new User, in query error return new User
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    public async Task<User?> GetUser(string usernameOrEmail)
    {
        try
        {
            return await _queryable.FirstOrDefaultAsync(x => x.Username == usernameOrEmail || x.Email == usernameOrEmail);
        }
        catch
        {
            return null;
        }
    }

    public async Task<User?> GetUser(int Id)
    {
        try
        {
            return await _queryable.SingleOrDefaultAsync(x => x.Id == Id);
        }
        catch
        {
            return null;
        }
    }

    public async Task<User?> GetUserByPhone(string phone)
    {
        try
        {
            return await _queryable.FirstOrDefaultAsync(x => x.Mobile == phone);
        }
        catch
        {
            return null;
        }
    }
}
