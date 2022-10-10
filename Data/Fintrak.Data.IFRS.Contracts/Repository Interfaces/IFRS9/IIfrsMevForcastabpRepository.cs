using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;

namespace Fintrak.Data.IFRS.Contracts
{
    public interface IIfrsMevForcastabpRepository : IDataRepository<IfrsMevForcastabp>
    {
        IEnumerable<IfrsMevForcastabp> GetAvailableIfrsMevForcastabp(int defaultCount, string path);
        IEnumerable<IfrsMevForcastabp> GetIfrsMevForcastabpBySearch(string searchParam, string path);
        IEnumerable<string> GetDistinctProduct();
        IEnumerable<IfrsMevForcastabp> GetIfrsMevForcastabpByProduct(string ProductVal);
    }
}
