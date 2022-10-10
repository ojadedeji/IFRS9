using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Core.Entities;
using Fintrak.Data.Core.Contracts;

namespace Fintrak.Data.Core
{
    [Export(typeof(IProcessRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProcessRepository : DataRepositoryBase<Processes>, IProcessRepository
    {
        protected override Processes AddEntity(CoreContext entityContext, Processes entity)
        {
            return entityContext.Set<Processes>().Add(entity);
        }

        protected override Processes UpdateEntity(CoreContext entityContext, Processes entity)
        {
            return (from e in entityContext.Set<Processes>()
                    where e.ProcessId == entity.ProcessId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Processes> GetEntities(CoreContext entityContext)
        {
            return from e in entityContext.Set<Processes>()
                   select e;
        }

        protected override Processes GetEntity(CoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<Processes>()
                         where e.ProcessId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ProcessInfo> GetProcesses()
        {
            using (CoreContext entityContext = new CoreContext())
            {
                var query = from a in entityContext.ProcessSet
                           
                            select new ProcessInfo()
                            {
                                Processes = a
                            };

                return query.ToFullyLoaded();
            }
        }
    }
}
