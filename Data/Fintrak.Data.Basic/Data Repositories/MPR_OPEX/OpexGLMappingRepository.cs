using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IOpexGLMappingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OpexGLMappingRepository : DataRepositoryBase<OpexGLMapping>, IOpexGLMappingRepository
    {

        protected override OpexGLMapping AddEntity(BasicContext entityContext, OpexGLMapping entity)
        {
            return entityContext.Set<OpexGLMapping>().Add(entity);
        }

        protected override OpexGLMapping UpdateEntity(BasicContext entityContext, OpexGLMapping entity)
        {
            return (from e in entityContext.Set<OpexGLMapping>()
                    where e.GLMappingId == entity.GLMappingId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<OpexGLMapping> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<OpexGLMapping>()
                   select e;
        }

        protected override OpexGLMapping GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<OpexGLMapping>()
                         where e.GLMappingId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
