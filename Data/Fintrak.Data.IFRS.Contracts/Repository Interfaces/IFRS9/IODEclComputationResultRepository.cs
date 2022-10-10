using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IODEclComputationResultRepository : IDataRepository<ODEclComputationResult>
    {
        IEnumerable<ODEclComputationResult> GetODEclComputationResultBySearch(string searchParam, string path);
        IEnumerable<ODEclComputationResult> GetODEclComputationResults(int defaultCount, string path);
    }
}
