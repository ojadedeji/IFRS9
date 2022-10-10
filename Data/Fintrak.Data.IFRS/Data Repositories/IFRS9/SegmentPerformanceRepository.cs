using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ISegmentPerformanceRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SegmentPerformanceRepository : DataRepositoryBase<SegmentPerformance>, ISegmentPerformanceRepository
    {
        protected override SegmentPerformance AddEntity(IFRSContext entityContext, SegmentPerformance entity)
        {
            return entityContext.Set<SegmentPerformance>().Add(entity);
        }

        protected override SegmentPerformance UpdateEntity(IFRSContext entityContext, SegmentPerformance entity)
        {
            return (from e in entityContext.Set<SegmentPerformance>()
                    where e.SegmentId == entity.SegmentId
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<SegmentPerformance> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<SegmentPerformance>()
                   select e;
        }

        protected override SegmentPerformance GetEntity(IFRSContext entityContext, int segmentId)
        {
            var query = (from e in entityContext.Set<SegmentPerformance>()
                         where e.SegmentId == segmentId
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}