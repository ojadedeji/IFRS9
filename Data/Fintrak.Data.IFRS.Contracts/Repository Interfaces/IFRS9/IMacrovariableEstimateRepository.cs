using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IMacrovariableEstimateRepository : IDataRepository<MacrovariableEstimate>
    {
        IEnumerable<MacrovariableEstimate> GetMacrovariableEstimateByCategory(string Category);
        IEnumerable<MacrovariableEstimate> ExportMacrovariableEstimate(int defaultCount, string path);
    }
}
