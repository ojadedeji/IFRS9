using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface ICollateralGrowthRateRepository : IDataRepository<CollateralGrowthRate>{
        IEnumerable<CollateralGrowthRate> GetCollateralGrowthRateBySearch(string searchParam);
        IEnumerable<CollateralGrowthRate> GetCollateralGrowthRates(int defaultCount);
    }
}
