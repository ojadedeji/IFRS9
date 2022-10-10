using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Data.Core.Contracts;
using Fintrak.Shared.Core.Entities;

namespace Fintrak.Data.Core
{
    [Export(typeof(IProductTypeRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProductTypeRepository : DataRepositoryBase<ProductType>, IProductTypeRepository
    {
        protected override ProductType AddEntity(CoreContext entityContext, ProductType entity)
        {
            return entityContext.Set<ProductType>().Add(entity);
        }

        protected override ProductType UpdateEntity(CoreContext entityContext, ProductType entity)
        {
            return (from e in entityContext.Set<ProductType>()
                    where e.ProductTypeId == entity.ProductTypeId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ProductType> GetEntities(CoreContext entityContext)
        {
            return from e in entityContext.Set<ProductType>()
                   select e;
        }

        protected override ProductType GetEntity(CoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ProductType>()
                         where e.ProductTypeId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}
