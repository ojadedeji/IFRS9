using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IRevenueBudgetRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RevenueBudgetRepository : DataRepositoryBase<RevenueBudget>, IRevenueBudgetRepository
    {

        protected override RevenueBudget AddEntity(BasicContext entityContext, RevenueBudget entity)
        {
            return entityContext.Set<RevenueBudget>().Add(entity);
        }

        protected override RevenueBudget UpdateEntity(BasicContext entityContext, RevenueBudget entity)
        {
            return (from e in entityContext.Set<RevenueBudget>()
                    where e.BudgetId == entity.BudgetId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<RevenueBudget> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<RevenueBudget>()
                   select e;
        }

        protected override RevenueBudget GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<RevenueBudget>()
                         where e.BudgetId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }


        public IEnumerable<RevenueBudget> GetRevenueBudgets(string year)
        {
            using (BasicContext entityContext = new BasicContext())
            {
                var query = from a in entityContext.RevenueBudgetSet
                            where a.Year == year
                            select a;

                return query.ToFullyLoaded();
            }
        }

      
    }
}
