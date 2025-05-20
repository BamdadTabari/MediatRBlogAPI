
namespace DataLayer;
public interface IUnitOfWork : IDisposable
{
    IBlogCategoryRepository BlogCategoryRepository { get; }
    IBlogRepository BlogRepository { get; }
    ITokenBlacklistRepository TokenBlacklistRepository { get; }
    IUserRepo UserRepository { get; }
    IUserRoleRepo UserRoleRepository { get; }
    IRoleRepo RoleRepository { get; }
    Task<bool> CommitAsync(CancellationToken cancellationToken);
}

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        BlogCategoryRepository = new BlogCategoryRepository(_context);
        BlogRepository = new BlogRepository(_context);
        TokenBlacklistRepository = new TokenBlacklistRepo(_context);
        UserRepository = new UserRepo(_context);
        UserRoleRepository = new UserRoleRepo(_context);
        RoleRepository = new RoleRepo(context);
    }

    public IBlogCategoryRepository BlogCategoryRepository { get; }
    public IBlogRepository BlogRepository { get; }
    public ITokenBlacklistRepository TokenBlacklistRepository { get; }
    public IUserRepo UserRepository { get; }
    public IUserRoleRepo UserRoleRepository { get; }
    public IRoleRepo RoleRepository { get; }
	public async Task<bool> CommitAsync(CancellationToken cancellationToken) => await _context.SaveChangesAsync(cancellationToken) > 0;

    // dispose and add to garbage collector
    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}