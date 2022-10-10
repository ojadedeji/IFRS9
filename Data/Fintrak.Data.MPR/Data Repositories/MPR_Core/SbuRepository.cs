using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(ISbuRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SbuRepository : DataRepositoryBase<Sbu>, ISbuRepository
    {

        protected override Sbu AddEntity(MPRContext entityContext, Sbu entity)
        {
            return entityContext.Set<Sbu>().Add(entity);
        }

        protected override Sbu UpdateEntity(MPRContext entityContext, Sbu entity)
        {
            return (from e in entityContext.Set<Sbu>()
                    where e.SbuId == entity.SbuId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Sbu> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<Sbu>()
                   select e;
        }

        protected override Sbu GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<Sbu>()
                         where e.SbuId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}
