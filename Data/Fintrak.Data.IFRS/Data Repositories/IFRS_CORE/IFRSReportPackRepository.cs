using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IIFRSReportPackRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IFRSReportPackRepository : DataRepositoryBase<IFRSReportPack>, IIFRSReportPackRepository
    {

        protected override IFRSReportPack AddEntity(IFRSContext entityContext, IFRSReportPack entity)
        {
            return entityContext.Set<IFRSReportPack>().Add(entity);
        }
        protected override IFRSReportPack UpdateEntity(IFRSContext entityContext, IFRSReportPack entity)
        {
            return (from e in entityContext.Set<IFRSReportPack>()
                    where e.ReportPackId == entity.ReportPackId
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<IFRSReportPack> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<IFRSReportPack>()
                   select e;
        }

      
        protected override IFRSReportPack GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IFRSReportPack>()
                         where e.ReportPackId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}
