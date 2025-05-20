namespace DataLayer;

public static class ProductCommentExtension
{
    public static IQueryable<ProductComment> ApplyFilter(this IQueryable<ProductComment> query, DefaultPaginationFilter filter)
    {

        if (!string.IsNullOrEmpty(filter.Keyword))
            query = query.Where(x => x.Comment.ToLower().Contains(filter.Keyword.ToLower().Trim()) || x.Slug.ToLower().Contains(filter.Keyword.ToLower().Trim())
            || x.Name.ToLower().Contains(filter.Keyword.ToLower().Trim()) || x.Email.ToLower().Contains(filter.Keyword.ToLower().Trim()));

        if (filter.BoolFilter != null)
            query = query.Where(x => x.IsActive == filter.BoolFilter);

        return query;
    }


    public static IQueryable<ProductComment> ApplySort(this IQueryable<ProductComment> query, SortByEnum? sortBy)
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
