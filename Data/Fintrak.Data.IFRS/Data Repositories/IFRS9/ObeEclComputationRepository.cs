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
    [Export(typeof(IObeEclComputationRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ObeEclComputationRepository : DataRepositoryBase<ObeEclComputation>, IObeEclComputationRepository
    {
        protected override ObeEclComputation AddEntity(IFRSContext entityContext, ObeEclComputation entity)
        {
            return entityContext.Set<ObeEclComputation>().Add(entity);
        }

        protected override ObeEclComputation UpdateEntity(IFRSContext entityContext, ObeEclComputation entity)
        {
            return (from e in entityContext.Set<ObeEclComputation>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }
        
        protected override IEnumerable<ObeEclComputation> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<ObeEclComputation>().Take(200)   //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                   select e;
        }

        protected override ObeEclComputation GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ObeEclComputation>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }



        /*
                public IEnumerable<ObeEclComputationInfo> GetObeEclComputations()
                {
                    using (IFRSContext entityContext = new IFRSContext())
                    {
                        var query = from a in entityContext.ObeEclComputationDataSet
                                    join b in entityContext.ScheduleTypeSet on a.Schedule_Type equals b.Code
                                    select new ObeEclComputationInfo()
                                    {
                                        ObeEclComputation = a,
                                        ScheduleType = b
                                    };

                        return query.ToFullyLoaded();
                    }
                }

        */


        public IEnumerable<ObeEclComputation> GetObeEclComputationBySearch(string searchParam, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (searchParam.Contains("ExportData "))
                {
                    searchParam = searchParam.Replace("ExportData ", "");
                    var query = (from e in entityContext.Set<ObeEclComputation>()
                                 where searchParam.Contains(e.Refno) || searchParam.Contains(e.AccountNo)
                                 //orderby e.RefNo, e.datepmt
                                 select new
                                 {
                                     AccountNo = e.Refno,
                                     e.CustomerName,
                                     HC1 = e.Producttype,
                                     HC2 = e.SubType,
                                     e.Stage,
                                     e.Currency,
                                     e.ExchangeRate,
                                     EAD = e.AmortizedCost,
                                     EAD_Trans = e.AmortizedCost_Trans,
                                     e.TotalRecoverableAmt,
                                     e.LGD,
                                     e.DiscountFactor,
                                     PDBase=e.PDBest,
                                     e.PDOptimistic,
                                     e.PDDownTurn,
                                     ECLBase = e.FinalECLBest,
                                     ECLOptimistic = e.FinalECLOptimistic,
                                     ECLDownTurn = e.FinalECLDownTurn,
                                     ECLWeightAvg = e.FinalECLWeightAvg,
                                     Date = e.date_pmt,
                                     e.Rundate
                                 }).OrderBy(c => c.AccountNo).ThenBy(c => c.Date); 

                    if (searchParam.Length >= 5 && searchParam.Substring(0, 5) == "split")
                    {
                        searchParam = searchParam.Substring(5, searchParam.Length - 5);
                        var accounts = (from e in query select new { e.AccountNo }).Distinct();
                        var count = accounts.Count();
                        var ExportHandler = new ExcelService(path);
                        var accountNo = count > 0 ? accounts.ToList().ElementAt(0).AccountNo : "";
                        string response = null;
                        for (int i = 0; i < count; ++i)
                        {
                            accountNo = accounts.ToList().ElementAt(i).AccountNo;
                            response = ExportHandler.Export(query.Where(e => e.AccountNo == accountNo).ToList(), path + accountNo.Replace("/", ""));
                        }
                    }
                    else
                    {
                        var ExportHandler = new ExcelService(path);
                        string response = ExportHandler.Export(query.ToList(), path);
                    }

                    return new List<ObeEclComputation>().Take(0).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<ObeEclComputation>()
                                 where searchParam.Contains(e.Refno)
                                 //orderby e.RefNo, e.datepmt
                                 select e);

                    return query.ToArray();
                }
            }
        }

        public IEnumerable<ObeEclComputation> GetObeEclComputations(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<ObeEclComputation>()
                                 select new
                                 {
                                     AccountNo = e.Refno,
                                     e.CustomerName,                                  
                                     HC1 = e.Producttype,
                                     HC2 = e.SubType,
                                     e.Stage,                                    
                                     e.Currency,
                                     e.ExchangeRate,
                                     EAD = e.AmortizedCost,
                                     EAD_Trans = e.AmortizedCost_Trans,
                                     e.TotalRecoverableAmt,
                                     e.LGD,
                                     e.DiscountFactor,
                                     PDBase =e.PDBest,
                                     e.PDOptimistic,
                                     e.PDDownTurn,
                                     ECLBase = e.FinalECLBest,
                                     ECLOptimistic = e.FinalECLOptimistic,
                                     ECLDownTurn = e.FinalECLDownTurn,
                                     ECLWeightAvg = e.FinalECLWeightAvg,
                                     Date = e.date_pmt,
                                     e.Rundate
                                 }).OrderBy(c => c.AccountNo).ThenBy(c => c.Date); ;
                    if (defaultCount == 0)
                    {
                        var ExportHandler = new ExcelService();
                        var response = ExportHandler.Export(query.ToList(), path);
                    }
                    else
                    {
                        var accounts = (from e in query select new { e.AccountNo }).Distinct();
                        var count = accounts.Count();
                        var ExportHandler = new ExcelService(path);
                        var accountNo = count > 0 ? accounts.ToList().ElementAt(0).AccountNo : "";
                        string response = null;
                        for (int i = 0; i < count; ++i)
                        {
                            accountNo = accounts.ToList().ElementAt(i).AccountNo;
                            response = ExportHandler.Export(query.Where(e => e.AccountNo == accountNo).ToList(), path + accountNo.Replace("/", ""));
                        }
                    }
                    return new List<ObeEclComputation>().Take(defaultCount).ToArray();

                    // var query = (from e in entityContext.Set<ObeEclComputation>().OrderBy(c => c.AccountNo).ThenBy(c => c.date_pmt)
                    //             select e);
                    // var ExportHandler = new ExcelService();
                    // var response = ExportHandler.Export(query.ToList(), path);

                    // return query.Take(defaultCount).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<ObeEclComputation>().Take(defaultCount).OrderBy(c => c.AccountNo).ThenBy(c => c.date_pmt)
                                 select e);

                    return query.ToArray();
                }
            }
        }



    }
}