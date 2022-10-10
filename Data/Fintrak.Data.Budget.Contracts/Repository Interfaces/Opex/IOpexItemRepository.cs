using Fintrak.Shared.Budget.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.Budget.Contracts
{
    public interface IOpexItemRepository : IDataRepository<OpexItem>
    {
        IEnumerable<OpexItemInfo> GetOpexItems(string year, string reviewCode,string categoryCode);
        IEnumerable<OpexItemInfo> GetOpexItems(string year, string reviewCode);
    }
}
