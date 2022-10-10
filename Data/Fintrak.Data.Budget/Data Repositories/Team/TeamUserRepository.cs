using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Budget.Entities;
using Fintrak.Data.Budget.Contracts;

namespace Fintrak.Data.Budget
{
    [Export(typeof(ITeamUserRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TeamUserRepository : DataRepositoryBase<TeamUser>, ITeamUserRepository
    {

        protected override TeamUser AddEntity(BudgetContext entityContext, TeamUser entity)
        {
            return entityContext.Set<TeamUser>().Add(entity);
        }

        protected override TeamUser UpdateEntity(BudgetContext entityContext, TeamUser entity)
        {
            return (from e in entityContext.Set<TeamUser>() 
                    where e.TeamUserId == entity.TeamUserId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<TeamUser> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<TeamUser>()
                   select e;
        }

        protected override TeamUser GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<TeamUser>()
                         where e.TeamUserId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

      
    }
}
