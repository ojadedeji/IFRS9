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
    [Export(typeof(IUnquotedEquityMedAVGPBRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UnquotedEquityMedAVGPBRepository : DataRepositoryBase<UnquotedEquityMedAVGPB>, IUnquotedEquityMedAVGPBRepository
    {
        protected override UnquotedEquityMedAVGPB AddEntity(IFRSContext entityContext, UnquotedEquityMedAVGPB entity)
        {
            return entityContext.Set<UnquotedEquityMedAVGPB>().Add(entity);
        }

        protected override UnquotedEquityMedAVGPB UpdateEntity(IFRSContext entityContext, UnquotedEquityMedAVGPB entity)
        {
            return (from e in entityContext.Set<UnquotedEquityMedAVGPB>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<UnquotedEquityMedAVGPB> GetEntities(IFRSContext entityContext)
        {
            var query = from e in entityContext.Set<UnquotedEquityMedAVGPB>()
                        select e;
            query = query.OrderBy(a => a.ID).GroupBy(e => e.CompanyCode).Select(a => a.FirstOrDefault()).Take(500);
            return query;
        }

        protected override UnquotedEquityMedAVGPB GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<UnquotedEquityMedAVGPB>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<UnquotedEquityMedAVGPB> GetUnquotedEquityMedAVGPBBySearch(string searchParam, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (searchParam.Contains("ExportData "))
                {
                    searchParam = searchParam.Replace("ExportData ", "");
                    var query = (from e in entityContext.Set<UnquotedEquityMedAVGPB>()
                                 where searchParam.Contains(e.Caption)
                                 orderby e.CompanyCode
                                 select new
                                 {
                                     e.Caption,
                                     e.Class,
                                     e.MedianPB,
                                     e.AveragePB,
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

                    return new List<UnquotedEquityMedAVGPB>().Take(0).ToArray().OrderBy(c => c.CompanyCode).ThenBy(c => c.CompanyCode); ;
                }
                else
                {
                    var query = (from e in entityContext.Set<UnquotedEquityMedAVGPB>()
                                 where e.CompanyCode == searchParam
                                 //orderby e.RefNo, e.datepmt
                                 select e);

                    return query.ToArray();
                }
            }
        }

        public IEnumerable<UnquotedEquityMedAVGPB> GetUnquotedEquityMedAVGPBs(int defaultCount)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<UnquotedEquityMedAVGPB>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                             select e).Take(defaultCount);
                return query.ToArray();
            }
        }

        public IEnumerable<UnquotedEquityMedAVGPB> ExportUnquotedEquityMedAVGPB(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<UnquotedEquityMedAVGPB>()
                                 select new
                                 {
                                     e.Caption,
                                     e.Class,
                                     e.MedianPB,
                                     e.AveragePB,
                                     e.ReportType,
                                     e.CompanyCode
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<UnquotedEquityMedAVGPB>().Take(0).ToArray().OrderBy(c => c.CompanyCode).ThenBy(c => c.CompanyCode);

                }
                else
                {
                    var query = (from e in entityContext.Set<UnquotedEquityMedAVGPB>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                                 select e);

                    return query.ToArray();
                }
            }
        }
    }
}
