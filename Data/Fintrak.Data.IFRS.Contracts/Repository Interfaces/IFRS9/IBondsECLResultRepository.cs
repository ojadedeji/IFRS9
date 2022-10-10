using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IBondsECLResultRepository : IDataRepository<BondsECLResult>{
        IEnumerable<BondsECLResult> GetBondsECLResultBySearch(string searchParam);
        IEnumerable<BondsECLResult> GetBondsECLResults(int defaultCount, string path);
    }
}
