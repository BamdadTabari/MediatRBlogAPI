using Microsoft.EntityFrameworkCore;

namespace DataLayer;
public interface INewsletterRepository : IRepository<Newsletter>
{
    Task<Newsletter?> Get(string slug);
	Task<Newsletter?> Get(int id);
	PaginatedList<Newsletter> GetPaginated(DefaultPaginationFilter filter);
    Task<List<Newsletter>> GetAll();
}
public class NewsletterRepository : Repository<Newsletter>, INewsletterRepository
{
    private readonly IQueryable<Newsletter> _queryable;


    public NewsletterRepository(ApplicationDbContext context) : base(context)
    {
        _queryable = DbContext.Set<Newsletter>();
    }

    public async Task<Newsletter?> Get(string slug)
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

	public async Task<Newsletter?> Get(int id)
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

	public async Task<List<Newsletter>> GetAll()
    {
        try
        {
            return await _queryable.AsNoTracking().ToListAsync();
        }
        catch
        {
            return [];
        }
    }

    public PaginatedList<Newsletter> GetPaginated(DefaultPaginationFilter filter)
    {
        try
        {
            var query = _queryable.AsNoTracking().Skip((filter.Page - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ApplyFilter(filter).ApplySort(filter.SortBy);
            var dataTotalCount = _queryable.Count();
            return new PaginatedList<Newsletter>([.. query], dataTotalCount, filter.Page, filter.PageSize);
        }
        catch
        {
            return new PaginatedList<Newsletter>([], 0, filter.Page, filter.PageSize);
        }
    }
}