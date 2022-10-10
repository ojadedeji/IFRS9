using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Data.Core.Contracts;
using Fintrak.Shared.Core.Entities;

namespace Fintrak.Data.Core
{
    [Export(typeof(IReportStatusRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ReportStatusRepository : DataRepositoryBase<ReportStatus>, IReportStatusRepository
    {
        protected override ReportStatus AddEntity(CoreContext entityContext, ReportStatus entity)
        {
            return entityContext.Set<ReportStatus>().Add(entity);
        }

        protected override ReportStatus UpdateEntity(CoreContext entityContext, ReportStatus entity)
        {
            return (from e in entityContext.Set<ReportStatus>()
                    where e.StatusId == entity.StatusId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ReportStatus> GetEntities(CoreContext entityContext)
        {
            return from e in entityContext.Set<ReportStatus>()
                   select e;
        }

        
        protected override ReportStatus GetEntity(CoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ReportStatus>()
                         where e.StatusId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ReportStatusInfo> GetAllReportStatus()
        {
            using (CoreContext entityContext = new CoreContext())
            {
                var query = from a in entityContext.ReportStatusSet

                            select new ReportStatusInfo()
                            {
                                ReportStatus = a
                            };
                var results = query.ToArray();
                return results;
            }
        }

    }
}
