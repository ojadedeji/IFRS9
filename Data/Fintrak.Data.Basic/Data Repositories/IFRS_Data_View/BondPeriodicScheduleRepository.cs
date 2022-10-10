using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IBondPeriodicScheduleRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BondPeriodicScheduleRepository : DataRepositoryBase<BondPeriodicSchedule>, IBondPeriodicScheduleRepository
    {

        protected override BondPeriodicSchedule AddEntity(BasicContext entityContext, BondPeriodicSchedule entity)
        {
            return entityContext.Set<BondPeriodicSchedule>().Add(entity);
        }

        protected override BondPeriodicSchedule UpdateEntity(BasicContext entityContext, BondPeriodicSchedule entity)
        {
            return (from e in entityContext.Set<BondPeriodicSchedule>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<BondPeriodicSchedule> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<BondPeriodicSchedule>()
                   select e;
        }

        protected override BondPeriodicSchedule GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<BondPeriodicSchedule>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<string> GetDistinctBondPeriodicScheduleRefNos()
        {
            BasicContext entityContext = new BasicContext();

            var query = (entityContext.BondPeriodicScheduleSet.Select<BondPeriodicSchedule, string>(r => r.RefNo)).Distinct();

            return query.ToFullyLoaded(); 
        }

        public IEnumerable<BondPeriodicSchedule> GetBondPeriodicScheduleRefNos(string bondPeriodicScheduleRefNo)
        {
            BasicContext entityContext = new BasicContext();

            var query = entityContext.BondPeriodicScheduleSet.AsQueryable().Where(r => r.RefNo == bondPeriodicScheduleRefNo);

            return query.ToFullyLoaded();
        }

    }
}
