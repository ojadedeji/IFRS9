using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IActivityBaseRatioRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ActivityBaseRatioRepository : DataRepositoryBase<ActivityBaseRatio>, IActivityBaseRatioRepository
    {

        protected override ActivityBaseRatio AddEntity(MPRContext entityContext, ActivityBaseRatio entity)
        {
            return entityContext.Set<ActivityBaseRatio>().Add(entity);
        }

        protected override ActivityBaseRatio UpdateEntity(MPRContext entityContext, ActivityBaseRatio entity)
        {
            return (from e in entityContext.Set<ActivityBaseRatio>()
                    where e.ActivityBaseRatioId == entity.ActivityBaseRatioId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ActivityBaseRatio> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<ActivityBaseRatio>()
                   select e;
        }

        protected override ActivityBaseRatio GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ActivityBaseRatio>()
                         where e.ActivityBaseRatioId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
