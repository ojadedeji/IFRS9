using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface ICummulativePDRepository : IDataRepository<CummulativePD>{
        IEnumerable<CummulativePD> GetCummulativePDBySearch(string searchParam);
        IEnumerable<CummulativePD> GetCummulativePDs(int defaultCount, string path);
    }
}
