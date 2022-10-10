using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Data.Core.Contracts;
using Fintrak.Shared.Core.Entities;

namespace Fintrak.Data.Core
{
    [Export(typeof(ICurrencyRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CurrencyRepository : DataRepositoryBase<Currency>, ICurrencyRepository
    {
        protected override Currency AddEntity(CoreContext entityContext, Currency entity)
        {
            return entityContext.Set<Currency>().Add(entity);
        }

        protected override Currency UpdateEntity(CoreContext entityContext, Currency entity)
        {
            return (from e in entityContext.Set<Currency>()
                    where e.CurrencyId == entity.CurrencyId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Currency> GetEntities(CoreContext entityContext)
        {
            return from e in entityContext.Set<Currency>()
                   select e;
        }



        protected override Currency GetEntity(CoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<Currency>()
                         where e.CurrencyId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}
