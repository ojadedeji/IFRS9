using Fintrak.Shared.Common.Services.QueryService;
using System.Collections.Generic;

namespace Fintrak.Shared.Common.Services
{
    public class ResultData<T>
    {
        public ResultData(List<T> results, QueryOptions queryOptions)
        {
            Results = results;
            QueryOptions = queryOptions;
        }

        public QueryOptions QueryOptions { get; set; }
        public List<T> Results { get; set; }
    }
}
