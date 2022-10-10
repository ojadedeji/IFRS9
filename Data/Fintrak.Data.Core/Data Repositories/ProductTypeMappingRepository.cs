using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Data.Core.Contracts;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Core.Entities;

namespace Fintrak.Data.Core
{
    [Export(typeof(IProductTypeMappingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProductTypeMappingRepository : DataRepositoryBase<ProductTypeMapping>, IProductTypeMappingRepository
    {
        protected override ProductTypeMapping AddEntity(CoreContext entityContext, ProductTypeMapping entity)
        {
            return entityContext.Set<ProductTypeMapping>().Add(entity);
        }

        protected override ProductTypeMapping UpdateEntity(CoreContext entityContext, ProductTypeMapping entity)
        {
            return (from e in entityContext.Set<ProductTypeMapping>()
                    where e.ProductTypeMappingId == entity.ProductTypeMappingId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ProductTypeMapping> GetEntities(CoreContext entityContext)
        {
            return from e in entityContext.Set<ProductTypeMapping>()
                   select e;
        }

        protected override ProductTypeMapping GetEntity(CoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ProductTypeMapping>()
                         where e.ProductTypeMappingId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ProductTypeMappingInfo> GetProductTypeMappingByProduct(string productCode)
        {
            using (CoreContext entityContext = new CoreContext())
            {
                var query = from a in entityContext.ProductTypeMappingSet
                            join b in entityContext.ProductSet on a.ProductCode equals b.Code
                            join c in entityContext.ProductTypeSet on a.ProductType equals c.Name
                            where a.ProductCode == productCode
                            select new ProductTypeMappingInfo()
                            {
                                ProductTypeMapping = a,
                                Product = b,
                                ProductType = c
                            };

                return query.ToFullyLoaded();
            }
        }
    }
}
