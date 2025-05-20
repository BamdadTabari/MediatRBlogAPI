
namespace DataLayer;
public interface IUnitOfWork : IDisposable
{
    IBrandRepository BrandRepository { get; }
    IBlogCategoryRepository BlogCategoryRepository { get; }
    IBlogRepository BlogRepository { get; }
    INewsletterRepository NewsletterRepository { get; }
    IOrderRepository OrderRepository { get; }
    IProductCategoryRepository ProductCategoryRepository { get; }
    IProductRepository ProductRepository { get; }
    IProductPropertyProductRepository ProductPropertyProductRepository { get; }
    IProductPropertyRepository ProductPropertyRepository { get; }
    IProductPropertyValuesRepository ProductPropertyValuesRepository { get; }
    IProductCommentRepository ProductCommentRepository { get; }
    IUserWalletRepository UserWalletRepository { get; }
    IContactRepository ContactRepository { get; }
    IProductCategoryProductRepository ProductCategoryProductRepository { get; }
    IOtpRepository OtpRepository { get; }
    ITokenBlacklistRepository TokenBlacklistRepository { get; }
    IUserRepo UserRepository { get; }
    IUserRoleRepo UserRoleRepository { get; }
    IRoleRepo RoleRepository { get; }
    ICartRepository CartRepository { get; }
    IBasketRepository BasketRepository { get; }
    IBulkOrderRepository BulkOrderRepository { get; }
    IOptionRepository OptionRepository { get; }
    Task<bool> CommitAsync();
}

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        BrandRepository = new BrandRepository(_context);
        BlogCategoryRepository = new BlogCategoryRepository(_context);
        BlogRepository = new BlogRepository(_context);
        NewsletterRepository = new NewsletterRepository(_context);
        OrderRepository = new OrderRepository(_context);
        ProductCategoryRepository = new ProductCategoryRepository(_context);
        ProductRepository = new ProductRepository(_context);
        ProductPropertyProductRepository = new ProductPropertyProductRepository(_context);
        ProductPropertyRepository = new ProductPropertyRepository(_context);
        ProductPropertyValuesRepository = new ProductPropertyValuesRepository(_context);
        ProductCommentRepository = new ProductCommentRepository(_context);
        UserWalletRepository = new UserWalletRepository(_context);
        ContactRepository = new ContactRepository(_context);
        ProductCategoryProductRepository = new ProductCategoryProductRepository(_context);
        OtpRepository = new OtpRepository(_context);
        TokenBlacklistRepository = new TokenBlacklistRepo(_context);
        CartRepository = new CartRepository(_context);
        BulkOrderRepository = new BulkOrderRepository(_context);
        BasketRepository = new BasketRepository(_context);
        UserRepository = new UserRepo(_context);
        UserRoleRepository = new UserRoleRepo(_context);
        RoleRepository = new RoleRepo(context);
        OptionRepository = new OptionRepository(context);
    }

    public IBrandRepository BrandRepository { get; }
    public IBlogCategoryRepository BlogCategoryRepository { get; }
    public IBlogRepository BlogRepository { get; }
    public INewsletterRepository NewsletterRepository { get; }
    public IOrderRepository OrderRepository { get; }
    public IProductCategoryRepository ProductCategoryRepository { get; }
    public IProductRepository ProductRepository { get; }
    public IProductPropertyProductRepository ProductPropertyProductRepository { get; }
    public IProductPropertyRepository ProductPropertyRepository { get; }
    public IProductPropertyValuesRepository ProductPropertyValuesRepository { get; }
    public IProductCommentRepository ProductCommentRepository { get; }
    public IUserWalletRepository UserWalletRepository { get; }
    public IContactRepository ContactRepository { get; }
    public IProductCategoryProductRepository ProductCategoryProductRepository { get; }
    public IOtpRepository OtpRepository { get; }
    public ITokenBlacklistRepository TokenBlacklistRepository { get; }
    public IUserRepo UserRepository { get; }
    public IUserRoleRepo UserRoleRepository { get; }
    public IRoleRepo RoleRepository { get; }
    public ICartRepository CartRepository { get; }
    public IBasketRepository BasketRepository { get; }
    public IBulkOrderRepository BulkOrderRepository { get; }
	public IOptionRepository OptionRepository { get; set; }
	public async Task<bool> CommitAsync() => await _context.SaveChangesAsync() > 0;

    // dispose and add to garbage collector
    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}