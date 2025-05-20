namespace DataLayer;
public static class NewsletterExtension
{
    public static IQueryable<Newsletter> ApplyFilter(this IQueryable<Newsletter> query, DefaultPaginationFilter filter)
    {

        if (!string.IsNullOrEmpty(filter.Keyword))
            query = query.Where(x => x.PhoneNumber.ToLower().Contains(filter.Keyword.ToLower().Trim()) || x.Slug.ToLower().Contains(filter.Keyword.ToLower().Trim()));

        return query;
    }


    public static IQueryable<Newsletter> ApplySort(this IQueryable<Newsletter> query, SortByEnum? sortBy)
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