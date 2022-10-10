using Fintrak.Shared.Budget.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.Budget.Contracts
{
    public interface IDepreciationRateRepository : IDataRepository<DepreciationRate>
    {
        IEnumerable<DepreciationRateInfo> GetDepreciationRates(string year, string reviewCode, string categoryCode);
        IEnumerable<DepreciationRateInfo> GetDepreciationRates(string year, string reviewCode);
    }
}
