using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface ILoanSignificantFlagRepository : IDataRepository<LoanSignificantFlag>
    {
        IEnumerable<LoanSignificantFlag> GetLoanSignificantFlagBySearch(string searchParam);
        IEnumerable<LoanSignificantFlag> GetLoanSignificantFlags(int defaultCount);
    }
}
