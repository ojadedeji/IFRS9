using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IIndividualScheduleRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IndividualScheduleRepository : DataRepositoryBase<IndividualSchedule>, IIndividualScheduleRepository
    {

        protected override IndividualSchedule AddEntity(IFRSContext entityContext, IndividualSchedule entity)
        {
            return entityContext.Set<IndividualSchedule>().Add(entity);
        }

        protected override IndividualSchedule UpdateEntity(IFRSContext entityContext, IndividualSchedule entity)
        {
            return (from e in entityContext.Set<IndividualSchedule>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IndividualSchedule> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<IndividualSchedule>()
                   select e;
        }

        protected override IndividualSchedule GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IndividualSchedule>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IndividualScheduleInfo> GetIndividualSchedules(string refNo)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.LoanPryDataSet                           
                            where a.RefNo == refNo
                            select new IndividualScheduleInfo()
                              {
                                  LoanPryData = a,
                                 
                              };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<IndividualSchedule> GetIndividualSchedules()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = entityContext.IndividualScheduleSet.AsQueryable().Where(r => r.Processed == false);

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<string> GetDistinctRefNos()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (entityContext.LoanPryDataSet.Select<LoanPry, string>(r => r.RefNo)).Distinct();

                return query.ToFullyLoaded();
            }
        }

    }
}






