using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface ILGDComptResultRepository : IDataRepository<LGDComptResult>
    {
        IEnumerable<LGDComptResult> GetLGDComptResultBySearch(string searchParam);
        IEnumerable<LGDComptResult> GetLGDComptResults(int defaultCount, string path);
    }
}
