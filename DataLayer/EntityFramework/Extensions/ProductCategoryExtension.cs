namespace DataLayer;

public static class ProductCategoryExtension
{
    public static IQueryable<ProductCategory> ApplyFilter(this IQueryable<ProductCategory> query, DefaultPaginationFilter filter)
    {

        if (!string.IsNullOrEmpty(filter.Keyword))
            query = query.Where(x => x.Title.ToLower().Contains(filter.Keyword.ToLower().Trim()) || x.Slug.ToLower().Contains(filter.Keyword.ToLower().Trim())
            || x.Description.ToLower().Contains(filter.Keyword.ToLower().Trim()));

        return query;
    }


    public static IQueryable<ProductCategory> ApplySort(this IQueryable<ProductCategory> query, SortByEnum? sortBy)
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
