using System;

namespace Fintrak.Data.Services.QueryService
{
    public class QueryOptions
    {
        public QueryOptions () {
            Count = 0;
            CurrentPage = 1;
            PageSize = 20;
            PageCount = 0;
        }

        public int Count { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int PageSize { get; set; }

        public int PageCount { get; set; }
    }
}
