using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Data.SystemCore.Contracts;
using Fintrak.Shared.SystemCore.Entities;

namespace Fintrak.Data.SystemCore
{
    [Export(typeof(IErrorTrackerRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ErrorTrackerRepository : DataRepositoryBase<ErrorTracker>, IErrorTrackerRepository
    {

        protected override ErrorTracker AddEntity(SystemCoreContext entityContext, ErrorTracker entity)
        {
            return entityContext.Set<ErrorTracker>().Add(entity);
        }

        protected override ErrorTracker UpdateEntity(SystemCoreContext entityContext, ErrorTracker entity)
        {
            return (from e in entityContext.Set<ErrorTracker>() 
                    where e.ErrorTrackerId == entity.ErrorTrackerId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ErrorTracker> GetEntities(SystemCoreContext entityContext)
        {
            return from e in entityContext.Set<ErrorTracker>()
                   select e;
        }

        protected override ErrorTracker GetEntity(SystemCoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ErrorTracker>()
                         where e.ErrorTrackerId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
