using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IEclCalculationModelRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EclCalculationModelRepository : DataRepositoryBase<EclCalculationModel>, IEclCalculationModelRepository
    {
        protected override EclCalculationModel AddEntity(IFRSContext entityContext, EclCalculationModel entity)
        {
            return entityContext.Set<EclCalculationModel>().Add(entity);
        }

        protected override EclCalculationModel UpdateEntity(IFRSContext entityContext, EclCalculationModel entity)
        {
            return (from e in entityContext.Set<EclCalculationModel>()
                    where e.EclModelId == entity.EclModelId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<EclCalculationModel> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<EclCalculationModel>()
                   select e;
        }

        protected override EclCalculationModel GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<EclCalculationModel>()
                         where e.EclModelId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}