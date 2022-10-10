using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;
using Fintrak.Shared.Common.Services;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IIfrsStaffBenefitsReportSummaryRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsStaffBenefitsReportSummaryRepository : DataRepositoryBase<IfrsStaffBenefitsReportSummary>, IIfrsStaffBenefitsReportSummaryRepository
    {
        protected override IfrsStaffBenefitsReportSummary AddEntity(IFRSContext entityContext, IfrsStaffBenefitsReportSummary entity)
        {
            return entityContext.Set<IfrsStaffBenefitsReportSummary>().Add(entity);
        }

        protected override IfrsStaffBenefitsReportSummary UpdateEntity(IFRSContext entityContext, IfrsStaffBenefitsReportSummary entity)
        {
            return (from e in entityContext.Set<IfrsStaffBenefitsReportSummary>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<IfrsStaffBenefitsReportSummary> GetEntities(IFRSContext entityContext)
        {
            var query = from e in entityContext.Set<IfrsStaffBenefitsReportSummary>()
                        select e;
            query = query.OrderBy(a => a.ID).GroupBy(e => e.GLCODE).Select(a => a.FirstOrDefault()).Take(500);
            return query;
        }

        protected override IfrsStaffBenefitsReportSummary GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IfrsStaffBenefitsReportSummary>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IfrsStaffBenefitsReportSummary> GetIfrsStaffBenefitsReportSummaryBySearch(string searchParam, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (searchParam.Contains("ExportData "))
                {
                    searchParam = searchParam.Replace("ExportData ", "");
                    var query = (from e in entityContext.Set<IfrsStaffBenefitsReportSummary>()
                                 where searchParam.Contains(e.GLCODE)
                                 orderby e.GLCODE
                                 select new
                                 {
                                     e.ID,
                                     e.GLCODE,
                                     e.GLAccountName,
                                     e.UnaditedPerGL,
                                     e.PerListing,
                                     e.AuditAdjustment,
                                     e.Amortizedcost,
                                     e.AmortisedCostPerComputationSchedule
                                 });

                    if (searchParam.Substring(0, 5) == "split")
                    {
                        searchParam = searchParam.Substring(5, searchParam.Length - 5);
                        var accounts = (from e in query select new { e.GLCODE }).Distinct();
                        var count = accounts.Count();
                        var ExportHandler = new ExcelService(path);
                        var accountNo = count > 0 ? accounts.ToList().ElementAt(0).GLCODE : "";
                        string response = null;
                        for (int i = 0; i < count; ++i)
                        {
                            accountNo = accounts.ToList().ElementAt(i).GLCODE;
                            response = ExportHandler.Export(query.Where(e => e.GLCODE == accountNo).ToList(), path + accountNo.Replace("/", ""));
                        }
                    }
                    else
                    {
                        var ExportHandler = new ExcelService(path);
                        string response = ExportHandler.Export(query.ToList(), path);
                    }

                    return new List<IfrsStaffBenefitsReportSummary>().Take(0).ToArray().OrderBy(c => c.GLCODE).ThenBy(c => c.GLCODE); ;
                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsStaffBenefitsReportSummary>()
                                 where e.GLCODE == searchParam
                                 //orderby e.RefNo, e.datepmt
                                 select e);

                    return query.ToArray();
                }
            }
        }

        public IEnumerable<IfrsStaffBenefitsReportSummary> GetIfrsStaffBenefitsReportSummary(int defaultCount)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<IfrsStaffBenefitsReportSummary>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                             select e).Take(defaultCount);
                return query.ToArray();
            }
        }

        public IEnumerable<IfrsStaffBenefitsReportSummary> ExportIfrsStaffBenefitsReportSummary(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<IfrsStaffBenefitsReportSummary>()
                                 select new
                                 {
                                     e.ID,
                                     e.GLCODE,
                                     e.GLAccountName,
                                     e.UnaditedPerGL,
                                     e.PerListing,
                                     e.AuditAdjustment,
                                     e.Amortizedcost,
                                     e.AmortisedCostPerComputationSchedule
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<IfrsStaffBenefitsReportSummary>().Take(0).ToArray().OrderBy(c => c.GLCODE).ThenBy(c => c.GLCODE);

                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsStaffBenefitsReportSummary>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                                 select e);

                    return query.ToArray();
                }
            }
        }
    }
}
