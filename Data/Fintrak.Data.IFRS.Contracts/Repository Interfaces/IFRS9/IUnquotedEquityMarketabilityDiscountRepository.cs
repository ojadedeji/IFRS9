using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IUnquotedEquityMarketabilityDiscountRepository : IDataRepository<UnquotedEquityMarketabilityDiscount>
    {
        //IEnumerable<UnquotedEquityMarketabilityDiscount> GetUnquotedEquityMarketabilityDiscountBySearch(string searchParam, string path);
        IEnumerable<UnquotedEquityMarketabilityDiscount> GetUnquotedEquityMarketabilityDiscounts(int defaultCount);
        IEnumerable<UnquotedEquityMarketabilityDiscount> ExportUnquotedEquityMarketabilityDiscount(int defaultCount, string path);
    }
}
