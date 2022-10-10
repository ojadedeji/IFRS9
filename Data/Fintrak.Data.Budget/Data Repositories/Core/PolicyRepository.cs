using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Budget.Entities;
using Fintrak.Data.Budget.Contracts;

namespace Fintrak.Data.Budget
{
    [Export(typeof(IPolicyRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PolicyRepository : DataRepositoryBase<Policy>, IPolicyRepository
    {

        protected override Policy AddEntity(BudgetContext entityContext, Policy entity)
        {
            return entityContext.Set<Policy>().Add(entity);
        }

        protected override Policy UpdateEntity(BudgetContext entityContext, Policy entity)
        {
            return (from e in entityContext.Set<Policy>() 
                    where e.PolicyId == entity.PolicyId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Policy> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<Policy>()
                   select e;
        }

        protected override Policy GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<Policy>()
                         where e.PolicyId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<PolicyInfo> GetPolicies()
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.PolicySet
                            join b in entityContext.ModuleSet on a.ModuleCode equals b.Code

                            select new PolicyInfo()
                            {
                                Policy = a,
                                Module = b
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
