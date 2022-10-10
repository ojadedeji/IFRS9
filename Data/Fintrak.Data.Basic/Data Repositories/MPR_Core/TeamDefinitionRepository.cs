using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;
using Fintrak.Shared.Basic.Framework;

namespace Fintrak.Data.Basic
{
    [Export(typeof(ITeamDefinitionRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TeamDefinitionRepository : DataRepositoryBase<TeamDefinition>, ITeamDefinitionRepository
    {

        protected override TeamDefinition AddEntity(BasicContext entityContext, TeamDefinition entity)
        {
            return entityContext.Set<TeamDefinition>().Add(entity);
        }

        protected override TeamDefinition UpdateEntity(BasicContext entityContext, TeamDefinition entity)
        {
            return (from e in entityContext.Set<TeamDefinition>() 
                    where e.TeamDefinitionId == entity.TeamDefinitionId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<TeamDefinition> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<TeamDefinition>()
                   select e;
        }

        protected override TeamDefinition GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<TeamDefinition>()
                         where e.TeamDefinitionId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
