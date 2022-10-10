using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IMevlistingRepository : IDataRepository<Mevlisting>
    {
        IEnumerable<Mevlisting> GetAvailableMevlisting(int defaultCount, string path);
        IEnumerable<Mevlisting> GetMevlistingBySearch(string searchParam, string path);
        IEnumerable<string> GetDistinctMevs();
        IEnumerable<Mevlisting> GetMevlistingByMev(string mevVal);


    }
}
