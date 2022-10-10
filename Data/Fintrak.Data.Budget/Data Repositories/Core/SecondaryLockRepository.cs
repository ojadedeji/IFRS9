using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Budget.Entities;
using Fintrak.Data.Budget.Contracts;

namespace Fintrak.Data.Budget
{
    [Export(typeof(ISecondaryLockRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SecondaryLockRepository : DataRepositoryBase<SecondaryLock>, ISecondaryLockRepository
    {

        protected override SecondaryLock AddEntity(BudgetContext entityContext, SecondaryLock entity)
        {
            return entityContext.Set<SecondaryLock>().Add(entity);
        }

        protected override SecondaryLock UpdateEntity(BudgetContext entityContext, SecondaryLock entity)
        {
            return (from e in entityContext.Set<SecondaryLock>() 
                    where e.SecondaryLockId == entity.SecondaryLockId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<SecondaryLock> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<SecondaryLock>()
                   select e;
        }

        protected override SecondaryLock GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<SecondaryLock>()
                         where e.SecondaryLockId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<SecondaryLockInfo> GetSecondaryLocks(string year, string reviewCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.SecondaryLockSet
                            join b in entityContext.TeamDefinitionSet on a.DefinitionCode equals b.Code into bparents
                            from bp in bparents.Where(bpt => (a.Year == bpt.Year && bpt.ReviewCode == reviewCode)).DefaultIfEmpty()
                            join c in entityContext.TeamSet on a.MisCode equals c.Code into cparents
                            from cp in cparents.Where(cpt => (a.Year == cpt.Year && cpt.ReviewCode == reviewCode)).DefaultIfEmpty()
                            join d in entityContext.ModuleSet on a.ModuleCode equals d.Code
                            where a.Year == year 

                            select new SecondaryLockInfo()
                            {
                                SecondaryLock = a,
                                TeamDefinition = bp,
                                Team = cp,
                                Module =  d
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
