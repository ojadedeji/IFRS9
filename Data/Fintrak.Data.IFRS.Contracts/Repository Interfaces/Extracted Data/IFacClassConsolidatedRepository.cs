using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IFacClassConsolidatedRepository : IDataRepository<FacClassConsolidated>
    {
        IEnumerable<FacClassConsolidated> GetFacClassConsolidatedBySearch(string searchParam);
        IEnumerable<FacClassConsolidated> GetFacClassConsolidated(int defaultCount, string path);
    }
}
