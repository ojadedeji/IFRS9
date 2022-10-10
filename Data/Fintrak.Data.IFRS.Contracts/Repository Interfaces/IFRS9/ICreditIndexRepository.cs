using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface ICreditIndexRepository : IDataRepository<CreditIndex>
    {
        IEnumerable<CreditIndex> GetAvailableCreditIndex(int defaultCount, string path);
        IEnumerable<CreditIndex> GetCreditIndexBySearch(string searchParam, string path);
        IEnumerable<DateTime> GetDistinctForcast();
        IEnumerable<CreditIndex> GetCreditIndexByForcast(int ForcastVal);


    }
}
