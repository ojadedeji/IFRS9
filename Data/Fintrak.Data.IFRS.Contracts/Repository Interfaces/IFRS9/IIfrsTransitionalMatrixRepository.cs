using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IIfrsTransitionalMatrixRepository : IDataRepository<IfrsTransitionalMatrix>
    {
        IEnumerable<IfrsTransitionalMatrix> GetIfrsTransitionalMatrixBySearch(string searchParam, string path);
        IEnumerable<IfrsTransitionalMatrix> GetIfrsTransitionalMatrixs(int defaultCount);
        IEnumerable<IfrsTransitionalMatrix> ExportIfrsTransitionalMatrix(int defaultCount, string path);
    }
}
