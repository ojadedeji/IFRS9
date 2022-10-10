using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Budget.Entities;
using Fintrak.Data.Budget.Contracts;

namespace Fintrak.Data.Budget
{
    [Export(typeof(ICurrencyRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CurrencyRepository : DataRepositoryBase<Currency>, ICurrencyRepository
    {

        protected override Currency AddEntity(BudgetContext entityContext, Currency entity)
        {
            return entityContext.Set<Currency>().Add(entity);
        }

        protected override Currency UpdateEntity(BudgetContext entityContext, Currency entity)
        {
            return (from e in entityContext.Set<Currency>() 
                    where e.CurrencyId == entity.CurrencyId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Currency> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<Currency>()
                   select e;
        }

        protected override Currency GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<Currency>()
                         where e.CurrencyId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

      
    }
}
