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
    [Export(typeof(IUEQUnquotedEquitySummaryReportRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UEQUnquotedEquitySummaryReportRepository : DataRepositoryBase<UEQUnquotedEquitySummaryReport>, IUEQUnquotedEquitySummaryReportRepository
    {
        protected override UEQUnquotedEquitySummaryReport AddEntity(IFRSContext entityContext, UEQUnquotedEquitySummaryReport entity)
        {
            return entityContext.Set<UEQUnquotedEquitySummaryReport>().Add(entity);
        }

        protected override UEQUnquotedEquitySummaryReport UpdateEntity(IFRSContext entityContext, UEQUnquotedEquitySummaryReport entity)
        {
            return (from e in entityContext.Set<UEQUnquotedEquitySummaryReport>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<UEQUnquotedEquitySummaryReport> GetEntities(IFRSContext entityContext)
        {
            var query = from e in entityContext.Set<UEQUnquotedEquitySummaryReport>()
                        select e;
            query = query.OrderBy(a => a.ID).GroupBy(e => e.CompanyCode).Select(a => a.FirstOrDefault()).Take(500);
            return query;
        }

        protected override UEQUnquotedEquitySummaryReport GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<UEQUnquotedEquitySummaryReport>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<UEQUnquotedEquitySummaryReport> GetUEQUnquotedEquitySummaryReportBySearch(string searchParam, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (searchParam.Contains("ExportData "))
                {
                    searchParam = searchParam.Replace("ExportData ", "");
                    var query = (from e in entityContext.Set<UEQUnquotedEquitySummaryReport>()
                                 where searchParam.Contains(e.description)
                                 orderby e.CompanyCode
                                 select new
                                 {
                                     e.description,
                                     e.Units,
                                     e.BookValue,
                                     e.fairvalue,
                                     e.MarketPrice,
                                     e.ExchangeRate,
                                     e.MarketValue,
                                     e.GainLoss,
                                     e.Rundate,
                                     e.CompanyCode
                                 });

                    if (searchParam.Substring(0, 5) == "split")
                    {
                        searchParam = searchParam.Substring(5, searchParam.Length - 5);
                        var accounts = (from e in query select new { e.CompanyCode }).Distinct();
                        var count = accounts.Count();
                        var ExportHandler = new ExcelService(path);
                        var accountNo = count > 0 ? accounts.ToList().ElementAt(0).CompanyCode : "";
                        string response = null;
                        for (int i = 0; i < count; ++i)
                        {
                            accountNo = accounts.ToList().ElementAt(i).CompanyCode;
                            response = ExportHandler.Export(query.Where(e => e.CompanyCode == accountNo).ToList(), path + accountNo.Replace("/", ""));
                        }
                    }
                    else
                    {
                        var ExportHandler = new ExcelService(path);
                        string response = ExportHandler.Export(query.ToList(), path);
                    }

                    return new List<UEQUnquotedEquitySummaryReport>().Take(0).ToArray().OrderBy(c => c.CompanyCode).ThenBy(c => c.CompanyCode); ;
                }
                else
                {
                    DateTime searchpar = Convert.ToDateTime(searchParam);
                    var query = (from e in entityContext.Set<UEQUnquotedEquitySummaryReport>()
                                 where e.Rundate == searchpar
                                 //orderby e.RefNo, e.datepmt
                                 select e);

                    return query.ToArray();
                }
            }
        }

        public IEnumerable<UEQUnquotedEquitySummaryReport> GetUEQUnquotedEquitySummaryReports(int defaultCount)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<UEQUnquotedEquitySummaryReport>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                             select e).Take(defaultCount);
                return query.ToArray();
            }
        }

        public IEnumerable<UEQUnquotedEquitySummaryReport> ExportUEQUnquotedEquitySummaryReport(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<UEQUnquotedEquitySummaryReport>()
                                 select new
                                 {
                                     e.description,
                                     e.Units,
                                     e.BookValue,
                                     e.fairvalue,
                                     e.MarketPrice,
                                     e.ExchangeRate,
                                     e.MarketValue,
                                     e.GainLoss,
                                     e.Rundate,
                                     e.CompanyCode
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<UEQUnquotedEquitySummaryReport>().Take(0).ToArray().OrderBy(c => c.CompanyCode).ThenBy(c => c.CompanyCode);

                }
                else
                {
                    var query = (from e in entityContext.Set<UEQUnquotedEquitySummaryReport>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                                 select e);

                    return query.ToArray();
                }
            }
        }
    }
}
