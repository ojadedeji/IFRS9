using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(ILowCostRemapRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LowCostRemapRepository : DataRepositoryBase<LowCostRemap>, ILowCostRemapRepository
    {

        protected override LowCostRemap AddEntity(MPRContext entityContext, LowCostRemap entity)
        {
            return entityContext.Set<LowCostRemap>().Add(entity);
        }

        protected override LowCostRemap UpdateEntity(MPRContext entityContext, LowCostRemap entity)
        {
            return (from e in entityContext.Set<LowCostRemap>()
                    where e.LowCostRemapId == entity.LowCostRemapId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<LowCostRemap> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<LowCostRemap>()
                   select e;
        }

        protected override LowCostRemap GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<LowCostRemap>()
                         where e.LowCostRemapId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
