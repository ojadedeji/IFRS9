using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IIndividualScheduleRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IndividualScheduleRepository : DataRepositoryBase<IndividualSchedule>, IIndividualScheduleRepository
    {

        protected override IndividualSchedule AddEntity(BasicContext entityContext, IndividualSchedule entity)
        {
            return entityContext.Set<IndividualSchedule>().Add(entity);
        }

        protected override IndividualSchedule UpdateEntity(BasicContext entityContext, IndividualSchedule entity)
        {
            return (from e in entityContext.Set<IndividualSchedule>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IndividualSchedule> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<IndividualSchedule>()
                   select e;
        }

        protected override IndividualSchedule GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IndividualSchedule>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IndividualScheduleInfo> GetIndividualSchedules(string refNo)
        {
            using (BasicContext entityContext = new BasicContext())
            {
                var query = from a in entityContext.LoanPrimaryDataSet
                            join b in entityContext.IntegralFeeSet on a.RefNo equals b.RefNo into bparents
                            from bp in bparents.DefaultIfEmpty()
                            join c in entityContext.LoanIRRDataSet on a.RefNo equals c.RefNo into cparents
                            from cp in cparents.DefaultIfEmpty()
                            where a.RefNo == refNo
                            select new IndividualScheduleInfo()
                              {
                                  LoanPrimaryData = a,
                                  IntegralFee = bp,
                                  LoanIRRData = cp
                              };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<IndividualSchedule> GetIndividualSchedules()
        {
            using (BasicContext entityContext = new BasicContext())
            {
                var query = entityContext.IndividualScheduleSet.AsQueryable().Where(r => r.Processed == false);

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<string> GetDistinctRefNos()
        {
            using (BasicContext entityContext = new BasicContext())
            {
                var query = (entityContext.LoanPrimaryDataSet.Select<LoanPrimaryData, string>(r => r.RefNo)).Distinct();

                return query.ToFullyLoaded();
            }
        }

    }
}






