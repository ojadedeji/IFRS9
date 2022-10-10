using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(ISbuTypeRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SbuTypeRepository : DataRepositoryBase<SbuType>, ISbuTypeRepository
    {

        protected override SbuType AddEntity(MPRContext entityContext, SbuType entity)
        {
            return entityContext.Set<SbuType>().Add(entity);
        }

        protected override SbuType UpdateEntity(MPRContext entityContext, SbuType entity)
        {
            return (from e in entityContext.Set<SbuType>()
                    where e.SbuTypeId == entity.SbuTypeId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<SbuType> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<SbuType>()
                   select e;
        }

        protected override SbuType GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<SbuType>()
                         where e.SbuTypeId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}
