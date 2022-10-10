using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IIfrsHistoricalMEVAbpRepository : IDataRepository<IfrsHistoricalMEVAbp>
    {
        IEnumerable<IfrsHistoricalMEVAbp> GetIfrsHistoricalMEVAbpBySearch(string searchParam, string path);
        IEnumerable<IfrsHistoricalMEVAbp> GetIfrsHistoricalMEVAbps(int defaultCount);
        IEnumerable<IfrsHistoricalMEVAbp> ExportIfrsHistoricalMEVAbp(int defaultCount, string path);
    }
}
