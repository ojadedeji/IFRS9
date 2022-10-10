using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ICollateralCategoryRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CollateralCategoryRepository : DataRepositoryBase<CollateralCategory>, ICollateralCategoryRepository
    {

        protected override CollateralCategory AddEntity(IFRSContext entityContext, CollateralCategory entity)
        {
            return entityContext.Set<CollateralCategory>().Add(entity);
        }

        protected override CollateralCategory UpdateEntity(IFRSContext entityContext, CollateralCategory entity)
        {
            return (from e in entityContext.Set<CollateralCategory>()
                    where e.CollateralCategoryId == entity.CollateralCategoryId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CollateralCategory> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<CollateralCategory>()
                   select e;
        }

        protected override CollateralCategory GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CollateralCategory>()
                         where e.CollateralCategoryId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
 
    }
}
