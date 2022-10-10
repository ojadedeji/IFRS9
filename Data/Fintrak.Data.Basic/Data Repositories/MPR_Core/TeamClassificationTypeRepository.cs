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
    [Export(typeof(ITeamClassificationTypeRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TeamClassificationTypeRepository : DataRepositoryBase<TeamClassificationType>, ITeamClassificationTypeRepository
    {

        protected override TeamClassificationType AddEntity(BasicContext entityContext, TeamClassificationType entity)
        {
            return entityContext.Set<TeamClassificationType>().Add(entity);
        }

        protected override TeamClassificationType UpdateEntity(BasicContext entityContext, TeamClassificationType entity)
        {
            return (from e in entityContext.Set<TeamClassificationType>() 
                    where e.TeamClassificationTypeId == entity.TeamClassificationTypeId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<TeamClassificationType> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<TeamClassificationType>()
                   select e;
        }

        protected override TeamClassificationType GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<TeamClassificationType>()
                         where e.TeamClassificationTypeId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
