using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IBondsECLComputationResultRepository : IDataRepository<BondsECLComputationResult>
    {
        IEnumerable<BondsECLComputationResult> GetBondsECLComputationResultBySearch(string searchParam, string path);
        IEnumerable<BondsECLComputationResult> GetBondsECLComputationResults(int defaultCount, string path);
    }
}
