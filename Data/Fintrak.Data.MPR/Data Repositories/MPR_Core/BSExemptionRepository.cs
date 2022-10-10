using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IBSExemptionRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BSExemptionRepository : DataRepositoryBase<BSExemption>, IBSExemptionRepository
    {

        protected override BSExemption AddEntity(MPRContext entityContext, BSExemption entity)
        {
            return entityContext.Set<BSExemption>().Add(entity);
        }

        protected override BSExemption UpdateEntity(MPRContext entityContext, BSExemption entity)
        {
            return (from e in entityContext.Set<BSExemption>() 
                    where e.BSExemptionId == entity.BSExemptionId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<BSExemption> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<BSExemption>()
                   select e;
        }

        protected override BSExemption GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<BSExemption>()
                         where e.BSExemptionId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

       
      
    }
}
