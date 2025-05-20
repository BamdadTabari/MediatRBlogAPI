using Microsoft.EntityFrameworkCore;

namespace DataLayer;
public interface IProductCommentRepository : IRepository<ProductComment>
{
    Task<ProductComment?> Get(string slug);
    Task<ProductComment?> Get(int id);
    PaginatedList<ProductComment> GetPaginated(DefaultPaginationFilter filter);
    Task<List<ProductComment>> GetAll();
}
public class ProductCommentRepository : Repository<ProductComment>, IProductCommentRepository
{
    private readonly IQueryable<ProductComment> _queryable;


    public ProductCommentRepository(ApplicationDbContext context) : base(context)
    {
        _queryable = DbContext.Set<ProductComment>();
    }

    public async Task<ProductComment?> Get(string slug)
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

    public async Task<ProductComment?> Get(int id)
    {
        try
        {
            return await _queryable.SingleOrDefaultAsync(x => x.Id == id);
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<ProductComment>> GetAll()
    {
        try
        {
            return await _queryable.AsNoTracking().AsNoTracking().ToListAsync();
        }
        catch
        {
            return [];
        }
    }

    public PaginatedList<ProductComment> GetPaginated(DefaultPaginationFilter filter)
    {
        try
        {
            var query = _queryable.AsNoTracking().Skip((filter.Page - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ApplyFilter(filter).ApplySort(filter.SortBy);
            var dataTotalCount = _queryable.Count();
            return new PaginatedList<ProductComment>([.. query], dataTotalCount, filter.Page, filter.PageSize);
        }
        catch
        {
            return new PaginatedList<ProductComment>([], 0, filter.Page, filter.PageSize);
        }
    }
}