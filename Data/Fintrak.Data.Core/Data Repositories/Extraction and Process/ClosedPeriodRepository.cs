using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Data.Core.Contracts;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Core.Entities;

namespace Fintrak.Data.Core
{
    [Export(typeof(IClosedPeriodRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ClosedPeriodRepository : DataRepositoryBase<ClosedPeriod>, IClosedPeriodRepository
    {
        protected override ClosedPeriod AddEntity(CoreContext entityContext, ClosedPeriod entity)
        {
            return entityContext.Set<ClosedPeriod>().Add(entity);
        }

        protected override ClosedPeriod UpdateEntity(CoreContext entityContext, ClosedPeriod entity)
        {
            return (from e in entityContext.Set<ClosedPeriod>()
                    where e.ClosedPeriodId == entity.ClosedPeriodId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ClosedPeriod> GetEntities(CoreContext entityContext)
        {
            return from e in entityContext.Set<ClosedPeriod>()
                   select e;
        }

        protected override ClosedPeriod GetEntity(CoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ClosedPeriod>()
                         where e.ClosedPeriodId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ClosedPeriodInfo> GetClosedPeriods()
        {
            using (CoreContext entityContext = new CoreContext())
            {
                var query = from a in entityContext.ClosedPeriodSet
 
                            select new ClosedPeriodInfo()
                            {
                                ClosedPeriod = a
                            };

                return query.ToFullyLoaded();
            }
        }
    }
}
