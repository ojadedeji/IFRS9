using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IMarginalPddSTRLBRepository : IDataRepository<MarginalPddSTRLB>
    {
        IEnumerable<MarginalPddSTRLB> GetMarginalPddSTRLBBySearch(string searchParam);
        IEnumerable<MarginalPddSTRLB> GetMarginalPddSTRLBs(int defaultCount, string path);
    }
}
