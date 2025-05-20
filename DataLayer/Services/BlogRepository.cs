using Microsoft.EntityFrameworkCore;

namespace DataLayer;

public interface IBlogRepository : IRepository<Blog>
{
    Task<Blog?> Get(string slug);
    Task<Blog?> Get(int id);
    PaginatedList<Blog> GetPaginated(DefaultPaginationFilter filter);
	Task<List<Blog>> GetBlogs(int count);
	Task<List<Blog>> GetAll();
    Task<List<Blog>> GetLatestBlogs(int take);
}
public class BlogRepository : Repository<Blog>, IBlogRepository
{
    private readonly IQueryable<Blog> _queryable;


    public BlogRepository(ApplicationDbContext context) : base(context)
    {
        _queryable = DbContext.Set<Blog>();
    }

    public async Task<Blog?> Get(string slug)
    {
        try
        {
            return await _queryable.Include(x => x.BlogCategory).SingleOrDefaultAsync(x => x.Slug == slug);
        }
        catch
        {
            return null;
        }
    }

    public async Task<Blog?> Get(int id)
    {
        try
        {
            return await _queryable.Include(x => x.BlogCategory).SingleOrDefaultAsync(x => x.Id == id);
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<Blog>> GetAll()
    {
        try
        {
            return await _queryable.Include(x => x.BlogCategory).AsNoTracking().ToListAsync();
        }
        catch
        {
            return [];
        }
    }

	public async Task<List<Blog>> GetBlogs(int count)
	{
		return await _queryable.AsNoTracking().Include(x=>x.BlogCategory).Where(x => x.ShowBlog).Skip(0).Take(count).ToListAsync();
	}

	public async Task<List<Blog>> GetLatestBlogs(int take)
    {
        try
        {
            return await _queryable.AsNoTracking().Where(x=>x.ShowBlog).Skip(0).Take(take).ToListAsync();
        }
        catch
        {
            return [];
        }
    }

    public PaginatedList<Blog> GetPaginated(DefaultPaginationFilter filter)
    {
        try
        {
            var query = _queryable.Include(x => x.BlogCategory).AsNoTracking().Skip((filter.Page - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ApplyFilter(filter).ApplySort(filter.SortBy);
            var dataTotalCount = _queryable.Count();
            return new PaginatedList<Blog>([.. query], dataTotalCount, filter.Page, filter.PageSize);
        }
        catch
        {
            return new PaginatedList<Blog>([], 0, filter.Page, filter.PageSize);
        }
    }
}