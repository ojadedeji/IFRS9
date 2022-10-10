using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IUEQUnquotedEquitySummaryReportRepository : IDataRepository<UEQUnquotedEquitySummaryReport>
    {
        IEnumerable<UEQUnquotedEquitySummaryReport> GetUEQUnquotedEquitySummaryReportBySearch(string searchParam, string path);
        IEnumerable<UEQUnquotedEquitySummaryReport> GetUEQUnquotedEquitySummaryReports(int defaultCount);
        IEnumerable<UEQUnquotedEquitySummaryReport> ExportUEQUnquotedEquitySummaryReport(int defaultCount, string path);
    }
}
