namespace DataLayer;
public static class ProductExtension
{
    public static IQueryable<Product> ApplyFilter(this IQueryable<Product> query, DefaultPaginationFilter filter)
    {

        if (!string.IsNullOrEmpty(filter.Keyword))
            query = query.Where(x => x.Title.ToLower().Contains(filter.Keyword.ToLower().Trim()) || x.Slug.ToLower().Contains(filter.Keyword.ToLower().Trim())
            || x.Description.ToLower().Contains(filter.Keyword.ToLower().Trim()));

        if (filter.BoolFilter != null)
            query = query.Where(x => x.IsActive == filter.BoolFilter);

        if (filter.BoolFilter2 != null && filter.BoolFilter2 == true)
            query = query.Where(x => x.Count > 0);

        if (filter.Min != null)
            query = query.Where(x => x.Price >= filter.Min);
        if (filter.Max != null)
            query = query.Where(x => x.Price <= filter.Max);
        return query;
    }


    public static IQueryable<Product> ApplySort(this IQueryable<Product> query, SortByEnum? sortBy)
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
