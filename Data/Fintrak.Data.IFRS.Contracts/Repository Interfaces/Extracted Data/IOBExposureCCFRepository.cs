using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IOBExposureCCFRepository : IDataRepository<OBExposureCCF>
    {
        IEnumerable<OBExposureCCF> GetOBExposureCCFBySearch(int flag,string searchParam);
        IEnumerable<OBExposureCCF> GetOBExposureCCF(int flag,int defaultCount, string path);
    }
}
