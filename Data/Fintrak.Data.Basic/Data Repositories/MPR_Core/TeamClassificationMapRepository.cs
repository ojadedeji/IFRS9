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
    [Export(typeof(ITeamClassificationMapRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TeamClassificationMapRepository : DataRepositoryBase<TeamClassificationMap>, ITeamClassificationMapRepository
    {

        protected override TeamClassificationMap AddEntity(BasicContext entityContext, TeamClassificationMap entity)
        {
            return entityContext.Set<TeamClassificationMap>().Add(entity);
        }

        protected override TeamClassificationMap UpdateEntity(BasicContext entityContext, TeamClassificationMap entity)
        {
            return (from e in entityContext.Set<TeamClassificationMap>() 
                    where e.TeamClassificationMapId == entity.TeamClassificationMapId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<TeamClassificationMap> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<TeamClassificationMap>()
                   select e;
        }

        protected override TeamClassificationMap GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<TeamClassificationMap>()
                         where e.TeamClassificationMapId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
