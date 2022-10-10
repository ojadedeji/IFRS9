using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface ICollateralDetailsRepository : IDataRepository<CollateralDetails>
    {
        IEnumerable<CollateralDetails> GetCollateralDetailsBySearch(string searchParam);
        IEnumerable<CollateralDetails> GetCollateralDetails(int defaultCount, string path);
    }
}
