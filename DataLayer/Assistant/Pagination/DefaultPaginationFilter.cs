namespace DataLayer;
public class DefaultPaginationFilter : PaginationFilter
{
    public DefaultPaginationFilter(int pageNumber = 1, int pageSize = 10) : base(pageNumber, pageSize) { }
    public DefaultPaginationFilter() { }

    public string? Keyword { get; set; }
    public bool? BoolFilter { get; set; }
    public SortByEnum SortBy { get; set; } = SortByEnum.CreationDate;
}