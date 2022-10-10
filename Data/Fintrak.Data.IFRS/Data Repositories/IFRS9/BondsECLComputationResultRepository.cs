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
    [Export(typeof(IBondsECLComputationResultRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BondsECLComputationResultRepository : DataRepositoryBase<BondsECLComputationResult>, IBondsECLComputationResultRepository
    {
        protected override BondsECLComputationResult AddEntity(IFRSContext entityContext, BondsECLComputationResult entity)
        {
            return entityContext.Set<BondsECLComputationResult>().Add(entity);
        }

        protected override BondsECLComputationResult UpdateEntity(IFRSContext entityContext, BondsECLComputationResult entity)
        {
            return (from e in entityContext.Set<BondsECLComputationResult>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<BondsECLComputationResult> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<BondsECLComputationResult>()
                   select e;
        }

        protected override BondsECLComputationResult GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<BondsECLComputationResult>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }



        /////////////////////////////////////////// Methods from IRepo..

        public IEnumerable<BondsECLComputationResult> GetBondsECLComputationResultBySearch(string searchParam, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (searchParam.Contains("ExportData "))
                {
                    searchParam = searchParam.Replace("ExportData ", "");
                    var query = (from e in entityContext.Set<BondsECLComputationResult>()
                                 where searchParam.Contains(e.Refno) || searchParam.Contains(e.CustomerName) || searchParam.Contains(e.Producttype)
                                 //orderby e.RefNo, e.datepmt
                                 select new
                                 {
                                     AccountNo = e.Refno,
                                     AssetDescription = e.CustomerName,
                                     Date = e.date_pmt,
                                     AssetType = e.Producttype,
                                     e.Stage,
                                     e.Currency,
                                     e.ExchangeRate,
                                     EAD = e.AmortizedCost,
                                     EAD_Trans = e.AmortizedCost_Trans,
                                     e.LGD,
                                     e.DiscountFactor,
                                     PD = e.PDBest,
                                     e.FinalECLBest,
                                     e.FinalECLOptimistic,
                                     e.FinalECLDownTurn,
                                     e.FinalECLWeightAvg,
                                     e.Rundate
                                 });

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

                    return new List<BondsECLComputationResult>().Take(0).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<BondsECLComputationResult>()
                                 where searchParam.Contains(e.Refno)
                                 //orderby e.RefNo, e.datepmt
                                 select e);

                    return query.ToArray();
                }
            }
        }

        public IEnumerable<BondsECLComputationResult> GetBondsECLComputationResults(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<BondsECLComputationResult>()
                                 select new
                                 {
                                     AccountNo = e.Refno,
                                     AssetDescription = e.CustomerName,
                                     Date = e.date_pmt,
                                     AssetType = e.Producttype,
                                     e.Stage,
                                     e.Currency,
                                     e.ExchangeRate,
                                     EAD = e.AmortizedCost,
                                     EAD_Trans = e.AmortizedCost_Trans,
                                     e.LGD,
                                     e.DiscountFactor,
                                     PD = e.PDBest,
                                     e.FinalECLBest,
                                     e.FinalECLOptimistic,
                                     e.FinalECLDownTurn,
                                     e.FinalECLWeightAvg,
                                     e.Rundate
                                 });
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

                    return new List<BondsECLComputationResult>().Take(0).ToArray();

                    //var query = (from e in entityContext.Set<BondsECLComputationResult>()//.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                    //             select e);
                    //var ExportHandler = new ExcelService();
                    //var response = ExportHandler.Export(query.ToList(), path);

                    //return query.Take(defaultCount).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<BondsECLComputationResult>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                                 select e);
                    return query.ToArray();
                }
            }
        }


    }
}