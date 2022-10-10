using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IFacilityClassificationRepository : IDataRepository<FacilityClassification>
    {
        IEnumerable<FacilityClassification> GetFacilityClassificationBySearch(string type,string searchParam);
        IEnumerable<FacilityClassification> GetFacilityClassification(int defaultcount,string type);
    }
}
