using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Data.Core.Contracts;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Core.Entities;

namespace Fintrak.Data.Core
{
    [Export(typeof(IGLDefinitionRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GLDefinitionRepository : DataRepositoryBase<GLDefinition>, IGLDefinitionRepository
    {
        protected override GLDefinition AddEntity(CoreContext entityContext, GLDefinition entity)
        {
            return entityContext.Set<GLDefinition>().Add(entity);
        }

        protected override GLDefinition UpdateEntity(CoreContext entityContext, GLDefinition entity)
        {
            return (from e in entityContext.Set<GLDefinition>()
                    where e.GLDefinitionId == entity.GLDefinitionId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<GLDefinition> GetEntities(CoreContext entityContext)
        {
            return from e in entityContext.Set<GLDefinition>()
                   select e;
        }

        protected override GLDefinition GetEntity(CoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<GLDefinition>()
                         where e.GLDefinitionId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}
