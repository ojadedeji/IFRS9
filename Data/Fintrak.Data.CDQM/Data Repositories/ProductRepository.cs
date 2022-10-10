using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.CDQM.Entities;
using Fintrak.Data.CDQM.Contracts;

namespace Fintrak.Data.CDQM
{
    [Export(typeof(ICDQMProductRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CDQMProductRepository : DataRepositoryBase<CDQMProduct>, ICDQMProductRepository
    {

        protected override CDQMProduct AddEntity(CDQMContext entityContext, CDQMProduct entity)
        {
            return entityContext.Set<CDQMProduct>().Add(entity);
        }

        protected override CDQMProduct UpdateEntity(CDQMContext entityContext, CDQMProduct entity)
        {
            return (from e in entityContext.Set<CDQMProduct>() 
                    where e.ProductId == entity.ProductId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CDQMProduct> GetEntities(CDQMContext entityContext)
        {
            return from e in entityContext.Set<CDQMProduct>()
                   select e;
        }

        protected override CDQMProduct GetEntity(CDQMContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CDQMProduct>()
                         where e.ProductId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
