using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Core.Framework;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.Core.Contracts
{
    public interface IExtractionTriggerRepository : IDataRepository<ExtractionTrigger>
    {
        IEnumerable<ExtractionTriggerInfo> GetExtractionTriggers();
        IEnumerable<ExtractionTriggerInfo> GetExtractionTriggerByJob(string jobCode);
        IEnumerable<ExtractionTriggerInfo>  GetExtractionTriggerByExtraction(int extractionId);
        IEnumerable<ExtractionTriggerInfo> GetExtractionTriggerByRunDate(DateTime startDate,DateTime endDate);
        IEnumerable<ExtractionTriggerInfo> GetExtractionTriggerByRunTime(DateTime runTime);
    }
}
