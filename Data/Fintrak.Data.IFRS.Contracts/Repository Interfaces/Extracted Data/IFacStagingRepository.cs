using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IFacStagingRepository : IDataRepository<FacStaging>
    {
        IEnumerable<FacStaging> GetFacStagingBySearch(string searchParam);
        IEnumerable<FacStaging> GetFacStaging(int defaultCount, string path);
    }
}
