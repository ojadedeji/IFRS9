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
    [Export(typeof(IUnquotedEquityMedAVGPERepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UnquotedEquityMedAVGPERepository : DataRepositoryBase<UnquotedEquityMedAVGPE>, IUnquotedEquityMedAVGPERepository
    {
        protected override UnquotedEquityMedAVGPE AddEntity(IFRSContext entityContext, UnquotedEquityMedAVGPE entity)
        {
            return entityContext.Set<UnquotedEquityMedAVGPE>().Add(entity);
        }

        protected override UnquotedEquityMedAVGPE UpdateEntity(IFRSContext entityContext, UnquotedEquityMedAVGPE entity)
        {
            return (from e in entityContext.Set<UnquotedEquityMedAVGPE>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<UnquotedEquityMedAVGPE> GetEntities(IFRSContext entityContext)
        {
            var query = from e in entityContext.Set<UnquotedEquityMedAVGPE>()
                        select e;
            query = query.OrderBy(a => a.ID).GroupBy(e => e.CompanyCode).Select(a => a.FirstOrDefault()).Take(500);
            return query;
        }

        protected override UnquotedEquityMedAVGPE GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<UnquotedEquityMedAVGPE>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<UnquotedEquityMedAVGPE> GetUnquotedEquityMedAVGPEBySearch(string searchParam, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (searchParam.Contains("ExportData "))
                {
                    searchParam = searchParam.Replace("ExportData ", "");
                    var query = (from e in entityContext.Set<UnquotedEquityMedAVGPE>()
                                 where searchParam.Contains(e.Caption)
                                 orderby e.CompanyCode
                                 select new
                                 {
                                     e.Caption,
                                     e.Class,
                                     e.MedianPE,
                                     e.AveragePE,
                                     e.ReportType,
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

                    return new List<UnquotedEquityMedAVGPE>().Take(0).ToArray().OrderBy(c => c.CompanyCode).ThenBy(c => c.CompanyCode); ;
                }
                else
                {
                    var query = (from e in entityContext.Set<UnquotedEquityMedAVGPE>()
                                 where e.CompanyCode == searchParam
                                 //orderby e.RefNo, e.datepmt
                                 select e);

                    return query.ToArray();
                }
            }
        }

        public IEnumerable<UnquotedEquityMedAVGPE> GetUnquotedEquityMedAVGPEs(int defaultCount)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<UnquotedEquityMedAVGPE>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                             select e).Take(defaultCount);
                return query.ToArray();
            }
        }

        public IEnumerable<UnquotedEquityMedAVGPE> ExportUnquotedEquityMedAVGPE(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<UnquotedEquityMedAVGPE>()
                                 select new
                                 {
                                     e.Caption,
                                     e.Class,
                                     e.MedianPE,
                                     e.AveragePE,
                                     e.ReportType,
                                     e.CompanyCode
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<UnquotedEquityMedAVGPE>().Take(0).ToArray().OrderBy(c => c.CompanyCode).ThenBy(c => c.CompanyCode);

                }
                else
                {
                    var query = (from e in entityContext.Set<UnquotedEquityMedAVGPE>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                                 select e);

                    return query.ToArray();
                }
            }
        }
    }
}
