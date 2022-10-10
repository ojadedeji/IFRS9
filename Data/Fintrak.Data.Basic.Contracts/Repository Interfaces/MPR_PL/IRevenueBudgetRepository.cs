using Fintrak.Shared.Basic.Entities;
using Fintrak.Shared.Basic.Framework;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.Basic.Contracts
{
    public interface IRevenueBudgetRepository : IDataRepository<RevenueBudget>
    {
        IEnumerable<RevenueBudget> GetRevenueBudgets(string year);
    }
}
