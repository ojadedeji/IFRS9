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
    [Export(typeof(ITeamClassificationTypeRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TeamClassificationTypeRepository : DataRepositoryBase<TeamClassificationType>, ITeamClassificationTypeRepository
    {

        protected override TeamClassificationType AddEntity(BudgetContext entityContext, TeamClassificationType entity)
        {
            return entityContext.Set<TeamClassificationType>().Add(entity);
        }

        protected override TeamClassificationType UpdateEntity(BudgetContext entityContext, TeamClassificationType entity)
        {
            return (from e in entityContext.Set<TeamClassificationType>() 
                    where e.TeamClassificationTypeId == entity.TeamClassificationTypeId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<TeamClassificationType> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<TeamClassificationType>()
                   select e;
        }

        protected override TeamClassificationType GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<TeamClassificationType>()
                         where e.TeamClassificationTypeId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<TeamClassificationType> GetTeamClassificationTypes(string year, string reviewCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.TeamClassificationTypeSet
                            where a.Year == year && a.ReviewCode == reviewCode
                                select a;

                return query.ToFullyLoaded();
            }
        }
      
    }
}
