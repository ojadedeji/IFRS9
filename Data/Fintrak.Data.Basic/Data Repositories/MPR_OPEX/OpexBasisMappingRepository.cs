using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IOpexBasisMappingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OpexBasisMappingRepository : DataRepositoryBase<OpexBasisMapping>, IOpexBasisMappingRepository
    {

        protected override OpexBasisMapping AddEntity(BasicContext entityContext, OpexBasisMapping entity)
        {
            return entityContext.Set<OpexBasisMapping>().Add(entity);
        }

        protected override OpexBasisMapping UpdateEntity(BasicContext entityContext, OpexBasisMapping entity)
        {
            return (from e in entityContext.Set<OpexBasisMapping>()
                    where e.OpexBasisMappingId == entity.OpexBasisMappingId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<OpexBasisMapping> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<OpexBasisMapping>()
                   select e;
        }

        protected override OpexBasisMapping GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<OpexBasisMapping>()
                         where e.OpexBasisMappingId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
