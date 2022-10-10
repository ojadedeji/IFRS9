using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IOBExposureRepository : IDataRepository<OBExposure>
    {
        IEnumerable<OBExposure> GetOBExposureBySearch(int flag,string searchParam);
        IEnumerable<OBExposure> GetOBExposure(int flag,int defaultCount, string path);
    }
}
