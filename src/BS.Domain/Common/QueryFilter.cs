namespace BS.Domain.Common;

public enum SortDirection
{
    ASC = 1,
    DESC = 2,
}

public class QueryFilter
{
    public string SearchText { get; set; }

    public string SortColumn { get; set; }

    public SortDirection SortDirection { get; set; } = SortDirection.DESC;

    /// <summary>
    /// NOTE: PAGE NUMBER DEFAULT SET TO 0. PASS PAGE NUMBER EXPLICITLY FOR PAGING ELSE NO PAGING
    /// </summary>
    public int PageNumber { get; set; } = 0;

    public int PageSize { get; set; } = 10;

    public int RecordCount { get; set; }

    public int PageCount
    {
        get
        {
            return (RecordCount / PageSize) + (RecordCount % PageSize == 0 ? 0 : 1);
        }
    }
}
