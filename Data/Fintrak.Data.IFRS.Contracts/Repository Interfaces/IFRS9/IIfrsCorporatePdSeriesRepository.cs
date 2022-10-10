using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;
using Fintrak.Shared.Common.Services.QueryService;
using Fintrak.Shared.Common.Services;
using System.IO;

namespace Fintrak.Data.IFRS.Contracts
{
    public interface IIfrsCorporatePdSeriesRepository : IDataRepository<IfrsCorporatePdSeries>
    {
        IEnumerable<IfrsCorporatePdSeries> GetPaginatedEntities(QueryOptions queryOptions);
        string GetForExport(string Path);
        UInt64 GetTotalRecordsCount(string tableName, string columnName, string searchParamS, Double? searchParamN);
    }
}
