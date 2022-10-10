using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IServiceseRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ServiceseRepository : DataRepositoryBase<Servicese>, IServiceseRepository
    {

        protected override Servicese AddEntity(MPRContext entityContext, Servicese entity)
        {
            return entityContext.Set<Servicese>().Add(entity);
        }

        protected override Servicese UpdateEntity(MPRContext entityContext, Servicese entity)
        {
            return (from e in entityContext.Set<Servicese>()
                    where e.ServicesId == entity.ServicesId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Servicese> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<Servicese>()
                   select e;
        }

        protected override Servicese GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<Servicese>()
                         where e.ServicesId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}
