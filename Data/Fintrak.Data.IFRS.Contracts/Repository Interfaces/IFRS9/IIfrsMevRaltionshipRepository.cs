using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IIfrsMevRaltionshipRepository : IDataRepository<IfrsMevRaltionship>
    {
        IEnumerable<IfrsMevRaltionship> GetIfrsMevRaltionshipBySearch(string searchParam, string path);
        IEnumerable<IfrsMevRaltionship> GetIfrsMevRaltionships(int defaultCount);
        IEnumerable<IfrsMevRaltionship> ExportIfrsMevRaltionship(int defaultCount, string path);
    }
}
