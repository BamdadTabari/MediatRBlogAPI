namespace DataLayer;
public static class UserExtension
{
    public static IQueryable<User> ApplyFilter(this IQueryable<User> query, DefaultPaginationFilter filter)
    {

        if (!string.IsNullOrEmpty(filter.Keyword))
            query = query.Where(x => x.Email.ToString().ToLower().Contains(filter.Keyword.ToLower().Trim()) || x.Slug.ToLower().Contains(filter.Keyword.ToLower().Trim())
            || x.Mobile.ToLower().Contains(filter.Keyword.ToLower().Trim()));

        return query;
    }


    public static IQueryable<User> ApplySort(this IQueryable<User> query, SortByEnum? sortBy)
    {
        return sortBy switch
        {
            SortByEnum.CreationDate => query.OrderBy(x => x.CreatedAt),
            SortByEnum.CreationDateDescending => query.OrderByDescending(x => x.CreatedAt),
            SortByEnum.UpdateDate => query.OrderBy(x => x.UpdatedAt),
            SortByEnum.UpdateDateDescending => query.OrderByDescending(x => x.UpdatedAt),
            _ => query.OrderByDescending(x => x.Id)
        };
    }
}