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
    [Export(typeof(IUnquotedEquityMKTRDiscRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UnquotedEquityMKTRDiscRepository : DataRepositoryBase<UnquotedEquityMKTRDisc>, IUnquotedEquityMKTRDiscRepository
    {
        protected override UnquotedEquityMKTRDisc AddEntity(IFRSContext entityContext, UnquotedEquityMKTRDisc entity)
        {
            return entityContext.Set<UnquotedEquityMKTRDisc>().Add(entity);
        }

        protected override UnquotedEquityMKTRDisc UpdateEntity(IFRSContext entityContext, UnquotedEquityMKTRDisc entity)
        {
            return (from e in entityContext.Set<UnquotedEquityMKTRDisc>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<UnquotedEquityMKTRDisc> GetEntities(IFRSContext entityContext)
        {
            var query = from e in entityContext.Set<UnquotedEquityMKTRDisc>()
                        select e;
            query = query.OrderBy(a => a.ID).GroupBy(e => e.CompanyCode).Select(a => a.FirstOrDefault()).Take(500);
            return query;
        }

        protected override UnquotedEquityMKTRDisc GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<UnquotedEquityMKTRDisc>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<UnquotedEquityMKTRDisc> GetUnquotedEquityMKTRDiscBySearch(string searchParam, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (searchParam.Contains("ExportData "))
                {
                    searchParam = searchParam.Replace("ExportData ", "");
                    var query = (from e in entityContext.Set<UnquotedEquityMKTRDisc>()
                                 where searchParam.Contains(e.CompanyCode)
                                 orderby e.CompanyCode
                                 select new
                                 {
                                     e.MKTRDisc,
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

                    return new List<UnquotedEquityMKTRDisc>().Take(0).ToArray().OrderBy(c => c.CompanyCode).ThenBy(c => c.CompanyCode); ;
                }
                else
                {
                    DateTime searchpar = Convert.ToDateTime(searchParam);
                    var query = (from e in entityContext.Set<UnquotedEquityMKTRDisc>()
                                 where e.Rundate == searchpar
                                 //orderby e.RefNo, e.datepmt
                                 select e);

                    return query.ToArray();
                }
            }
        }

        public IEnumerable<UnquotedEquityMKTRDisc> GetUnquotedEquityMKTRDiscs(int defaultCount)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<UnquotedEquityMKTRDisc>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                             select e).Take(defaultCount);
                return query.ToArray();
            }
        }

        public IEnumerable<UnquotedEquityMKTRDisc> ExportUnquotedEquityMKTRDisc(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<UnquotedEquityMKTRDisc>()
                                 select new
                                 {
                                     e.MKTRDisc,
                                     e.Rundate,
                                     e.CompanyCode
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<UnquotedEquityMKTRDisc>().Take(0).ToArray().OrderBy(c => c.CompanyCode).ThenBy(c => c.CompanyCode);

                }
                else
                {
                    var query = (from e in entityContext.Set<UnquotedEquityMKTRDisc>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                                 select e);

                    return query.ToArray();
                }
            }
        }
    }
}
