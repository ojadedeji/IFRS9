using Fintrak.Shared.MPR.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.MPR.Contracts
{
    public interface ICustAccountRepository : IDataRepository<CustAccount>
    {
        List<CustAccount> GetCustomerAccountBySearch(string searchType, string searchValue, int number);
    }
}
