using Fintrak.Shared.CDQM.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.CDQM.Contracts
{
    public interface ICDQMCustomerPersistentRepository : IDataRepository<CDQMCustomerPersistent>
    {
        IEnumerable<CDQMCustomerPersistent> GetCustomerPersistents(string groupId);
        List<CDQMCustomerPersistent> GetCustomerPersistentByGroupId(string groupId);
    }
}
