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
    [Export(typeof(IUnquotedEquitySummaryInputRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UnquotedEquitySummaryInputRepository : DataRepositoryBase<UnquotedEquitySummaryInput>, IUnquotedEquitySummaryInputRepository
    {
        protected override UnquotedEquitySummaryInput AddEntity(IFRSContext entityContext, UnquotedEquitySummaryInput entity)
        {
            return entityContext.Set<UnquotedEquitySummaryInput>().Add(entity);
        }

        protected override UnquotedEquitySummaryInput UpdateEntity(IFRSContext entityContext, UnquotedEquitySummaryInput entity)
        {
            return (from e in entityContext.Set<UnquotedEquitySummaryInput>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<UnquotedEquitySummaryInput> GetEntities(IFRSContext entityContext)
        {
            var query = from e in entityContext.Set<UnquotedEquitySummaryInput>()
                        select e;
            query = query.OrderBy(a => a.ID).GroupBy(e => e.Description).Select(a => a.FirstOrDefault()).Take(500);
            return query;
        }

        protected override UnquotedEquitySummaryInput GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<UnquotedEquitySummaryInput>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<UnquotedEquitySummaryInput> GetUnquotedEquitySummaryInputBySearch(string searchParam, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (searchParam.Contains("ExportData "))
                {
                    searchParam = searchParam.Replace("ExportData ", "");
                    var query = (from e in entityContext.Set<UnquotedEquitySummaryInput>()
                                 where searchParam.Contains(e.Description)
                                 orderby e.Description
                                 select new
                                 {
                                     e.Description,
                                     e.Units,
                                     e.BookValue,
                                     e.MarketPrice,
                                     e.ExchangeRate,
                                     e.Rundate,
                                     e.CompanyCode
                                 });

                    if (searchParam.Substring(0, 5) == "split")
                    {
                        searchParam = searchParam.Substring(5, searchParam.Length - 5);
                        var accounts = (from e in query select new { e.Description }).Distinct();
                        var count = accounts.Count();
                        var ExportHandler = new ExcelService(path);
                        var accountNo = count > 0 ? accounts.ToList().ElementAt(0).Description : "";
                        string response = null;
                        for (int i = 0; i < count; ++i)
                        {
                            accountNo = accounts.ToList().ElementAt(i).Description;
                            response = ExportHandler.Export(query.Where(e => e.Description == accountNo).ToList(), path + accountNo.Replace("/", ""));
                        }
                    }
                    else
                    {
                        var ExportHandler = new ExcelService(path);
                        string response = ExportHandler.Export(query.ToList(), path);
                    }

                    return new List<UnquotedEquitySummaryInput>().Take(0).ToArray().OrderBy(c => c.Description).ThenBy(c => c.Description); ;
                }
                else
                {
                    DateTime searchpar = Convert.ToDateTime(searchParam);
                    var query = (from e in entityContext.Set<UnquotedEquitySummaryInput>()
                                 where e.Rundate == searchpar
                                 //orderby e.RefNo, e.datepmt
                                 select e);

                    return query.ToArray();
                }
            }
        }

        public IEnumerable<UnquotedEquitySummaryInput> GetUnquotedEquitySummaryInputs(int defaultCount)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<UnquotedEquitySummaryInput>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                             select e).Take(defaultCount);
                return query.ToArray();
            }
        }

        public IEnumerable<UnquotedEquitySummaryInput> ExportUnquotedEquitySummaryInput(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<UnquotedEquitySummaryInput>()
                                 select new
                                 {
                                     e.Description,
                                     e.Units,
                                     e.BookValue,
                                     e.MarketPrice,
                                     e.ExchangeRate,
                                     e.Rundate,
                                     e.CompanyCode
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<UnquotedEquitySummaryInput>().Take(0).ToArray().OrderBy(c => c.Description).ThenBy(c => c.Description);

                }
                else
                {
                    var query = (from e in entityContext.Set<UnquotedEquitySummaryInput>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                                 select e);

                    return query.ToArray();
                }
            }
        }
    }
}
