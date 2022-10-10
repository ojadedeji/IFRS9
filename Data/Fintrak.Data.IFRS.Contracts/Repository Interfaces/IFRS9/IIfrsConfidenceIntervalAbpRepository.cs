using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IIfrsConfidenceIntervalAbpRepository : IDataRepository<IfrsConfidenceIntervalAbp>
    {
        IEnumerable<IfrsConfidenceIntervalAbp> GetIfrsConfidenceIntervalAbpBySearch(string searchParam, string path);
        IEnumerable<IfrsConfidenceIntervalAbp> GetIfrsConfidenceIntervalAbps(int defaultCount);
        IEnumerable<IfrsConfidenceIntervalAbp> ExportIfrsConfidenceIntervalAbp(int defaultCount, string path);
        IEnumerable<double> GetIfrsDistinctCiLevel();
    }
}
