using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IEclComputationResultRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EclComputationResultRepository : DataRepositoryBase<EclComputationResult>, IEclComputationResultRepository
    {
        protected override EclComputationResult AddEntity(IFRSContext entityContext, EclComputationResult entity)
        {
            return entityContext.Set<EclComputationResult>().Add(entity);
        }

        protected override EclComputationResult UpdateEntity(IFRSContext entityContext, EclComputationResult entity)
        {
            return (from e in entityContext.Set<EclComputationResult>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<EclComputationResult> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<EclComputationResult>()
                   select e;
        }

        protected override EclComputationResult GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<EclComputationResult>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<EclComputationResult> GetEntityByStage(int stage)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.FinalEclOutputSet
                            where a.Stage == stage
                            select a;

                return query.ToFullyLoaded();
            }
        }
       
    }
}