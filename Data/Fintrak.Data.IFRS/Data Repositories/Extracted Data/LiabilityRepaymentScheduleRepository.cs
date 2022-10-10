using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ILiabilityRepaymentScheduleRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LiabilityRepaymentScheduleRepository : DataRepositoryBase<LiabilityRepaymentSchedule>, ILiabilityRepaymentScheduleRepository
    {
        protected override LiabilityRepaymentSchedule AddEntity(IFRSContext entityContext, LiabilityRepaymentSchedule entity)
        {
            return entityContext.Set<LiabilityRepaymentSchedule>().Add(entity);
        }

        protected override LiabilityRepaymentSchedule UpdateEntity(IFRSContext entityContext, LiabilityRepaymentSchedule entity)
        {
            return (from e in entityContext.Set<LiabilityRepaymentSchedule>()
                    where e.LiabilityRepayId == entity.LiabilityRepayId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<LiabilityRepaymentSchedule> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<LiabilityRepaymentSchedule>()
                   select e;
        }

        protected override LiabilityRepaymentSchedule GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<LiabilityRepaymentSchedule>()
                         where e.LiabilityRepayId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}