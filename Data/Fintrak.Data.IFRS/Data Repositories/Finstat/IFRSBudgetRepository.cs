using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IIFRSBudgetRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IFRSBudgetRepository : DataRepositoryBase<IFRSBudget>, IIFRSBudgetRepository
    {

        protected override IFRSBudget AddEntity(IFRSContext entityContext, IFRSBudget entity)
        {
            return entityContext.Set<IFRSBudget>().Add(entity);
        }

        protected override IFRSBudget UpdateEntity(IFRSContext entityContext, IFRSBudget entity)
        {
            return (from e in entityContext.Set<IFRSBudget>() 
                    where e.IFRSBudgetId == entity.IFRSBudgetId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IFRSBudget> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<IFRSBudget>()
                   select e;
        }

        protected override IFRSBudget GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IFRSBudget>()
                         where e.IFRSBudgetId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}
