using Microsoft.EntityFrameworkCore;

namespace DataLayer;
public interface IProductCategoryRepository : IRepository<ProductCategory>
{
    Task<ProductCategory?> Get(string slug);
    Task<ProductCategory?> Get(int id);
    PaginatedList<ProductCategory> GetPaginated(DefaultPaginationFilter filter);
    Task<List<ProductCategory>> GetAll();
    Task<List<ProductCategory>> GetAllParents(int categoryId);
    Task<List<ProductCategory>> GetAllForProduct();
    Task<List<ProductCategory>> GetLatests(int take);
}
public class ProductCategoryRepository : Repository<ProductCategory>, IProductCategoryRepository
{
    private readonly IQueryable<ProductCategory> _queryable;


    public ProductCategoryRepository(ApplicationDbContext context) : base(context)
    {
        _queryable = DbContext.Set<ProductCategory>();
    }

    public async Task<ProductCategory?> Get(string slug)
    {
        try
        {
            return await _queryable.Include(x => x.Parent).Include(x => x.Children).Include(x => x.ProductCategoryProducts).ThenInclude(x => x.Product)
                .SingleOrDefaultAsync(x => x.Slug == slug);
        }
        catch
        {
            return null;
        }
    }

    public async Task<ProductCategory?> Get(int id)
    {
        try
        {
            return await _queryable.Include(x => x.Parent).Include(x => x.Children).Include(x => x.ProductCategoryProducts).ThenInclude(x => x.Product)
                .SingleOrDefaultAsync(x => x.Id == id);
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<ProductCategory>> GetAll()
    {
        try
        {
            return await _queryable.Include(x => x.Parent).Include(x => x.Children).Include(x => x.ProductCategoryProducts).ThenInclude(x => x.Product)
                .ToListAsync();
        }
        catch
        {
            return [];
        }
    }

    public async Task<List<ProductCategory>> GetAllForProduct()
    {
        try
        {
            return await _queryable.Include(x => x.Parent).Include(x => x.Children).Include(x => x.ProductCategoryProducts).ThenInclude(x => x.Product)
                .AsNoTracking().Where(x => x.Children == null || x.Children.Count() == 0).ToListAsync();
        }
        catch
        {
            return [];
        }
    }

    public async Task<List<ProductCategory>> GetAllParents(int categoryId)
    {
        try
        {
            return await _queryable.Include(x=>x.Children).Where(x=> x.Children.Select(y=>y.Id).Contains(categoryId))
                .AsNoTracking().ToListAsync();
        }
        catch
        {
            return [];
        }
    }

    public async Task<List<ProductCategory>> GetLatests(int take)
    {
        try
        {
            return await _queryable.AsNoTracking()
				.OrderByDescending(x => x.Id).Take(take).ToListAsync();
        }
        catch
        {
            return [];
        }
    }

    public PaginatedList<ProductCategory> GetPaginated(DefaultPaginationFilter filter)
    {
        try
        {
            var query = _queryable.Include(x => x.Parent).Include(x => x.Children).Include(x => x.ProductCategoryProducts).ThenInclude(x => x.Product)
                        .AsNoTracking().Skip((filter.Page - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ApplyFilter(filter).ApplySort(filter.SortBy);
            var dataTotalCount = _queryable.Count();
            return new PaginatedList<ProductCategory>([.. query], dataTotalCount, filter.Page, filter.PageSize);
        }
        catch
        {
            return new PaginatedList<ProductCategory>([], 0, filter.Page, filter.PageSize);
        }
    }
}