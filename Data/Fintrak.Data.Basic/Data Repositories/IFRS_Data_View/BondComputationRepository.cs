using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IBondComputationRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BondComputationRepository : DataRepositoryBase<BondComputation>, IBondComputationRepository
    {

        protected override BondComputation AddEntity(BasicContext entityContext, BondComputation entity)
        {
            return entityContext.Set<BondComputation>().Add(entity);
        }

        protected override BondComputation UpdateEntity(BasicContext entityContext, BondComputation entity)
        {
            return (from e in entityContext.Set<BondComputation>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<BondComputation> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<BondComputation>()
                   select e;
        }

        protected override BondComputation GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<BondComputation>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }


        public IEnumerable<string> GetDistinctBondComputationRefNos()
        {
            BasicContext entityContext = new BasicContext();

            var query = (entityContext.BondComputationSet.Select<BondComputation, string>(r => r.RefNo)).Distinct();

            return query.ToFullyLoaded();
        }

        public IEnumerable<BondComputation> GetBondPeriodicScheduleRefNos(string bondComputationRefNo)
        {
            BasicContext entityContext = new BasicContext();

            var query = entityContext.BondComputationSet.AsQueryable().Where(r => r.RefNo == bondComputationRefNo);

            return query.ToFullyLoaded();
        }

    }
}
