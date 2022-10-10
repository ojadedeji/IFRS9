using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Core.Framework;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.Core.Contracts
{
    public interface IExtractionJobRepository : IDataRepository<ExtractionJob>
    {
        IEnumerable<ExtractionJob> GetExtractionJobByRunDate(DateTime startDate,DateTime endDate);

        void ClearExtractionHistory(int solutionId);
    }
}
