namespace DataLayer;
public static class OrderExtension
{
    public static IQueryable<Order> ApplyFilter(this IQueryable<Order> query, DefaultPaginationFilter filter)
    {

        if (!string.IsNullOrEmpty(filter.Keyword))
            query = query.Where(x => x.Amount.ToString().ToLower().Contains(filter.Keyword.ToLower().Trim()) || x.tracenumber.ToString().ToLower().Contains(filter.Keyword.ToLower().Trim())
            || x.Slug.ToString().ToLower().Contains(filter.Keyword.ToLower().Trim()));

        return query;
    }


    public static IQueryable<Order> ApplySort(this IQueryable<Order> query, SortByEnum? sortBy)
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