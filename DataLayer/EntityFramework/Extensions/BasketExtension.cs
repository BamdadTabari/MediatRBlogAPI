namespace DataLayer;
public static class BasketExtension
{
    public static IQueryable<Basket> ApplyFilter(this IQueryable<Basket> query, DefaultPaginationFilter filter)
    {

        if (!string.IsNullOrEmpty(filter.Keyword))
            query = query.Where(x => x.SessionId.ToLower().Contains(filter.Keyword.ToLower().Trim()) || x.Slug.ToLower().Contains(filter.Keyword.ToLower().Trim()));


        if(filter.BoolFilter != null)
            query = query.Where(x=>x.IsPaymentDone == filter.BoolFilter);

		if (filter.BoolFilter2 != null)
			query = query.Where(x => x.IsSended == filter.BoolFilter2);

		return query;
    }


    public static IQueryable<Basket> ApplySort(this IQueryable<Basket> query, SortByEnum? sortBy)
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