using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IReconciliationRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ReconciliationRepository : DataRepositoryBase<Reconciliation>, IReconciliationRepository
    {
        protected override Reconciliation AddEntity(IFRSContext entityContext, Reconciliation entity)
        {
            return entityContext.Set<Reconciliation>().Add(entity);
        }

        protected override Reconciliation UpdateEntity(IFRSContext entityContext, Reconciliation entity)
        {
            return (from e in entityContext.Set<Reconciliation>()
                    where e.ReconciliationId == entity.ReconciliationId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Reconciliation> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<Reconciliation>()
                   select e;
        }

        protected override Reconciliation GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<Reconciliation>()
                         where e.ReconciliationId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}