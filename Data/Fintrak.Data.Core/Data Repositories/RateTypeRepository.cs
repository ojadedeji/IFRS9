using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Data.Core.Contracts;
using Fintrak.Shared.Core.Entities;

namespace Fintrak.Data.Core
{
    [Export(typeof(IRateTypeRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RateTypeRepository : DataRepositoryBase<RateType>, IRateTypeRepository
    {
        protected override RateType AddEntity(CoreContext entityContext, RateType entity)
        {
            return entityContext.Set<RateType>().Add(entity);
        }

        protected override RateType UpdateEntity(CoreContext entityContext, RateType entity)
        {
            return (from e in entityContext.Set<RateType>()
                    where e.RateTypeId == entity.RateTypeId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<RateType> GetEntities(CoreContext entityContext)
        {
            return from e in entityContext.Set<RateType>()
                   select e;
        }

        protected override RateType GetEntity(CoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<RateType>()
                         where e.RateTypeId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
        
    }
}
