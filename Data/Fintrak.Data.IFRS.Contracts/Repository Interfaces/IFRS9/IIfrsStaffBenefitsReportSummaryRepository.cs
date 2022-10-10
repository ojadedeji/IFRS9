using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IIfrsStaffBenefitsReportSummaryRepository : IDataRepository<IfrsStaffBenefitsReportSummary>
    {
        IEnumerable<IfrsStaffBenefitsReportSummary> GetIfrsStaffBenefitsReportSummaryBySearch(string searchParam, string path);
        IEnumerable<IfrsStaffBenefitsReportSummary> GetIfrsStaffBenefitsReportSummary(int defaultCount);
        IEnumerable<IfrsStaffBenefitsReportSummary> ExportIfrsStaffBenefitsReportSummary(int defaultCount, string path);
    }
}
