using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IBondComputationResultZeroRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BondComputationResultZeroRepository : DataRepositoryBase<BondComputationResultZero>, IBondComputationResultZeroRepository
    {

        protected override BondComputationResultZero AddEntity(BasicContext entityContext, BondComputationResultZero entity)
        {
            return entityContext.Set<BondComputationResultZero>().Add(entity);
        }

        protected override BondComputationResultZero UpdateEntity(BasicContext entityContext, BondComputationResultZero entity)
        {
            return (from e in entityContext.Set<BondComputationResultZero>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<BondComputationResultZero> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<BondComputationResultZero>()
                   select e;
        }

        protected override BondComputationResultZero GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<BondComputationResultZero>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public  IEnumerable<string> GetDistinctBondComputationResultZeroRefNos()
        {
            BasicContext entityContext = new BasicContext();

            var query = (entityContext.BondComputationResultZeroSet.Select<BondComputationResultZero, string>(r => r.RefNo)).Distinct();

            return query.ToFullyLoaded();
        }

        public IEnumerable<BondComputationResultZero> GetBondComputationResultZeroRefNos(string bondComputationResultZeroRefNo)
        {
            BasicContext entityContext = new BasicContext();

            var query = entityContext.BondComputationResultZeroSet.AsQueryable().Where(r => r.RefNo == bondComputationResultZeroRefNo);
                
            return query.ToFullyLoaded();
        }

    }
}

