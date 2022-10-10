using System;

namespace Fintrak.Shared.Common.Services.QueryService
{
    public class QueryOptionsCalculator
    {
        public static int CalculateStart(QueryOptions queryOptions)
        {
            return (queryOptions.CurrentPage - 1) * queryOptions.PageSize;
        }
        public static UInt64 CalculateTotalPages(UInt64 count, int pageSize)
        {
            return (UInt64)Math.Ceiling((double)count / pageSize);
        }
    }
}
