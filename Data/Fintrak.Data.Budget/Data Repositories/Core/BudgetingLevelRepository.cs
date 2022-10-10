using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Budget.Entities;
using Fintrak.Data.Budget.Contracts;

namespace Fintrak.Data.Budget
{
    [Export(typeof(IBudgetingLevelRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BudgetingLevelRepository : DataRepositoryBase<BudgetingLevel>, IBudgetingLevelRepository
    {

        protected override BudgetingLevel AddEntity(BudgetContext entityContext, BudgetingLevel entity)
        {
            return entityContext.Set<BudgetingLevel>().Add(entity);
        }

        protected override BudgetingLevel UpdateEntity(BudgetContext entityContext, BudgetingLevel entity)
        {
            return (from e in entityContext.Set<BudgetingLevel>() 
                    where e.BudgetingLevelId == entity.BudgetingLevelId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<BudgetingLevel> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<BudgetingLevel>()
                   select e;
        }

        protected override BudgetingLevel GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<BudgetingLevel>()
                         where e.BudgetingLevelId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<BudgetingLevelInfo> GetBudgetingLevels(string year, string reviewCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.BudgetingLevelSet
                            join b in entityContext.ModuleSet on a.ModuleCode equals b.Code
                            join c in entityContext.TeamDefinitionSet on a.DefinitionCode equals c.Code into cparents
                            from cp in cparents.Where(cpt => (a.Year == cpt.Year && a.ReviewCode == cpt.ReviewCode)).DefaultIfEmpty()
                            where a.Year == year && a.ReviewCode == reviewCode 
                            select new BudgetingLevelInfo()
                            {
                                BudgetingLevel = a,
                                Module = b,
                                TeamDefinition = cp
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
