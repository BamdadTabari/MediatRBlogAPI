using Microsoft.EntityFrameworkCore;
namespace DataLayer;
public interface IBlogCategoryRepository : IRepository<BlogCategory>
{
    Task<BlogCategory?> Get(string slug);
    Task<BlogCategory?> Get(int id);
    PaginatedList<BlogCategory> GetPaginated(DefaultPaginationFilter filter);
    Task<List<BlogCategory>> GetAll();
}
public class BlogCategoryRepository : Repository<BlogCategory>, IBlogCategoryRepository
{
    private readonly IQueryable<BlogCategory> _queryable;


    public BlogCategoryRepository(ApplicationDbContext context) : base(context)
    {
        _queryable = DbContext.Set<BlogCategory>();
    }

    public async Task<BlogCategory?> Get(string slug)
    {
        try
        {
            return await _queryable.Include(x => x.Blogs).SingleOrDefaultAsync(x => x.Slug == slug);
        }
        catch
        {
            return null;
        }
    }

    public async Task<BlogCategory?> Get(int id)
    {
        try
        {
            return await _queryable.Include(x => x.Blogs).SingleOrDefaultAsync(x => x.Id == id);
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<BlogCategory>> GetAll()
    {
        try
        {
            return await _queryable.Include(x => x.Blogs).AsNoTracking().ToListAsync();
        }
        catch
        {
            return [];
        }
    }

    public PaginatedList<BlogCategory> GetPaginated(DefaultPaginationFilter filter)
    {
        try
        {
            var query = _queryable.AsNoTracking().Skip((filter.Page - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ApplyFilter(filter).ApplySort(filter.SortBy);
            var dataTotalCount = _queryable.Count();
            return new PaginatedList<BlogCategory>([.. query], dataTotalCount, filter.Page, filter.PageSize);
        }
        catch
        {
            return new PaginatedList<BlogCategory>([], 0, filter.Page, filter.PageSize);
        }
    }
}