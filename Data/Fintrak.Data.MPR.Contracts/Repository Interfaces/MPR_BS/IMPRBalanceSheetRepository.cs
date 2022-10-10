using Fintrak.Shared.MPR.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.MPR.Contracts
{
    public interface IMPRBalanceSheetRepository : IDataRepository<MPRBalanceSheet>
    {
        IEnumerable<MPRBalanceSheet> GetBalanceSheets(DateTime runDate, int number);
        //IEnumerable<MPRBalanceSheet> GetAllBalanceSheets(DateTime runDate, int number, DateTime fromDate, DateTime toDate);
        IEnumerable<MPRBalanceSheet> GetRunDate();

        IEnumerable<MPRBalanceSheet> GetAllBalanceSheets(string searchType, string searchValue, int number, DateTime fromDate);



    }
}
