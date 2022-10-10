using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Budget.Entities;
using Fintrak.Data.Budget.Contracts;

namespace Fintrak.Data.Budget
{
    [Export(typeof(IPrimaryLockRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PrimaryLockRepository : DataRepositoryBase<PrimaryLock>, IPrimaryLockRepository
    {

        protected override PrimaryLock AddEntity(BudgetContext entityContext, PrimaryLock entity)
        {
            return entityContext.Set<PrimaryLock>().Add(entity);
        }

        protected override PrimaryLock UpdateEntity(BudgetContext entityContext, PrimaryLock entity)
        {
            return (from e in entityContext.Set<PrimaryLock>() 
                    where e.PrimaryLockId == entity.PrimaryLockId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<PrimaryLock> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<PrimaryLock>()
                   select e;
        }

        protected override PrimaryLock GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<PrimaryLock>()
                         where e.PrimaryLockId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<PrimaryLockInfo> GetPrimaryLocks(string year, string reviewCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.PrimaryLockSet
                            join b in entityContext.TeamDefinitionSet on a.DefinitionCode equals b.Code into bparents
                            from bp in bparents.Where(bpt => (a.Year == bpt.Year && bpt.ReviewCode == reviewCode)).DefaultIfEmpty()
                            join c in entityContext.TeamSet on a.MisCode equals c.Code into cparents
                            from cp in cparents.Where(cpt => (a.Year == cpt.Year && cpt.ReviewCode == reviewCode)).DefaultIfEmpty()

                            where a.Year == year 

                            select new PrimaryLockInfo()
                            {
                                PrimaryLock = a,
                                TeamDefinition = bp,
                                Team = cp
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
