using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IIFRSReportRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IFRSReportRepository : DataRepositoryBase<IFRSReport>, IIFRSReportRepository
    {

        protected override IFRSReport AddEntity(BasicContext entityContext, IFRSReport entity)
        {
            return entityContext.Set<IFRSReport>().Add(entity);
        }

        protected override IFRSReport UpdateEntity(BasicContext entityContext, IFRSReport entity)
        {
            return (from e in entityContext.Set<IFRSReport>()
                    where e.IFRSReportId == entity.IFRSReportId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IFRSReport> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<IFRSReport>()
                   select e;
        }

        protected override IFRSReport GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IFRSReport>()
                         where e.IFRSReportId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
