using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IOpexBasisMappingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OpexBasisMappingRepository : DataRepositoryBase<OpexBasisMapping>, IOpexBasisMappingRepository
    {

        protected override OpexBasisMapping AddEntity(MPRContext entityContext, OpexBasisMapping entity)
        {
            return entityContext.Set<OpexBasisMapping>().Add(entity);
        }

        protected override OpexBasisMapping UpdateEntity(MPRContext entityContext, OpexBasisMapping entity)
        {
            return (from e in entityContext.Set<OpexBasisMapping>()
                    where e.OpexBasisMappingId == entity.OpexBasisMappingId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<OpexBasisMapping> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<OpexBasisMapping>()
                   select e;
        }

        protected override OpexBasisMapping GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<OpexBasisMapping>()
                         where e.OpexBasisMappingId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
