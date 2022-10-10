using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IIfrsProjectedCummDefaultFrqRepository : IDataRepository<IfrsProjectedCummDefaultFrq>
    {
        //IEnumerable<IfrsProjectedCummDefaultFrq> GetIfrsProjectedCummDefaultFrqBySource(string Source);
        IEnumerable<IfrsProjectedCummDefaultFrq> GetAllIfrsProjectedCummDefaultFrq(int defaultCount, string path);
    }
}
