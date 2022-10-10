using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IEquityStockComputationResultRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EquityStockComputationResultRepository : DataRepositoryBase<EquityStockComputationResult>, IEquityStockComputationResultRepository
    {

        protected override EquityStockComputationResult AddEntity(IFRSContext entityContext, EquityStockComputationResult entity)
        {
            return entityContext.Set<EquityStockComputationResult>().Add(entity);
        }

        protected override EquityStockComputationResult UpdateEntity(IFRSContext entityContext, EquityStockComputationResult entity)
        {
            return (from e in entityContext.Set<EquityStockComputationResult>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<EquityStockComputationResult> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<EquityStockComputationResult>()
                   select e;
        }

        protected override EquityStockComputationResult GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<EquityStockComputationResult>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();


            return results;
        }

        public IEnumerable<EquityStockComputationResult> GetEquityStockComputationResultRefNos(string equityStockComputationResultClassification)
        {
            IFRSContext entityContext = new IFRSContext();

            var query = entityContext.EquityStockComputationResultSet.AsQueryable().Where(r => r.Classification == equityStockComputationResultClassification);

            return query.ToFullyLoaded();
        }

    }
}
