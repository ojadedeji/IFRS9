using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IActivityBaseRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ActivityBaseRepository : DataRepositoryBase<ActivityBase>, IActivityBaseRepository
    {

        protected override ActivityBase AddEntity(MPRContext entityContext, ActivityBase entity)
        {
            return entityContext.Set<ActivityBase>().Add(entity);
        }

        protected override ActivityBase UpdateEntity(MPRContext entityContext, ActivityBase entity)
        {
            return (from e in entityContext.Set<ActivityBase>()
                    where e.ActivityBaseId == entity.ActivityBaseId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ActivityBase> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<ActivityBase>()
                   select e;
        }

        protected override ActivityBase GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ActivityBase>()
                         where e.ActivityBaseId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
