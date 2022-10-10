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
    [Export(typeof(IOdECLResultRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OdECLResultRepository : DataRepositoryBase<OdECLResult>, IOdECLResultRepository
    {
        protected override OdECLResult AddEntity(IFRSContext entityContext, OdECLResult entity)
        {
            return entityContext.Set<OdECLResult>().Add(entity);
        }

        protected override OdECLResult UpdateEntity(IFRSContext entityContext, OdECLResult entity)
        {
            return (from e in entityContext.Set<OdECLResult>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<OdECLResult> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<OdECLResult>()
                   select e;
        }

        protected override OdECLResult GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<OdECLResult>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }



        /////////////////////////////////////////// Methods from IRepo..

        public IEnumerable<OdECLResult> GetOdECLResultBySearch(string searchParam) {
            using (IFRSContext entityContext = new IFRSContext()) {
                var query = (from e in entityContext.Set<OdECLResult>()
                             where e.RefNo == searchParam || e.AccountNo == searchParam            //orderby e.RefNo, e.datepmt                             
                             select e);
                return query.ToArray();
            }
        }

        public IEnumerable<OdECLResult> GetOdECLResults(int defaultCount, string path) {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<OdECLResult>()
                                 select new
                                 {
                                     AccountNo = e.RefNo,       
                                     CustomerNo= e.CustomerNo,
                                     e.CustomerName,
                                     HC1 = e.Producttype,
                                     HC2 = e.SubType,
                                     e.Stage,
                                     e.Currency,
                                     e.PrincipalOutBal,
                                     e.EIR,
                                     //e.ExchangeRate,
                                     EAD = e.AmortizedCost,
                                     //EAD_Trans = e.AmortizedCost_Trans,
                                     ECLBase = e.FinalECLBest,
                                     ECLOptimistic = e.FinalECLOptimistic,
                                     ECLDownTurn = e.FinalECLDownTurn,
                                     ECLWeightAvg = e.FinalECLWeightAvg,
                                     e.Rundate
                                 });
                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<OdECLResult>().Take(defaultCount).ToArray();

                }
                else
                {
                    var query = (from e in entityContext.Set<OdECLResult>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                                 select e);

                    return query.ToArray();
                }
            }
        }


    }
}