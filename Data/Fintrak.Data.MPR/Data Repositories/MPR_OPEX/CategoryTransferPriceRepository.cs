using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(ICategoryTransferPriceRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CategoryTransferPriceRepository : DataRepositoryBase<CategoryTransferPrice>, ICategoryTransferPriceRepository
    {

        protected override CategoryTransferPrice AddEntity(MPRContext entityContext, CategoryTransferPrice entity)
        {
            return entityContext.Set<CategoryTransferPrice>().Add(entity);
        }

        protected override CategoryTransferPrice UpdateEntity(MPRContext entityContext, CategoryTransferPrice entity)
        {
            return (from e in entityContext.Set<CategoryTransferPrice>()
                    where e.CategoryTransferPriceId == entity.CategoryTransferPriceId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CategoryTransferPrice> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<CategoryTransferPrice>()
                   select e;
        }

        protected override CategoryTransferPrice GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CategoryTransferPrice>()
                         where e.CategoryTransferPriceId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}
