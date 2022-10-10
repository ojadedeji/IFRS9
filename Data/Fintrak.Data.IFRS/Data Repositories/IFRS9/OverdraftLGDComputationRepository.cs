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
    [Export(typeof(IOverdraftLGDComputationRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OverdraftLGDComputationRepository : DataRepositoryBase<OverdraftLGDComputation>, IOverdraftLGDComputationRepository
    {
        protected override OverdraftLGDComputation AddEntity(IFRSContext entityContext, OverdraftLGDComputation entity)
        {
            return entityContext.Set<OverdraftLGDComputation>().Add(entity);
        }

        protected override OverdraftLGDComputation UpdateEntity(IFRSContext entityContext, OverdraftLGDComputation entity)
        {
            return (from e in entityContext.Set<OverdraftLGDComputation>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<OverdraftLGDComputation> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<OverdraftLGDComputation>()
                   select e;
        }

        protected override OverdraftLGDComputation GetEntity(IFRSContext entityContext, int Id)
        {
            var query = (from e in entityContext.Set<OverdraftLGDComputation>()
                         where e.Id == Id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<OverdraftLGDComputation> GetRecordByRefNo(string searchParam)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<OverdraftLGDComputation>()
                             where e.Refno == searchParam || e.AccountNo == searchParam
                             orderby e.date_pmt
                             select e);

                return query.ToArray();
            }
        }

        public IEnumerable<OverdraftLGDComputation> GetOverdraftLGDComputations(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<OverdraftLGDComputation>()
                                 select new
                                 {
                                     CustID = e.AccountNo,
                                     AccountNo = e.Refno,
                                     HC1 = e.Producttype,
                                     HC2 = e.SubType,
                                     e.CustomerName,
                                     e.Currency,
                                     e.ExchangeRate,
                                     Date = e.date_pmt,
                                     e.MaturityDate,
                                     EAD = e.AmortizedCost_FCY,
                                     EAD_Trans = e.AmortizedCost_LCY,
                                     e.EIR,
                                     e.Stage,
                                     e.TotalColRecAmt,
                                     e.RecoveryRate,
                                     e.UNSecuredRecovery,
                                     e.TotalRecoverableAmt,
                                     e.LGD
                                 });
                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<OverdraftLGDComputation>().Take(defaultCount).ToArray();

                    //var query = (from e in entityContext.Set<OverdraftLGDComputation>() select e);
                    //var ExportHandler = new ExcelService();
                    //var response = ExportHandler.Export(query.ToList(), path);

                    //return query.Take(defaultCount).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<OverdraftLGDComputation>().Take(defaultCount) select e);

                    return query.ToArray();
                }
            }
        }
    }
}
