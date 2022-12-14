using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface ICummulativeMatrixRepository : IDataRepository<CummulativeMatrix>
    {
        IEnumerable<CummulativeMatrix> GetAvailableCummulativeMatrix(int defaultCount, string path);
        IEnumerable<CummulativeMatrix> GetCummulativeMatrixBySearch(string searchParam, string path);
        IEnumerable<string> GetDistinctMatlevel();
        IEnumerable<CummulativeMatrix> GetCummulativeMatrixByMat_level(string Mat_levelVal);


    }
}
