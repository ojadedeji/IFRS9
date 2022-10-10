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
    [Export(typeof(ITeamClassificationRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TeamClassificationRepository : DataRepositoryBase<TeamClassification>, ITeamClassificationRepository
    {

        protected override TeamClassification AddEntity(BudgetContext entityContext, TeamClassification entity)
        {
            return entityContext.Set<TeamClassification>().Add(entity);
        }

        protected override TeamClassification UpdateEntity(BudgetContext entityContext, TeamClassification entity)
        {
            return (from e in entityContext.Set<TeamClassification>() 
                    where e.TeamClassificationId == entity.TeamClassificationId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<TeamClassification> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<TeamClassification>()
                   select e;
        }

        protected override TeamClassification GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<TeamClassification>()
                         where e.TeamClassificationId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<TeamClassification> GetTeamClassifications(string year, string reviewCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.TeamClassificationSet
                            where a.Year == year && a.ReviewCode == reviewCode
                                select a;

                return query.ToFullyLoaded();
            }
        }
      
    }
}
