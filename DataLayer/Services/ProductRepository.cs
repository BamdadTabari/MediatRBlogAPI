using Microsoft.EntityFrameworkCore;

namespace DataLayer;

public interface IProductRepository : IRepository<Product>
{
    Task<Product?> Get(string slug);
    Task<Product?> Get(int id);
    PaginatedList<Product> GetPaginated(DefaultPaginationFilter filter);
    PaginatedList<Product> GetPaginated(DefaultPaginationFilter filter, int categoryId);
    Task<List<Product>> GetAll();
    Task<List<Product>> GetLatestOffs(int take);
    Task<List<Product>> GetMostSelled(int take);
    Task<List<Product>> GetMostNewProducts(int take);
}
public class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly IQueryable<Product> _queryable;


    public ProductRepository(ApplicationDbContext context) : base(context)
    {
        _queryable = DbContext.Set<Product>();
    }

    public async Task<Product?> Get(string slug)
    {
        try
        {
            return await _queryable.Include(x => x.ProductCategoryProducts).ThenInclude(x => x.ProductCategory)
                .Include(x => x.ProductPropertyValueProducts).ThenInclude(x => x.ProductPropertyValues).ThenInclude(x=>x.ProductProperty)
                .Include(x => x.ProductComments).Include(x=>x.Brand)
                .Where(x=>x.IsActive)
                .SingleOrDefaultAsync(x => x.Slug == slug);
        }
        catch
        {
            return null;
        }
    }

    public async Task<Product?> Get(int id)
    {
        try
        {
            return await _queryable.Include(x => x.ProductCategoryProducts).ThenInclude(x => x.ProductCategory)
                .Include(x => x.ProductPropertyValueProducts).ThenInclude(x => x.ProductPropertyValues).ThenInclude(x => x.ProductProperty)
                .Include(x => x.ProductComments)
                .SingleOrDefaultAsync(x => x.Id == id);
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<Product>> GetAll()
    {
        try
        {
            return await _queryable.Include(x => x.ProductCategoryProducts).ThenInclude(x => x.ProductCategory)
                .Include(x => x.ProductPropertyValueProducts).ThenInclude(x => x.ProductPropertyValues)
                .Include(x => x.ProductComments).AsNoTracking().ToListAsync();
        }
        catch
        {
            return [];
        }
    }

    public async Task<List<Product>> GetLatestOffs(int take)
    {
        try
        {
            return await _queryable.Where(x => x.FinalPrice != 0).AsNoTracking().Where(x => x.IsActive && x.Count > 0).OrderByDescending(x => x.Id).Take(take).ToListAsync();
        }
        catch
        {
            return [];
        }
    }

    public async Task<List<Product>> GetMostNewProducts(int take)
    {
        try
        {
            return await _queryable
				.AsNoTracking()
                .Include(x=>x.ProductCategoryProducts).ThenInclude(x=>x.ProductCategory)
				.Where(x => x.IsActive && x.Count > 0)
                .OrderByDescending(x => x.Id) // مرتب‌سازی نزولی بر اساس Id
                .Take(take)
                .ToListAsync();
        }
        catch
        {
            return [];
        }
    }

    public async Task<List<Product>> GetMostSelled(int take)
    {
        try
        {
            return await _queryable.Include(x=>x.ProductCategoryProducts).ThenInclude(x=>x.ProductCategory).AsNoTracking().Where(x => x.IsActive && x.Count > 0)
                .OrderByDescending(x => x.SellCount) // مرتب‌سازی نزولی بر اساس SellCount
                .Take(take) // گرفتن n مقدار اول که بیشترین فروش را دارند
                .ToListAsync();
        }
        catch
        {
            return [];
        }
    }

    public PaginatedList<Product> GetPaginated(DefaultPaginationFilter filter)
    {
        try
        {
            var query = _queryable.Include(x => x.ProductCategoryProducts).ThenInclude(x => x.ProductCategory)
                .Include(x => x.ProductPropertyValueProducts).ThenInclude(x => x.ProductPropertyValues)
                .Include(x => x.ProductComments).AsNoTracking().Skip((filter.Page - 1) * filter.PageSize)
                .Include(x=>x.Brand)
                        .Take(filter.PageSize)
                        .ApplyFilter(filter).ApplySort(filter.SortBy);
            var dataTotalCount = _queryable.Count();
            return new PaginatedList<Product>([.. query], dataTotalCount, filter.Page, filter.PageSize);
        }
        catch
        {
            return new PaginatedList<Product>([], 0, filter.Page, filter.PageSize);
        }
    }

    public PaginatedList<Product> GetPaginated(DefaultPaginationFilter filter, int categoryId)
    {
        try
        {

            if(categoryId == 0)
            {
                var query = _queryable.Include(x => x.ProductCategoryProducts).ThenInclude(x => x.ProductCategory)
               .Include(x => x.ProductPropertyValueProducts).ThenInclude(x => x.ProductPropertyValues)
               .Include(x => x.ProductComments).Skip((filter.Page - 1) * filter.PageSize)
                       .Take(filter.PageSize)
                       .ApplyFilter(filter).ApplySort(filter.SortBy);
                var dataTotalCount = _queryable.Count();
                return new PaginatedList<Product>([.. query], dataTotalCount, filter.Page, filter.PageSize);
            }
            else
            {
                var query = _queryable.Include(x => x.ProductCategoryProducts).ThenInclude(x => x.ProductCategory)
               .Include(x => x.ProductPropertyValueProducts).ThenInclude(x => x.ProductPropertyValues)
               .Include(x => x.ProductComments).Skip((filter.Page - 1) * filter.PageSize)
               .Where(x => x.ProductCategoryProducts.Any(pcp => pcp.ProductCategoryId == categoryId))
                       .Take(filter.PageSize)
                       .ApplyFilter(filter).ApplySort(filter.SortBy);
                var dataTotalCount = _queryable.Count();
                return new PaginatedList<Product>([.. query], dataTotalCount, filter.Page, filter.PageSize);
            }
        }
        catch
        {
            return new PaginatedList<Product>([], 0, filter.Page, filter.PageSize);
        }
    }
}