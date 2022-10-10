using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IIfrsAccessLGDDiscountFactorOutputRepository : IDataRepository<IfrsAccessLGDDiscountFactorOutput>
    {
        IEnumerable<IfrsAccessLGDDiscountFactorOutput> GetIfrsAccessLGDDiscountFactorOutputBySearch(string searchParam, string path);
        IEnumerable<IfrsAccessLGDDiscountFactorOutput> GetIfrsAccessLGDDiscountFactorOutputs(int defaultCount);
        IEnumerable<IfrsAccessLGDDiscountFactorOutput> ExportIfrsAccessLGDDiscountFactorOutput(int defaultCount, string path);
    }
}
