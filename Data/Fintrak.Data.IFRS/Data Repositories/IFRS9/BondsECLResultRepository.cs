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
    [Export(typeof(IBondsECLResultRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BondsECLResultRepository : DataRepositoryBase<BondsECLResult>, IBondsECLResultRepository
    {
        protected override BondsECLResult AddEntity(IFRSContext entityContext, BondsECLResult entity)
        {
            return entityContext.Set<BondsECLResult>().Add(entity);
        }

        protected override BondsECLResult UpdateEntity(IFRSContext entityContext, BondsECLResult entity)
        {
            return (from e in entityContext.Set<BondsECLResult>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<BondsECLResult> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<BondsECLResult>()
                   select e;
        }

        protected override BondsECLResult GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<BondsECLResult>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }



        /////////////////////////////////////////// Methods from IRepo..

        public IEnumerable<BondsECLResult> GetBondsECLResultBySearch(string searchParam) {
            using (IFRSContext entityContext = new IFRSContext()) {
                var query = (from e in entityContext.Set<BondsECLResult>()
                             where e.RefNo == searchParam    || e.AccountNo == searchParam         //orderby e.RefNo, e.datepmt                             
                             select e);
                return query.ToArray();
            }
        }

        public IEnumerable<BondsECLResult> GetBondsECLResults(int defaultCount, string path) {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<BondsECLResult>()
                                 select new
                                 {
                                     AccountNo = e.RefNo,
                                     AssetDescription = e.CustomerName,
                                     AssetType = e.Producttype,
                                     //e.Stage,
                                     e.Currency,
                                     e.PrincipalOutBal,
                                     e.EIR,
                                     //e.ExchangeRate,
                                     //EAD = e.AmortizedCost,
                                     //EAD_Trans = e.AmortizedCost_Trans,
                                     e.FinalECLBest,
                                     e.FinalECLOptimistic,
                                     e.FinalECLDownTurn,
                                     e.FinalECLWeightAvg,
                                     e.Rundate
                                 });
                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<BondsECLResult>().Take(defaultCount).ToArray();

                    //var query = (from e in entityContext.Set<BondsECLResult>() //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                    //             select e);
                    //var ExportHandler = new ExcelService();
                    //var response = ExportHandler.Export(query.ToList(), path);

                    //return query.Take(defaultCount).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<BondsECLResult>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                                 select e);

                    return query.ToArray();
                }
            }
        }


    }
}