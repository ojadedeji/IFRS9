using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ILgdComputationResultRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LgdComputationResultRepository : DataRepositoryBase<LgdComputationResult>, ILgdComputationResultRepository
    {
        protected override LgdComputationResult AddEntity(IFRSContext entityContext, LgdComputationResult entity)
        {
            return entityContext.Set<LgdComputationResult>().Add(entity);
        }

        protected override LgdComputationResult UpdateEntity(IFRSContext entityContext, LgdComputationResult entity)
        {
            return (from e in entityContext.Set<LgdComputationResult>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<LgdComputationResult> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<LgdComputationResult>()
                   select e;
        }

        protected override LgdComputationResult GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<LgdComputationResult>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}