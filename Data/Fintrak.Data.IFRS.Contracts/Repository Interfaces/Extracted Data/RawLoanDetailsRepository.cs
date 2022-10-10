using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IRawLoanDetailsRepository : IDataRepository<RawLoanDetails>
    {
        IEnumerable<RawLoanDetails> GetLoanDetailsBySearch(string searchParam);
        IEnumerable<RawLoanDetails> GetLoanDetails(int defaultCount,string path);
        string[] GetDistinctLoanDetailsRefNos(int count);
    }
}
