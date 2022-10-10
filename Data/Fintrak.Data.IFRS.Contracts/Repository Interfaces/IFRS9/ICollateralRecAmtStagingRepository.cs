using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface ICollateralRecAmtStagingRepository : IDataRepository<CollateralRecAmtStaging>{
        IEnumerable<CollateralRecAmtStaging> GetCollateralRecAmtStagingBySearch(string searchParam);
        IEnumerable<CollateralRecAmtStaging> GetCollateralRecAmtStagings(int defaultCount);
    }
}
