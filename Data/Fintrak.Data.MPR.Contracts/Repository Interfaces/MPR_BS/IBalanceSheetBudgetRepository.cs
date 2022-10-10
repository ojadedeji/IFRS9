using Fintrak.Shared.MPR.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.MPR.Contracts
{
    public interface IBalanceSheetBudgetRepository : IDataRepository<BalanceSheetBudget>
    {
        IEnumerable<BalanceSheetBudget> GetBalanceSheetBudgets(string year);
        List<BalanceSheetBudget> GetBalanceSheetBySearch(string searchType);
    }
}
