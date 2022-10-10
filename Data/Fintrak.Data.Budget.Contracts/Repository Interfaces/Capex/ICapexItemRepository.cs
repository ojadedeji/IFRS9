using Fintrak.Shared.Budget.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.Budget.Contracts
{
    public interface ICapexItemRepository : IDataRepository<CapexItem>
    {
        IEnumerable<CapexItemInfo> GetCapexItems(string year, string reviewCode,string categoryCode);
        IEnumerable<CapexItemInfo> GetCapexItems(string year, string reviewCode);
    }
}
