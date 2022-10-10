using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IDepositRepaymentScheduleRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DepositRepaymentScheduleRepository : DataRepositoryBase<DepositRepaymentSchedule>, IDepositRepaymentScheduleRepository
    {
        protected override DepositRepaymentSchedule AddEntity(IFRSContext entityContext, DepositRepaymentSchedule entity)
        {
            return entityContext.Set<DepositRepaymentSchedule>().Add(entity);
        }

        protected override DepositRepaymentSchedule UpdateEntity(IFRSContext entityContext, DepositRepaymentSchedule entity)
        {
            return (from e in entityContext.Set<DepositRepaymentSchedule>()
                    where e.DepositRepayId == entity.DepositRepayId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<DepositRepaymentSchedule> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<DepositRepaymentSchedule>()
                   select e;
        }

        protected override DepositRepaymentSchedule GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<DepositRepaymentSchedule>()
                         where e.DepositRepayId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}