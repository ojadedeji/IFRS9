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
    [Export(typeof(ICheckDataAvailabilityRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CheckDataAvailabilityRepository : DataRepositoryBase<CheckDataAvailability>, ICheckDataAvailabilityRepository
    {
        protected override CheckDataAvailability AddEntity(CoreContext entityContext, CheckDataAvailability entity)
        {
            return entityContext.Set<CheckDataAvailability>().Add(entity);
        }

        protected override CheckDataAvailability UpdateEntity(CoreContext entityContext, CheckDataAvailability entity)
        {
            return (from e in entityContext.Set<CheckDataAvailability>()
                    where e.CheckDataId == entity.CheckDataId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CheckDataAvailability> GetEntities(CoreContext entityContext)
        {
            return from e in entityContext.Set<CheckDataAvailability>()
                   select e;
        }

        protected override CheckDataAvailability GetEntity(CoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CheckDataAvailability>()
                         where e.CheckDataId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}