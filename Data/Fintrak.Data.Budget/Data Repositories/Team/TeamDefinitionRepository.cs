using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Budget.Entities;
using Fintrak.Data.Budget.Contracts;
using Fintrak.Shared.Budget.Framework.Enums;

namespace Fintrak.Data.Budget
{
    [Export(typeof(ITeamDefinitionRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TeamDefinitionRepository : DataRepositoryBase<TeamDefinition>, ITeamDefinitionRepository
    {

        protected override TeamDefinition AddEntity(BudgetContext entityContext, TeamDefinition entity)
        {
            return entityContext.Set<TeamDefinition>().Add(entity);
        }

        protected override TeamDefinition UpdateEntity(BudgetContext entityContext, TeamDefinition entity)
        {
            return (from e in entityContext.Set<TeamDefinition>() 
                    where e.TeamDefinitionId == entity.TeamDefinitionId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<TeamDefinition> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<TeamDefinition>()
                   select e;
        }

        protected override TeamDefinition GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<TeamDefinition>()
                         where e.TeamDefinitionId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<TeamDefinition> GetTeamDefinitions(string year, string reviewCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.TeamDefinitionSet
                            where a.Year == year && a.ReviewCode == reviewCode
                                select a;

                return query.ToFullyLoaded();
            }
        }
      
    }
}
