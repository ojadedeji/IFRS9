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
    [Export(typeof(ITeamClassificationRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TeamClassificationRepository : DataRepositoryBase<TeamClassification>, ITeamClassificationRepository
    {

        protected override TeamClassification AddEntity(MPRContext entityContext, TeamClassification entity)
        {
            return entityContext.Set<TeamClassification>().Add(entity);
        }

        protected override TeamClassification UpdateEntity(MPRContext entityContext, TeamClassification entity)
        {
            return (from e in entityContext.Set<TeamClassification>() 
                    where e.TeamClassificationId == entity.TeamClassificationId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<TeamClassification> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<TeamClassification>()
                   select e;
        }

        protected override TeamClassification GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<TeamClassification>()
                         where e.TeamClassificationId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
