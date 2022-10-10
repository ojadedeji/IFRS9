using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(ILoanScheduleRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LoanScheduleRepository : DataRepositoryBase<LoanSchedule>, ILoanScheduleRepository
    {

        protected override LoanSchedule AddEntity(BasicContext entityContext, LoanSchedule entity)
        {
            return entityContext.Set<LoanSchedule>().Add(entity);
        }

        protected override LoanSchedule UpdateEntity(BasicContext entityContext, LoanSchedule entity)
        {
            return (from e in entityContext.Set<LoanSchedule>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<LoanSchedule> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<LoanSchedule>()
                   select e;
        }

        protected override LoanSchedule GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<LoanSchedule>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }


        public IEnumerable<string> GetDistinctLoanScheduleRefNos()
        {
            BasicContext entityContext = new BasicContext();

            var query = (entityContext.LoanScheduleSet.Select<LoanSchedule, string>(r => r.RefNo)).Distinct();

            return query.ToFullyLoaded();
        }

        public IEnumerable<LoanSchedule> GetLoanScheduleRefNos(string loanScheduleRefNo)
        {
            BasicContext entityContext = new BasicContext();

            var query = entityContext.LoanScheduleSet.AsQueryable().Where(r => r.RefNo == loanScheduleRefNo);

            return query.ToFullyLoaded();
        }

    }
}
