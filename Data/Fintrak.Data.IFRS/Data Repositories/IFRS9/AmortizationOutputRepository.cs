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
    [Export(typeof(IAmortizationOutputRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AmortizationOutputRepository : DataRepositoryBase<AmortizationOutput>, IAmortizationOutputRepository
    {
        protected override AmortizationOutput AddEntity(IFRSContext entityContext, AmortizationOutput entity)
        {
            return entityContext.Set<AmortizationOutput>().Add(entity);
        }

        protected override AmortizationOutput UpdateEntity(IFRSContext entityContext, AmortizationOutput entity)
        {
            return (from e in entityContext.Set<AmortizationOutput>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<AmortizationOutput> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<AmortizationOutput>()
                   select e;
        }

        protected override AmortizationOutput GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<AmortizationOutput>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }


        ///////// ------- ////////////////////////////////// Methods from IRepo..

        //public IEnumerable<AmortizationOutput> GetAmortizationOutputBySearch(string searchParam, string path) {
        //    using (IFRSContext entityContext = new IFRSContext()) {
        //        var query = (from e in entityContext.Set<AmortizationOutput>()
        //                     where e.Refno == searchParam  //orderby e.RunDate //, e.datepmt                             
        //                     select e);
        //        return query.ToArray();
        //    }
        //}
        public IEnumerable<AmortizationOutput> GetAmortizationOutputBySearch(string searchParam, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (searchParam.Contains("ExportData "))
                {
                    searchParam = searchParam.Replace("ExportData ", "");
                    var query = (from e in entityContext.Set<AmortizationOutput>()
                                 where searchParam.Contains(e.Refno)
                                 //orderby e.RefNo, e.datepmt
                                 select new
                                 {
                                     e.Refno,
                                     e.CustomerName,
                                     e.Value_Date,
                                     e.Maturity_Date,
                                     e.Currency,
                                     e.ExchangeRate,
                                     e.Stage,
                                     e.Date,
                                     e.EIR,
                                     e.DailyEir,
                                     e.NorminalRate,
                                     e.AmountPrincEnd,
                                     e.AmortizedCost,
                                     e.AmortizedCost_OverDue,
                                     e.Scenerio1_ECL,
                                     e.Scenerio2_ECL,
                                     e.Scenerio3_ECL,
                                     e.Total_ECL
                                 });

                    if (searchParam.Substring(0, 5) == "split")
                    {
                        searchParam = searchParam.Substring(5, searchParam.Length - 5);
                        var accounts = (from e in query select new { e.Refno }).Distinct();
                        var count = accounts.Count();
                        var ExportHandler = new ExcelService(path);
                        var accountNo = count > 0 ? accounts.ToList().ElementAt(0).Refno : "";
                        string response = null;
                        for (int i = 0; i < count; ++i)
                        {
                            accountNo = accounts.ToList().ElementAt(i).Refno;
                            response = ExportHandler.Export(query.Where(e => e.Refno == accountNo).ToList(), path + accountNo.Replace("/", ""));
                        }
                    }
                    else
                    {
                        var ExportHandler = new ExcelService(path);
                        string response = ExportHandler.Export(query.ToList(), path);
                    }

                    return new List<AmortizationOutput>().Take(0).ToArray().OrderBy(c => c.CustomerName).ThenBy(c => c.Refno); ;
                }
                else
                {
                    var query = (from e in entityContext.Set<AmortizationOutput>()
                                 where e.Refno == searchParam
                                 //orderby e.RefNo, e.datepmt
                                 select e);

                    return query.ToArray();
                }
            }
        }

        public IEnumerable<AmortizationOutput> GetAmortizationOutputs(int defaultCount) {
            using (IFRSContext entityContext = new IFRSContext()) {
                var query = (from e in entityContext.Set<AmortizationOutput>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                             select e).Take(defaultCount);
                return query.ToArray();
            }
        }

        public IEnumerable<AmortizationOutput> ExportAmortizationOutput(int defaultCount, string path) {
            using (IFRSContext entityContext = new IFRSContext()) {
                if (!string.IsNullOrEmpty(path)) {
                    var query = (from e in entityContext.Set<AmortizationOutput>()
                                 select new {
                                    e.Refno,
                                    e.CustomerName,
                                    e.Value_Date,
                                    e.Maturity_Date,
                                    e.Currency,
                                    e.ExchangeRate,
                                    e.Stage,
                                    e.Date,
                                    e.EIR,
                                    e.DailyEir,
                                    e.NorminalRate,
                                    e.AmountPrincEnd,
                                    e.AmortizedCost,
                                    e.AmortizedCost_OverDue,
                                    e.Scenerio1_ECL,
                                    e.Scenerio2_ECL,
                                    e.Scenerio3_ECL,
                                    e.Total_ECL
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<AmortizationOutput>().Take(0).ToArray().OrderBy(c => c.CustomerName).ThenBy(c => c.Refno);

                    //var query = (from e in entityContext.Set<LoanECLResult>() //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                    //             select e);
                    //var ExportHandler = new ExcelService();
                    //var response = ExportHandler.Export(query.ToList(), path);

                    //return query.Take(defaultCount).ToArray();
                } else {
                    var query = (from e in entityContext.Set<AmortizationOutput>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                                 select e);

                    return query.ToArray();
                }
            }
        }

    }
}