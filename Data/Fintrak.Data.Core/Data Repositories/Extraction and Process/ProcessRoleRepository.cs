using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Core.Entities;
using Fintrak.Data.Core.Contracts;

namespace Fintrak.Data.Core
{
    [Export(typeof(IProcessRoleRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProcessRoleRepository : DataRepositoryBase<ProcessRole>, IProcessRoleRepository
    {
        protected override ProcessRole AddEntity(CoreContext entityContext, ProcessRole entity)
        {
            return entityContext.Set<ProcessRole>().Add(entity);
        }

        protected override ProcessRole UpdateEntity(CoreContext entityContext, ProcessRole entity)
        {
            return (from e in entityContext.Set<ProcessRole>()
                    where e.ProcessRoleId == entity.ProcessRoleId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ProcessRole> GetEntities(CoreContext entityContext)
        {
            return from e in entityContext.Set<ProcessRole>()
                   select e;
        }

        protected override ProcessRole GetEntity(CoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ProcessRole>()
                         where e.ProcessRoleId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }



        public IEnumerable<ProcessRoleInfo> GetProcessRoles()
        {
            using (CoreContext entityContext = new CoreContext())
            {
                var query = from a in entityContext.ProcessRoleSet
                            join b in entityContext.ProcessSet on a.ProcessId equals b.ProcessId

                            select new ProcessRoleInfo()
                            {
                                ProcessRole = a,
                                Processes = b,

                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<ProcessRoleInfo> GetProcessRoleByProcess(int processId)
        {
            using (CoreContext entityContext = new CoreContext())
            {
                var query = from a in entityContext.ProcessRoleSet
                            join b in entityContext.ProcessSet on a.ProcessId equals b.ProcessId

                            where a.ProcessId == processId
                            select new ProcessRoleInfo()
                            {
                                ProcessRole = a,
                                Processes = b,

                            };

                return query.ToFullyLoaded();
            }
        }
    }
}
