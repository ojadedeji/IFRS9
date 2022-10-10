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
    [Export(typeof(ITeamClassificationTypeRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TeamClassificationTypeRepository : DataRepositoryBase<TeamClassificationType>, ITeamClassificationTypeRepository
    {

        protected override TeamClassificationType AddEntity(MPRContext entityContext, TeamClassificationType entity)
        {
            return entityContext.Set<TeamClassificationType>().Add(entity);
        }

        protected override TeamClassificationType UpdateEntity(MPRContext entityContext, TeamClassificationType entity)
        {
            return (from e in entityContext.Set<TeamClassificationType>() 
                    where e.TeamClassificationTypeId == entity.TeamClassificationTypeId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<TeamClassificationType> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<TeamClassificationType>()
                   select e;
        }

        protected override TeamClassificationType GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<TeamClassificationType>()
                         where e.TeamClassificationTypeId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
