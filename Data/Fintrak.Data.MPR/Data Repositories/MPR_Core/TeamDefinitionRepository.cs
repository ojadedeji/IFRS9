using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;
using Fintrak.Shared.MPR.Framework;

namespace Fintrak.Data.MPR
{
    [Export(typeof(ITeamDefinitionRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TeamDefinitionRepository : DataRepositoryBase<TeamDefinition>, ITeamDefinitionRepository
    {

        protected override TeamDefinition AddEntity(MPRContext entityContext, TeamDefinition entity)
        {
            return entityContext.Set<TeamDefinition>().Add(entity);
        }

        protected override TeamDefinition UpdateEntity(MPRContext entityContext, TeamDefinition entity)
        {
            return (from e in entityContext.Set<TeamDefinition>() 
                    where e.TeamDefinitionId == entity.TeamDefinitionId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<TeamDefinition> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<TeamDefinition>()
                   select e;
        }

        protected override TeamDefinition GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<TeamDefinition>()
                         where e.TeamDefinitionId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
