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
    [Export(typeof(IIfrsAccessEstimateRecoveryOutputRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsAccessEstimateRecoveryOutputRepository : DataRepositoryBase<IfrsAccessEstimateRecoveryOutput>, IIfrsAccessEstimateRecoveryOutputRepository
    {
        protected override IfrsAccessEstimateRecoveryOutput AddEntity(IFRSContext entityContext, IfrsAccessEstimateRecoveryOutput entity)
        {
            return entityContext.Set<IfrsAccessEstimateRecoveryOutput>().Add(entity);
        }

        protected override IfrsAccessEstimateRecoveryOutput UpdateEntity(IFRSContext entityContext, IfrsAccessEstimateRecoveryOutput entity)
        {
            return (from e in entityContext.Set<IfrsAccessEstimateRecoveryOutput>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<IfrsAccessEstimateRecoveryOutput> GetEntities(IFRSContext entityContext)
        {
            var query = from e in entityContext.Set<IfrsAccessEstimateRecoveryOutput>()
                        select e;
            query = query.OrderBy(a => a.ID).GroupBy(e => e.HistoryQuarter).Select(a => a.FirstOrDefault()).Take(500);
            return query;
        }

        protected override IfrsAccessEstimateRecoveryOutput GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IfrsAccessEstimateRecoveryOutput>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IfrsAccessEstimateRecoveryOutput> GetIfrsAccessEstimateRecoveryOutputBySearch(string searchParam, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (searchParam.Contains("ExportData "))
                {
                    searchParam = searchParam.Replace("ExportData ", "");
                    var query = (from e in entityContext.Set<IfrsAccessEstimateRecoveryOutput>()
                                 where searchParam.Contains(e.sector)
                                 orderby e.HistoryQuarter
                                 select new
                                 {
                                     e.Seq,
                                     e.sector,
                                     e.HistoryQuarter,
                                     e.ExtematedRecovery,
                                     e.Writeoff,
                                     e.WeightedOutstndBal,
                                     e.LGD
                                 });

                    if (searchParam.Substring(0, 5) == "split")
                    {
                        searchParam = searchParam.Substring(5, searchParam.Length - 5);
                        var accounts = (from e in query select new { e.sector }).Distinct();
                        var count = accounts.Count();
                        var ExportHandler = new ExcelService(path);
                        var accountNo = count > 0 ? accounts.ToList().ElementAt(0).sector : "";
                        string response = null;
                        for (int i = 0; i < count; ++i)
                        {
                            accountNo = accounts.ToList().ElementAt(i).sector;
                            response = ExportHandler.Export(query.Where(e => e.sector == accountNo).ToList(), path + accountNo.Replace("/", ""));
                        }
                    }
                    else
                    {
                        var ExportHandler = new ExcelService(path);
                        string response = ExportHandler.Export(query.ToList(), path);
                    }

                    return new List<IfrsAccessEstimateRecoveryOutput>().Take(0).ToArray().OrderBy(c => c.HistoryQuarter).ThenBy(c => c.sector); ;
                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsAccessEstimateRecoveryOutput>()
                                 where e.sector == searchParam
                                 //orderby e.RefNo, e.datepmt
                                 select e);

                    return query.ToArray();
                }
            }
        }

        public IEnumerable<IfrsAccessEstimateRecoveryOutput> GetIfrsAccessEstimateRecoveryOutputs(int defaultCount)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<IfrsAccessEstimateRecoveryOutput>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                             select e).Take(defaultCount);
                return query.ToArray();
            }
        }

        public IEnumerable<IfrsAccessEstimateRecoveryOutput> ExportIfrsAccessEstimateRecoveryOutput(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<IfrsAccessEstimateRecoveryOutput>()
                                 select new
                                 {
                                     e.Seq,
                                     e.sector,
                                     e.HistoryQuarter,
                                     e.ExtematedRecovery,
                                     e.Writeoff,
                                     e.WeightedOutstndBal,
                                     e.LGD
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<IfrsAccessEstimateRecoveryOutput>().Take(0).ToArray().OrderBy(c => c.HistoryQuarter).ThenBy(c => c.sector);

                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsAccessEstimateRecoveryOutput>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                                 select e);

                    return query.ToArray();
                }
            }
        }
    }
}
