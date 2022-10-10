using Fintrak.Shared.Basic.Entities;
using Fintrak.Shared.Basic.Framework;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.Basic.Contracts
{
    public interface IRevenueBudgetOfficerRepository : IDataRepository<RevenueBudgetOfficer>
    {
        IEnumerable<RevenueBudgetOfficer> GetRevenueBudgetOfficers(string year);
    }
}
