using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;

namespace Fintrak.Data.IFRS.Contracts
{
    public interface ITrialBalanceGapRepository : IDataRepository<TrialBalanceGap>
    {
        IEnumerable<TrialBalanceGap> GetTrialBalances(DateTime runDate);
        IEnumerable<TrialBalanceGap> GetGapTrialBalancesByBranch(DateTime runDate, string branchCode);
    }
}
