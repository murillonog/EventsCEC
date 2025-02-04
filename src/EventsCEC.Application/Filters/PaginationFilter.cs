﻿namespace EventsCEC.Application.Filters;

public class PaginationFilter
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public PaginationFilter()
    {
        this.PageNumber = 1;
        this.PageSize = 10;
    }
    public PaginationFilter(int? pageNumber, int? pageSize)
    {
        this.PageNumber = pageNumber ?? 1;
        this.PageSize = pageSize ?? 10;
    }
}
