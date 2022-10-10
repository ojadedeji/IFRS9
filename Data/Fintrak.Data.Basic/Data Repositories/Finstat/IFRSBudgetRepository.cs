using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IIFRSBudgetRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IFRSBudgetRepository : DataRepositoryBase<IFRSBudget>, IIFRSBudgetRepository
    {

        protected override IFRSBudget AddEntity(BasicContext entityContext, IFRSBudget entity)
        {
            return entityContext.Set<IFRSBudget>().Add(entity);
        }

        protected override IFRSBudget UpdateEntity(BasicContext entityContext, IFRSBudget entity)
        {
            return (from e in entityContext.Set<IFRSBudget>() 
                    where e.IFRSBudgetId == entity.IFRSBudgetId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IFRSBudget> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<IFRSBudget>()
                   select e;
        }

        protected override IFRSBudget GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IFRSBudget>()
                         where e.IFRSBudgetId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}
