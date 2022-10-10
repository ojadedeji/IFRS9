using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.SystemCore.Entities;
using Fintrak.Data.SystemCore.Contracts;

namespace Fintrak.Data.SystemCore
{
    [Export(typeof(IModuleRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ModuleRepository : DataRepositoryBase<Module>, IModuleRepository
    {
        protected override Module AddEntity(SystemCoreContext entityContext, Module entity)
        {
            return entityContext.Set<Module>().Add(entity);
        }

        protected override Module UpdateEntity(SystemCoreContext entityContext, Module entity)
        {
            return (from e in entityContext.Set<Module>()
                    where e.ModuleId == entity.ModuleId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Module> GetEntities(SystemCoreContext entityContext)
        {
            return from e in entityContext.Set<Module>()
                   select e;
        }

        protected override Module GetEntity(SystemCoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<Module>()
                         where e.ModuleId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
        public IEnumerable<ModuleInfo> GetModules()
        {
            using (SystemCoreContext entityContext = new SystemCoreContext())
            {
                var query = from a in entityContext.ModuleSet
                            join d in entityContext.SolutionSet on a.SolutionId equals d.SolutionId
                            select new ModuleInfo()
                            {
                                Module = a,
                                Solution = d
                            };

                return query.ToFullyLoaded();
            }
        }

    }
}
