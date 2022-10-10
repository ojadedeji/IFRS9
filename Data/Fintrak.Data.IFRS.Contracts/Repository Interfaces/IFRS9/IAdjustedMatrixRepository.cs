using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IAdjustedMatrixRepository : IDataRepository<AdjustedMatrix>
    {
        IEnumerable<AdjustedMatrix> GetAvailableAdjustedMatrix(int defaultCount, string path);
        IEnumerable<AdjustedMatrix> GetAdjustedMatrixBySearch(string searchParam, string path);
        IEnumerable<string> GetDistinctMat_level();
        IEnumerable<AdjustedMatrix> GetAdjustedMatrixByMat_level(string Mat_levelVal);


    }
}
