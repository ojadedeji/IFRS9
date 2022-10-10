using Fintrak.Shared.MPR.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.MPR.Contracts
{
    public interface IBalanceSheetBudgetOfficerRepository : IDataRepository<BalanceSheetBudgetOfficer>
    {
        IEnumerable<BalanceSheetBudgetOfficer> GetBalanceSheetBudgetOfficers(string year);
        List<BalanceSheetBudgetOfficer> GetBalanceSheetBySearch(string searchType);
    }
}
