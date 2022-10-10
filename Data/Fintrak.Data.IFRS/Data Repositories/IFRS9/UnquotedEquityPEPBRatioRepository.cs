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
    [Export(typeof(IUnquotedEquityPEPBRatioRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UnquotedEquityPEPBRatioRepository : DataRepositoryBase<UnquotedEquityPEPBRatio>, IUnquotedEquityPEPBRatioRepository
    {
        protected override UnquotedEquityPEPBRatio AddEntity(IFRSContext entityContext, UnquotedEquityPEPBRatio entity)
        {
            return entityContext.Set<UnquotedEquityPEPBRatio>().Add(entity);
        }

        protected override UnquotedEquityPEPBRatio UpdateEntity(IFRSContext entityContext, UnquotedEquityPEPBRatio entity)
        {
            return (from e in entityContext.Set<UnquotedEquityPEPBRatio>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<UnquotedEquityPEPBRatio> GetEntities(IFRSContext entityContext)
        {
            var query = from e in entityContext.Set<UnquotedEquityPEPBRatio>()
                        select e;
            query = query.OrderBy(a => a.ID).GroupBy(e => e.Coperation).Select(a => a.FirstOrDefault()).Take(500);
            return query;
        }

        protected override UnquotedEquityPEPBRatio GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<UnquotedEquityPEPBRatio>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<UnquotedEquityPEPBRatio> GetUnquotedEquityPEPBRatioBySearch(string searchParam, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (searchParam.Contains("ExportData "))
                {
                    searchParam = searchParam.Replace("ExportData ", "");
                    var query = (from e in entityContext.Set<UnquotedEquityPEPBRatio>()
                                 where searchParam.Contains(e.Coperation)
                                 orderby e.Coperation
                                 select new
                                 {
                                     e.Coperation,
                                     e.Country,
                                     e.PERatio,
                                     e.PBRatio,
                                     e.Rundate,
                                     e.CompanyCode
                                 });

                    if (searchParam.Substring(0, 5) == "split")
                    {
                        searchParam = searchParam.Substring(5, searchParam.Length - 5);
                        var accounts = (from e in query select new { e.Coperation }).Distinct();
                        var count = accounts.Count();
                        var ExportHandler = new ExcelService(path);
                        var accountNo = count > 0 ? accounts.ToList().ElementAt(0).Coperation : "";
                        string response = null;
                        for (int i = 0; i < count; ++i)
                        {
                            accountNo = accounts.ToList().ElementAt(i).Coperation;
                            response = ExportHandler.Export(query.Where(e => e.Coperation == accountNo).ToList(), path + accountNo.Replace("/", ""));
                        }
                    }
                    else
                    {
                        var ExportHandler = new ExcelService(path);
                        string response = ExportHandler.Export(query.ToList(), path);
                    }

                    return new List<UnquotedEquityPEPBRatio>().Take(0).ToArray().OrderBy(c => c.Coperation).ThenBy(c => c.Coperation); ;
                }
                else
                {
                    DateTime searchpar = Convert.ToDateTime(searchParam);
                    var query = (from e in entityContext.Set<UnquotedEquityPEPBRatio>()
                                 where e.Rundate == searchpar
                                 //orderby e.RefNo, e.datepmt
                                 select e);

                    return query.ToArray();
                }
            }
        }

        public IEnumerable<UnquotedEquityPEPBRatio> GetUnquotedEquityPEPBRatios(int defaultCount)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<UnquotedEquityPEPBRatio>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                             select e).Take(defaultCount);
                return query.ToArray();
            }
        }

        public IEnumerable<UnquotedEquityPEPBRatio> ExportUnquotedEquityPEPBRatio(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<UnquotedEquityPEPBRatio>()
                                 select new
                                 {
                                     e.Coperation,
                                     e.Country,
                                     e.PERatio,
                                     e.PBRatio,
                                     e.Rundate,
                                     e.CompanyCode
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<UnquotedEquityPEPBRatio>().Take(0).ToArray().OrderBy(c => c.Coperation).ThenBy(c => c.Coperation);

                }
                else
                {
                    var query = (from e in entityContext.Set<UnquotedEquityPEPBRatio>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                                 select e);

                    return query.ToArray();
                }
            }
        }
    }
}
