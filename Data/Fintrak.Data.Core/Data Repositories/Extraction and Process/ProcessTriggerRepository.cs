using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Core.Framework;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Core.Entities;
using Fintrak.Data.Core.Contracts;
using Fintrak.Shared.Common.Utils;

namespace Fintrak.Data.Core
{
    [Export(typeof(IProcessTriggerRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProcessTriggerRepository : DataRepositoryBase<ProcessTrigger>, IProcessTriggerRepository
    {
        protected override ProcessTrigger AddEntity(CoreContext entityContext, ProcessTrigger entity)
        {
            return entityContext.Set<ProcessTrigger>().Add(entity);
        }

        protected override ProcessTrigger UpdateEntity(CoreContext entityContext, ProcessTrigger entity)
        {
            return (from e in entityContext.Set<ProcessTrigger>()
                    where e.ProcessTriggerId == entity.ProcessTriggerId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ProcessTrigger> GetEntities(CoreContext entityContext)
        {
            return from e in entityContext.Set<ProcessTrigger>()
                   select e;
        }

        protected override ProcessTrigger GetEntity(CoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ProcessTrigger>()
                         where e.ProcessTriggerId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ProcessTriggerInfo> GetProcessTriggers()
        {
            using (CoreContext entityContext = new CoreContext())
            {
                var query = from a in entityContext.ProcessTriggerSet
                            join b in entityContext.ProcessSet on a.ProcessId equals b.ProcessId
                            
                            select new ProcessTriggerInfo()
                            {
                                ProcessTrigger = a,
                                Processes = b
                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<ProcessTriggerInfo> GetProcessTriggerByProcess(int processId)
        {
            using (CoreContext entityContext = new CoreContext())
            {
                var query = from a in entityContext.ProcessTriggerSet
                            join b in entityContext.ProcessSet on a.ProcessId equals b.ProcessId
                            join c in entityContext.ProcessJobSet on a.ProcessJobId equals c.ProcessJobId
                            where a.ProcessId == processId 
                            select new ProcessTriggerInfo()
                            {
                                ProcessTrigger = a,
                                Processes = b,
                                ProcessJob = c
                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<ProcessTriggerInfo> GetProcessTriggerByJob(string jobCode)
        {
            using (CoreContext entityContext = new CoreContext())
            {
                var query = from a in entityContext.ProcessTriggerSet
                            join b in entityContext.ProcessSet on a.ProcessId equals b.ProcessId
                            join c in entityContext.ProcessJobSet on a.ProcessJobId equals c.ProcessJobId
                            where c.Code == jobCode
                            select new ProcessTriggerInfo()
                            {
                                ProcessTrigger = a,
                                Processes = b,
                                ProcessJob = c
                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<ProcessTriggerInfo> GetProcessTriggerByRunDate()
        {
            using (CoreContext entityContext = new CoreContext())
            {
                var query = from a in entityContext.ProcessTriggerSet
                            join b in entityContext.ProcessSet on a.ProcessId equals b.ProcessId
                          
                            select new ProcessTriggerInfo()
                            {
                                ProcessTrigger = a,
                                Processes = b
                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<ProcessTriggerInfo> GetProcessTriggerByRunTime(DateTime runTime)
        {
            using (CoreContext entityContext = new CoreContext())
            {
                var query = from a in entityContext.ProcessTriggerSet
                            join b in entityContext.ProcessSet on a.ProcessId equals b.ProcessId
                           
                            select new ProcessTriggerInfo()
                            {
                                ProcessTrigger = a,
                                Processes = b
                            };

                return query.ToFullyLoaded();
            }
        }
    }
}
