using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Budget.Entities;
using Fintrak.Data.Budget.Contracts;

namespace Fintrak.Data.Budget
{
    [Export(typeof(ISecondaryLockLevelRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SecondaryLockLevelRepository : DataRepositoryBase<SecondaryLockLevel>, ISecondaryLockLevelRepository
    {

        protected override SecondaryLockLevel AddEntity(BudgetContext entityContext, SecondaryLockLevel entity)
        {
            return entityContext.Set<SecondaryLockLevel>().Add(entity);
        }

        protected override SecondaryLockLevel UpdateEntity(BudgetContext entityContext, SecondaryLockLevel entity)
        {
            return (from e in entityContext.Set<SecondaryLockLevel>() 
                    where e.SecondaryLockLevelId == entity.SecondaryLockLevelId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<SecondaryLockLevel> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<SecondaryLockLevel>()
                   select e;
        }

        protected override SecondaryLockLevel GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<SecondaryLockLevel>()
                         where e.SecondaryLockLevelId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<SecondaryLockLevelInfo> GetSecondaryLockLevels(string year, string reviewCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.SecondaryLockLevelSet
                            join b in entityContext.TeamDefinitionSet on a.DefinitionCode equals b.Code into bparents
                            from bp in bparents.Where(bpt => (bpt.Year == year && bpt.ReviewCode == reviewCode)).DefaultIfEmpty()
                            join c in entityContext.ModuleSet on a.ModuleCode equals c.Code

                            select new SecondaryLockLevelInfo()
                            {
                                SecondaryLockLevel = a,
                                TeamDefinition = bp,
                                Module = c
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
