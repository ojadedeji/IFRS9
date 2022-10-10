using Fintrak.Shared.Budget.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.Budget.Contracts
{
    public interface IFeeItemRepository : IDataRepository<FeeItem>
    {
        IEnumerable<FeeItemInfo> GetFeeItems(string year, string reviewCode);
        IEnumerable<FeeItemInfo> GetFeeItems(string year, string reviewCode,string categoryCode);
    }
}
