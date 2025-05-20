namespace DataLayer;

public static class BlogExtension
{
    public static IQueryable<Blog> ApplyFilter(this IQueryable<Blog> query, DefaultPaginationFilter filter)
    {

        if (!string.IsNullOrEmpty(filter.Keyword))
            query = query.Where(x => x.Name.ToLower().Contains(filter.Keyword.ToLower().Trim()) || x.Slug.ToLower().Contains(filter.Keyword.ToLower().Trim())
            || x.ShortDescription.ToLower().Contains(filter.Keyword.ToLower().Trim()));

        if (filter.BoolFilter != null)
            query = query.Where(x => x.ShowBlog == filter.BoolFilter);

        return query;
    }


    public static IQueryable<Blog> ApplySort(this IQueryable<Blog> query, SortByEnum? sortBy)
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
