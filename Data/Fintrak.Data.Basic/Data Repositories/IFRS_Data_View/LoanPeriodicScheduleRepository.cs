using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(ILoanPeriodicScheduleRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LoanPeriodicScheduleRepository : DataRepositoryBase<LoanPeriodicSchedule>, ILoanPeriodicScheduleRepository
    {

        protected override LoanPeriodicSchedule AddEntity(BasicContext entityContext, LoanPeriodicSchedule entity)
        {
            return entityContext.Set<LoanPeriodicSchedule>().Add(entity);
        }

        protected override LoanPeriodicSchedule UpdateEntity(BasicContext entityContext, LoanPeriodicSchedule entity)
        {
            return (from e in entityContext.Set<LoanPeriodicSchedule>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<LoanPeriodicSchedule> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<LoanPeriodicSchedule>()
                   select e;
        }

        protected override LoanPeriodicSchedule GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<LoanPeriodicSchedule>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<string> GetDistinctLoanPeriodicScheduleRefNos()
        {
            BasicContext entityContext = new BasicContext();

            var query = (entityContext.LoanPeriodicScheduleSet.Select<LoanPeriodicSchedule, string>(r => r.RefNo)).Distinct();

            return query.ToFullyLoaded();
        }

        public IEnumerable<LoanPeriodicSchedule> GetLoanPeriodicScheduleRefNos(string loanPeriodicScheduleRefNo)
        {
            BasicContext entityContext = new BasicContext();

            var query = entityContext.LoanPeriodicScheduleSet.AsQueryable().Where(r => r.RefNo == loanPeriodicScheduleRefNo);

            return query.ToFullyLoaded();
        }

    }
}
