using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ITBillsComputationResultRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TBillsComputationResultRepository : DataRepositoryBase<TBillsComputationResult>, ITBillsComputationResultRepository
    {

        protected override TBillsComputationResult AddEntity(IFRSContext entityContext, TBillsComputationResult entity)
        {
            return entityContext.Set<TBillsComputationResult>().Add(entity);
        }

        protected override TBillsComputationResult UpdateEntity(IFRSContext entityContext, TBillsComputationResult entity)
        {
            return (from e in entityContext.Set<TBillsComputationResult>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<TBillsComputationResult> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<TBillsComputationResult>()
                   select e;
        }

        protected override TBillsComputationResult GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<TBillsComputationResult>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<TBillsComputationResult> GetTBillsComputationResultRefNos(string tBillsComputationResultClassification)
        {
            IFRSContext entityContext = new IFRSContext();

            var query = entityContext.TBillsComputationResultSet.AsQueryable().Where(r => r.Classification == tBillsComputationResultClassification);

            return query.ToFullyLoaded();
        }

    }
}
