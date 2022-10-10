using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Budget.Entities;
using Fintrak.Data.Budget.Contracts;

namespace Fintrak.Data.Budget
{
    [Export(typeof(IModificationLevelRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ModificationLevelRepository : DataRepositoryBase<ModificationLevel>, IModificationLevelRepository
    {

        protected override ModificationLevel AddEntity(BudgetContext entityContext, ModificationLevel entity)
        {
            return entityContext.Set<ModificationLevel>().Add(entity);
        }

        protected override ModificationLevel UpdateEntity(BudgetContext entityContext, ModificationLevel entity)
        {
            return (from e in entityContext.Set<ModificationLevel>() 
                    where e.ModificationLevelId == entity.ModificationLevelId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ModificationLevel> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<ModificationLevel>()
                   select e;
        }

        protected override ModificationLevel GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ModificationLevel>()
                         where e.ModificationLevelId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ModificationLevelInfo> GetModificationLevels(string year, string reviewCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.ModificationLevelSet
                            join b in entityContext.ModuleSet on a.ModuleCode equals b.Code
                            join c in entityContext.TeamDefinitionSet on a.DefinitionCode equals c.Code

                            where c.Year == year && c.ReviewCode == reviewCode
                        
                            select new ModificationLevelInfo()
                            {
                                ModificationLevel = a,
                                Module = b,
                                TeamDefinition = c
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
