namespace DataLayer;
public static class ContactExtension
{
    public static IQueryable<Contact> ApplyFilter(this IQueryable<Contact> query, DefaultPaginationFilter filter)
    {

        if (!string.IsNullOrEmpty(filter.Keyword))
            query = query.Where(x => x.FullName.ToLower().Contains(filter.Keyword.ToLower().Trim()) || x.Phone.ToLower().Contains(filter.Keyword.ToLower().Trim())
            || x.Email.ToLower().Contains(filter.Keyword.ToLower().Trim()) || x.Slug.ToLower().Contains(filter.Keyword.ToLower().Trim()));

        return query;
    }


    public static IQueryable<Contact> ApplySort(this IQueryable<Contact> query, SortByEnum? sortBy)
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