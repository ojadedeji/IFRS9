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
    [Export(typeof(ICashFlowRestructureRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CashFlowRestructureRepository : DataRepositoryBase<CashFlowRestructure>, ICashFlowRestructureRepository
    {
        protected override CashFlowRestructure AddEntity(IFRSContext entityContext, CashFlowRestructure entity)
        {
            return entityContext.Set<CashFlowRestructure>().Add(entity);
        }

        protected override CashFlowRestructure UpdateEntity(IFRSContext entityContext, CashFlowRestructure entity)
        {
            return (from e in entityContext.Set<CashFlowRestructure>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CashFlowRestructure> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<CashFlowRestructure>()
                   select e;
        }

        protected override CashFlowRestructure GetEntity(IFRSContext entityContext, int Id)
        {
            var query = (from e in entityContext.Set<CashFlowRestructure>()
                         where e.ID == Id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<CashFlowRestructure> GetCashFlowRestructureBySearch(string searchParam)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<CashFlowRestructure>()
                             where e.Refno == searchParam 
                             orderby e.date_pmt
                             select e);

                return query.ToArray();
            }
        }

        public IEnumerable<CashFlowRestructure> GetCashFlowRestructures(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<CashFlowRestructure>()
                                 
                                 select new
                                 {
                                     RefNo = e.Refno,
                                     Date_PMT  = e.date_pmt,
                                     Amount = e.amt_pmt,                                   
                                     OpeningBalance= e.OpeningBalance,
                                     amt_int_pay=e.amt_int_pay,                                    
                                     amt_prin_pay=e.amt_prin_pay,
                                     amt = e.amt_pmt,
                                     CummulativeInterest =e.CummulativeInterest,
                                     ClosingBalance=e.ClosingBalance,
                                     Rundate = e.Rundate,

                                 });
                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<CashFlowRestructure>().Take(defaultCount).ToArray();

                    //var query = (from e in entityContext.Set<CashFlowRestructure>() select e);
                    //var ExportHandler = new ExcelService();
                    //var response = ExportHandler.Export(query.ToList(), path);

                    //return query.Take(defaultCount).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<CashFlowRestructure>().Take(defaultCount) select e);

                    return query.ToArray();
                }
            }
        }
    }
}
