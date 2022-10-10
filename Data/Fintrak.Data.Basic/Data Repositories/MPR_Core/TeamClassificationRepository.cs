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
    [Export(typeof(ITeamClassificationRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TeamClassificationRepository : DataRepositoryBase<TeamClassification>, ITeamClassificationRepository
    {

        protected override TeamClassification AddEntity(BasicContext entityContext, TeamClassification entity)
        {
            return entityContext.Set<TeamClassification>().Add(entity);
        }

        protected override TeamClassification UpdateEntity(BasicContext entityContext, TeamClassification entity)
        {
            return (from e in entityContext.Set<TeamClassification>() 
                    where e.TeamClassificationId == entity.TeamClassificationId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<TeamClassification> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<TeamClassification>()
                   select e;
        }

        protected override TeamClassification GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<TeamClassification>()
                         where e.TeamClassificationId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
