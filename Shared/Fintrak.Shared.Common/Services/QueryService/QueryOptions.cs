using Fintrak.Shared.Common.Services.QueryService;
using System;

namespace Fintrak.Shared.Common.Services.QueryService
{
    public class QueryOptions
    {
        public QueryOptions () {
            CurrentPage = 1;
            PageSize = 20;
            init = true;
            FilterField = "1";
            FilterOption = "1";
            FilterFieldType = "number";
        }

        public bool init { get; set; }

        public UInt64? TotalRecords { get; set; }

        public int CurrentPage { get; set; }

        public UInt64? TotalPages { get; set; }

        public int PageSize { get; set; }

        public int DisplayedRows { get; set; }

        public string FilterOption { get; set; }

        public string FilterField { get; set; }

        public string FilterFieldType { get; set; }

        public string SortField { get; set; }

        public SortOrder SortOrder { get; set; }

        public string Sort
        {
            get
            {
                return string.Format("{0} {1}", SortField, SortOrder.ToString());
            }
        }
    }
}
