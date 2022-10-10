using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Core.Framework;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.Core.Contracts
{
    public interface IProcessJobRepository : IDataRepository<ProcessJob>
    {
        IEnumerable<ProcessJob> GetProcessJobByRunDate(DateTime startDate,DateTime endDate);
        void ClearProcessHistory(int solutionId);
    }
}
