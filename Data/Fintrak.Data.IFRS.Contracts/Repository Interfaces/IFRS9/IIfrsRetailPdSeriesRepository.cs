using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;
using Fintrak.Shared.Common.Services.QueryService;
using Fintrak.Shared.Common.Services;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IIfrsRetailPdSeriesRepository : IDataRepository<IfrsRetailPdSeries>
    {
        IEnumerable<IfrsRetailPdSeries> GetPaginatedEntities(QueryOptions queryOptions);
        UInt64 GetTotalRecordsCount(string tableName, string columnName, string searchParamS, Double? searchParamN);
    }
}
