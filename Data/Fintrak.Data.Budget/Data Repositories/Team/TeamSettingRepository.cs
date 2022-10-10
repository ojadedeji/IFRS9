using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Budget.Entities;
using Fintrak.Data.Budget.Contracts;

namespace Fintrak.Data.Budget
{
    [Export(typeof(ITeamSettingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TeamSettingRepository : DataRepositoryBase<TeamSetting>, ITeamSettingRepository
    {

        protected override TeamSetting AddEntity(BudgetContext entityContext, TeamSetting entity)
        {
            return entityContext.Set<TeamSetting>().Add(entity);
        }

        protected override TeamSetting UpdateEntity(BudgetContext entityContext, TeamSetting entity)
        {
            return (from e in entityContext.Set<TeamSetting>() 
                    where e.TeamSettingId == entity.TeamSettingId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<TeamSetting> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<TeamSetting>()
                   select e;
        }

        protected override TeamSetting GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<TeamSetting>()
                         where e.TeamSettingId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

      
    }
}
