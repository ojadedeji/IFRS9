using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ILcBgEclComputationResultRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LcBgEclComputationResultRepository : DataRepositoryBase<LcBgEclComputationResult>, ILcBgEclComputationResultRepository
    {
        protected override LcBgEclComputationResult AddEntity(IFRSContext entityContext, LcBgEclComputationResult entity)
        {
            return entityContext.Set<LcBgEclComputationResult>().Add(entity);
        }

        protected override LcBgEclComputationResult UpdateEntity(IFRSContext entityContext, LcBgEclComputationResult entity)
        {
            return (from e in entityContext.Set<LcBgEclComputationResult>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<LcBgEclComputationResult> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<LcBgEclComputationResult>()
                   select e;
        }

        protected override LcBgEclComputationResult GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<LcBgEclComputationResult>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}