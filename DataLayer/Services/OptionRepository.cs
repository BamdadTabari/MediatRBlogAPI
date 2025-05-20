using Microsoft.EntityFrameworkCore;

namespace DataLayer;
public interface IOptionRepository : IRepository<Option>
{
	Task<Option?> Get(string slug);
	Task<Option?> Get(int id);
	Task<Option?> GetByKey(string optionKey);
	PaginatedList<Option> GetPaginated(DefaultPaginationFilter filter);
	Task<List<Option>> GetAll();
}
public class OptionRepository : Repository<Option>, IOptionRepository
{
	private readonly IQueryable<Option> _queryable;


	public OptionRepository(ApplicationDbContext context) : base(context)
	{
		_queryable = DbContext.Set<Option>();
	}

	public async Task<Option?> Get(string slug)
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

	public async Task<Option?> Get(int id)
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

	public async Task<List<Option>> GetAll()
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

	public async Task<Option?> GetByKey(string optionKey)
	{
		try
		{
			return await _queryable.SingleOrDefaultAsync(x => x.OptionKey == optionKey);
		}
		catch
		{
			return null;
		}
	}

	public PaginatedList<Option> GetPaginated(DefaultPaginationFilter filter)
	{
		try
		{
			var query = _queryable.AsNoTracking().Skip((filter.Page - 1) * filter.PageSize)
						.Take(filter.PageSize)
						.ApplyFilter(filter).ApplySort(filter.SortBy);
			var dataTotalCount = _queryable.Count();
			return new PaginatedList<Option>([.. query], dataTotalCount, filter.Page, filter.PageSize);
		}
		catch
		{
			return new PaginatedList<Option>([], 0, filter.Page, filter.PageSize);
		}
	}
}