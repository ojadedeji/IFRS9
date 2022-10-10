using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IEquityStockComputationResultRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EquityStockComputationResultRepository : DataRepositoryBase<EquityStockComputationResult>, IEquityStockComputationResultRepository
    {

        protected override EquityStockComputationResult AddEntity(BasicContext entityContext, EquityStockComputationResult entity)
        {
            return entityContext.Set<EquityStockComputationResult>().Add(entity);
        }

        protected override EquityStockComputationResult UpdateEntity(BasicContext entityContext, EquityStockComputationResult entity)
        {
            return (from e in entityContext.Set<EquityStockComputationResult>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<EquityStockComputationResult> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<EquityStockComputationResult>()
                   select e;
        }

        protected override EquityStockComputationResult GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<EquityStockComputationResult>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();


            return results;
        }

        public IEnumerable<EquityStockComputationResult> GetEquityStockComputationResultRefNos(string equityStockComputationResultClassification)
        {
            BasicContext entityContext = new BasicContext();

            var query = entityContext.EquityStockComputationResultSet.AsQueryable().Where(r => r.Classification == equityStockComputationResultClassification);

            return query.ToFullyLoaded();
        }

    }
}
