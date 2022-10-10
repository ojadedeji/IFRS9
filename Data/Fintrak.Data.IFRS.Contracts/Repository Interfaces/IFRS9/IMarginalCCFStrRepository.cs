using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IMarginalCCFStrRepository : IDataRepository<MarginalCCFStr>
    {
        IEnumerable<MarginalCCFStr> GetMarginalCCFStrBySearch(string searchParam);
        IEnumerable<MarginalCCFStr> GetMarginalCCFStrs(int defaultCount);
    }
}
