using Fintrak.Shared.MPR.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.MPR.Contracts
{
    public interface IMPRBalanceSheetAdjustmentRepository : IDataRepository<MPRBalanceSheetAdjustment>
    {
        List<MPRBalanceSheetAdjustment> GetBalanceSheetAdjustmentBySearch(string searchType, string searchValue, int number);
        List<MPRBalanceSheetAdjustment> GetCodebyUsers(string username);
        List<MPRBalanceSheetAdjustment> GetBalanceSheetAdjustmentByCode(string code, string userName);
    }
}
