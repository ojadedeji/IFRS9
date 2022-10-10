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
    [Export(typeof(ITeamClassificationMapRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TeamClassificationMapRepository : DataRepositoryBase<TeamClassificationMap>, ITeamClassificationMapRepository
    {

        protected override TeamClassificationMap AddEntity(MPRContext entityContext, TeamClassificationMap entity)
        {
            return entityContext.Set<TeamClassificationMap>().Add(entity);
        }

        protected override TeamClassificationMap UpdateEntity(MPRContext entityContext, TeamClassificationMap entity)
        {
            return (from e in entityContext.Set<TeamClassificationMap>() 
                    where e.TeamClassificationMapId == entity.TeamClassificationMapId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<TeamClassificationMap> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<TeamClassificationMap>()
                   select e;
        }

        protected override TeamClassificationMap GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<TeamClassificationMap>()
                         where e.TeamClassificationMapId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
