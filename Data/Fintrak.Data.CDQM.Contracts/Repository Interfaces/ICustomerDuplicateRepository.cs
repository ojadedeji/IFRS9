using System.Collections.Generic;
using Fintrak.Shared.CDQM.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Linq;

namespace Fintrak.Data.CDQM.Contracts
{
    public interface ICDQMCustomerDuplicateRepository : IDataRepository<CDQMCustomerDuplicate>
    {
        IEnumerable<string> GetCustomerGroupIDs();
        List<string> GetDistinctGroupIDs();
        CDQMCustomerDuplicate GetCustomerDuplicate(string customerId);
        IEnumerable<CDQMCustomerDuplicate> GetCustomerDuplicates(string groupId);
    }
}
