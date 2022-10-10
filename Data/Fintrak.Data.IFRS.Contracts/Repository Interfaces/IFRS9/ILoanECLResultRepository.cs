using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface ILoanECLResultRepository : IDataRepository<LoanECLResult>{
        IEnumerable<LoanECLResult> GetLoanECLResultBySearch(string searchParam);
        IEnumerable<LoanECLResult> GetLoanECLResults(int defaultCount, string path);
    }
}
