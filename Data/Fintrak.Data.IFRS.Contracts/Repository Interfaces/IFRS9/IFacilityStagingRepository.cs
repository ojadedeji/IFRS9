using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IFacilityStagingRepository : IDataRepository<FacilityStaging>
    {
        IEnumerable<FacilityStaging> GetEntityByParam(string searchParam);
        IEnumerable<FacilityStaging> GetAllFacilityStagings(int defaultCount, string path);
    }
}
