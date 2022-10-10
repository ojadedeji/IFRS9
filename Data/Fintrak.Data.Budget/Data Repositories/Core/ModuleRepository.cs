using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Budget.Entities;
using Fintrak.Data.Budget.Contracts;

namespace Fintrak.Data.Budget
{
    [Export(typeof(IModuleRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ModuleRepository : DataRepositoryBase<Module>, IModuleRepository
    {

        protected override Module AddEntity(BudgetContext entityContext, Module entity)
        {
            return entityContext.Set<Module>().Add(entity);
        }

        protected override Module UpdateEntity(BudgetContext entityContext, Module entity)
        {
            return (from e in entityContext.Set<Module>() 
                    where e.ModuleId == entity.ModuleId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Module> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<Module>()
                   select e;
        }

        protected override Module GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<Module>()
                         where e.ModuleId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

      
    }
}
