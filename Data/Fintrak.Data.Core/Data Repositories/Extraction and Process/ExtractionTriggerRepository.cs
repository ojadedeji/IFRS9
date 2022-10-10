using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Core.Entities;
using Fintrak.Data.Core.Contracts;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Data.Core
{
    [Export(typeof(IExtractionTriggerRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ExtractionTriggerRepository : DataRepositoryBase<ExtractionTrigger>, IExtractionTriggerRepository
    {
        protected override ExtractionTrigger AddEntity(CoreContext entityContext, ExtractionTrigger entity)
        {
            return entityContext.Set<ExtractionTrigger>().Add(entity);
        }

        protected override ExtractionTrigger UpdateEntity(CoreContext entityContext, ExtractionTrigger entity)
        {
            return (from e in entityContext.Set<ExtractionTrigger>()
                    where e.ExtractionTriggerId == entity.ExtractionTriggerId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ExtractionTrigger> GetEntities(CoreContext entityContext)
        {
            return from e in entityContext.Set<ExtractionTrigger>()
                   select e;
        }

        protected override ExtractionTrigger GetEntity(CoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ExtractionTrigger>()
                         where e.ExtractionTriggerId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ExtractionTriggerInfo> GetExtractionTriggers()
        {
            using (CoreContext entityContext = new CoreContext())
            { 
                var query = from a in entityContext.ExtractionTriggerSet
                            join b in entityContext.ExtractionSet on a.ExtractionId equals b.ExtractionId
                            join c in entityContext.SolutionRunDateSet on b.SolutionId equals c.SolutionId
                            where a.StartDate >= c.RunDate && a.EndDate <= c.RunDate 
                            select new ExtractionTriggerInfo()
                            {
                                ExtractionTrigger = a,
                                Extraction = b
                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<ExtractionTriggerInfo> GetExtractionTriggerByJob(string jobCode)
        {
            using (CoreContext entityContext = new CoreContext())
            {
                var query = from a in entityContext.ExtractionTriggerSet
                            join b in entityContext.ExtractionSet on a.ExtractionId equals b.ExtractionId
                            join c in entityContext.ExtractionJobSet on a.ExtractionJobId equals c.ExtractionJobId
                            where c.Code == jobCode
                            select new ExtractionTriggerInfo()
                            {
                                ExtractionTrigger = a,
                                Extraction = b,
                                ExtractionJob = c
                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<ExtractionTriggerInfo> GetExtractionTriggerByExtraction(int extractionId)
        {
            using (CoreContext entityContext = new CoreContext())
            {
                var query = from a in entityContext.ExtractionTriggerSet
                            join b in entityContext.ExtractionSet on a.ExtractionId equals b.ExtractionId
                            join c in entityContext.ExtractionJobSet on a.ExtractionJobId equals c.ExtractionJobId
                            where a.ExtractionId == extractionId 
                            select new ExtractionTriggerInfo()
                            {
                                ExtractionTrigger = a,
                                Extraction = b,
                                ExtractionJob = c
                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<ExtractionTriggerInfo> GetExtractionTriggerByRunDate(DateTime startDate, DateTime endDate)
        {
            using (CoreContext entityContext = new CoreContext())
            {
                var query = from a in entityContext.ExtractionTriggerSet
                            join b in entityContext.ExtractionSet on a.ExtractionId equals b.ExtractionId
                            join c in entityContext.ExtractionJobSet on a.ExtractionJobId equals c.ExtractionJobId
                            where a.StartDate >= startDate.Date &&  a.EndDate <= endDate.Date                            
                            select new ExtractionTriggerInfo()
                            {
                                ExtractionTrigger = a,
                                Extraction = b,
                                ExtractionJob = c
                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<ExtractionTriggerInfo> GetExtractionTriggerByRunTime(DateTime runTime)
        {
            using (CoreContext entityContext = new CoreContext())
            {
                var query = from a in entityContext.ExtractionTriggerSet
                            join b in entityContext.ExtractionSet on a.ExtractionId equals b.ExtractionId
                            join c in entityContext.ExtractionJobSet on a.ExtractionJobId equals c.ExtractionJobId
                            where a.RunTime == runTime  
                            select new ExtractionTriggerInfo()
                            {
                                ExtractionTrigger = a,
                                Extraction = b,
                                ExtractionJob = c
                            };

                return query.ToFullyLoaded();
            }
        }
    }
}
