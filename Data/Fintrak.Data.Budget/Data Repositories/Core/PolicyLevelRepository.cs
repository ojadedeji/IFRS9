using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Budget.Entities;
using Fintrak.Data.Budget.Contracts;

namespace Fintrak.Data.Budget
{
    [Export(typeof(IPolicyLevelRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PolicyLevelRepository : DataRepositoryBase<PolicyLevel>, IPolicyLevelRepository
    {

        protected override PolicyLevel AddEntity(BudgetContext entityContext, PolicyLevel entity)
        {
            return entityContext.Set<PolicyLevel>().Add(entity);
        }

        protected override PolicyLevel UpdateEntity(BudgetContext entityContext, PolicyLevel entity)
        {
            return (from e in entityContext.Set<PolicyLevel>() 
                    where e.PolicyLevelId == entity.PolicyLevelId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<PolicyLevel> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<PolicyLevel>()
                   select e;
        }

        protected override PolicyLevel GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<PolicyLevel>()
                         where e.PolicyLevelId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<PolicyLevelInfo> GetPolicyLevels(string year, string reviewCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.PolicyLevelSet
                            join b in entityContext.ModuleSet on a.ModuleCode equals b.Code
                            join c in entityContext.TeamDefinitionSet on a.DefinitionCode equals c.Code into cparents
                            from cp in cparents.Where(cpt => (a.Year == cpt.Year && a.ReviewCode == cpt.ReviewCode)).DefaultIfEmpty()
                            join d in entityContext.PolicySet on a.PolicyCode equals d.Code
                            where a.Year == year && a.ReviewCode == reviewCode 
                            select new PolicyLevelInfo()
                            {
                                PolicyLevel = a,
                                Module = b,
                                TeamDefinition = cp,
                                Policy = d
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
