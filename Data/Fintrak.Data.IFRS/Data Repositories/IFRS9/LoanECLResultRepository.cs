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
    [Export(typeof(ILoanECLResultRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LoanECLResultRepository : DataRepositoryBase<LoanECLResult>, ILoanECLResultRepository
    {
        protected override LoanECLResult AddEntity(IFRSContext entityContext, LoanECLResult entity)
        {
            return entityContext.Set<LoanECLResult>().Add(entity);
        }

        protected override LoanECLResult UpdateEntity(IFRSContext entityContext, LoanECLResult entity)
        {
            return (from e in entityContext.Set<LoanECLResult>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<LoanECLResult> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<LoanECLResult>()
                   select e;
        }

        protected override LoanECLResult GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<LoanECLResult>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }



        /////////////////////////////////////////// Methods from IRepo..

        public IEnumerable<LoanECLResult> GetLoanECLResultBySearch(string searchParam) {
            using (IFRSContext entityContext = new IFRSContext()) {
                var query = (from e in entityContext.Set<LoanECLResult>()
                             where e.RefNo == searchParam    || e.AccountNo == searchParam         //orderby e.RefNo, e.datepmt                             
                             select e);
                return query.ToArray();
            }
        }

        public IEnumerable<LoanECLResult> GetLoanECLResults(int defaultCount, string path) {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<LoanECLResult>()
                                 select new
                                 {
                                     AccountNo = e.RefNo,
                                     CustomerNo = e.AccountNo,
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

                    return new List<LoanECLResult>().Take(defaultCount).ToArray();

                    //var query = (from e in entityContext.Set<LoanECLResult>() //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                    //             select e);
                    //var ExportHandler = new ExcelService();
                    //var response = ExportHandler.Export(query.ToList(), path);

                    //return query.Take(defaultCount).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<LoanECLResult>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                                 select e);

                    return query.ToArray();
                }
            }
        }


    }
}