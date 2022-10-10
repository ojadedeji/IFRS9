using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IComputedForcastedPDLGDRepository : IDataRepository<ComputedForcastedPDLGD>
    {

        List<ComputedForcastedPDLGD> GetComputedForcastedPDLGD();
        IEnumerable<ComputedForcastedPDLGD> GetAvailableComputedForcastedPDLGD(int defaultCount, string path);
    }
}
