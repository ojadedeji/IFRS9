using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Core.Entities;
using Fintrak.Data.Core.Contracts;
using System.Data.SqlClient;

namespace Fintrak.Data.Core
{
    [Export(typeof(ICheckifrsDataAvailabilityRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CheckifrsDataAvailabilityRepository : DataRepositoryBase<CheckifrsDataAvailability>, ICheckifrsDataAvailabilityRepository
    {
        protected override CheckifrsDataAvailability AddEntity(CoreContext entityContext, CheckifrsDataAvailability entity)
        {
            return entityContext.Set<CheckifrsDataAvailability>().Add(entity);
        }

        protected override CheckifrsDataAvailability UpdateEntity(CoreContext entityContext, CheckifrsDataAvailability entity)
        {
            return (from e in entityContext.Set<CheckifrsDataAvailability>()
                    where e.CheckDataId == entity.CheckDataId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CheckifrsDataAvailability> GetEntities(CoreContext entityContext)
        {
            return from e in entityContext.Set<CheckifrsDataAvailability>()
                   select e;
        }

        protected override CheckifrsDataAvailability GetEntity(CoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CheckifrsDataAvailability>()
                         where e.CheckDataId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}