using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface ITrialBalanceRepository : IDataRepository<TrialBalance>
    {
        IEnumerable<TrialBalance> GetTrialBalances(DateTime runDate);
        IEnumerable<TrialBalance> GetTrialBalancesByBranch(DateTime runDate, string branchCode);
        IEnumerable<TrialBalance> ExportTrialBalance(int defaultCount, string path);
    }
}
