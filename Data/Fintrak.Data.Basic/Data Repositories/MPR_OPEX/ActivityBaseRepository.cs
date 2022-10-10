using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IActivityBaseRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ActivityBaseRepository : DataRepositoryBase<ActivityBase>, IActivityBaseRepository
    {

        protected override ActivityBase AddEntity(BasicContext entityContext, ActivityBase entity)
        {
            return entityContext.Set<ActivityBase>().Add(entity);
        }

        protected override ActivityBase UpdateEntity(BasicContext entityContext, ActivityBase entity)
        {
            return (from e in entityContext.Set<ActivityBase>()
                    where e.ActivityBaseId == entity.ActivityBaseId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ActivityBase> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<ActivityBase>()
                   select e;
        }

        protected override ActivityBase GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ActivityBase>()
                         where e.ActivityBaseId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
