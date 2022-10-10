using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IIfrsModicationSummaryRepository : IDataRepository<IfrsModicationSummary>
    {
        IEnumerable<IfrsModicationSummary> GetIfrsModicationSummaryBySearch(string searchParam, string path);
        IEnumerable<IfrsModicationSummary> GetIfrsModicationSummarys(int defaultCount);
        IEnumerable<IfrsModicationSummary> ExportIfrsModicationSummary(int defaultCount, string path);
    }
}
