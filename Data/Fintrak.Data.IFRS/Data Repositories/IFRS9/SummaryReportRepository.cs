using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ISummaryReportRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SummaryReportRepository : DataRepositoryBase<SummaryReport>, ISummaryReportRepository
    {
        protected override SummaryReport AddEntity(IFRSContext entityContext, SummaryReport entity)
        {
            return entityContext.Set<SummaryReport>().Add(entity);
        }

        protected override SummaryReport UpdateEntity(IFRSContext entityContext, SummaryReport entity)
        {
            return (from e in entityContext.Set<SummaryReport>()
                    where e.SummaryReportId == entity.SummaryReportId
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<SummaryReport> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<SummaryReport>()
                   select e;
        }

        protected override SummaryReport GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<SummaryReport>()
                         where e.SummaryReportId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}