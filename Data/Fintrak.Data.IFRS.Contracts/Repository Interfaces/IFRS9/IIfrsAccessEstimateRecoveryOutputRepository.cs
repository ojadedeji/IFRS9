using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IIfrsAccessEstimateRecoveryOutputRepository : IDataRepository<IfrsAccessEstimateRecoveryOutput>
    {
        IEnumerable<IfrsAccessEstimateRecoveryOutput> GetIfrsAccessEstimateRecoveryOutputBySearch(string searchParam, string path);
        IEnumerable<IfrsAccessEstimateRecoveryOutput> GetIfrsAccessEstimateRecoveryOutputs(int defaultCount);
        IEnumerable<IfrsAccessEstimateRecoveryOutput> ExportIfrsAccessEstimateRecoveryOutput(int defaultCount, string path);
    }
}
