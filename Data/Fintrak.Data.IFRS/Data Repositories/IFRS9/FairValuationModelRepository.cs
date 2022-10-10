using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IFairValuationModelRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FairValuationModelRepository : DataRepositoryBase<FairValuationModel>, IFairValuationModelRepository
    {
        protected override FairValuationModel AddEntity(IFRSContext entityContext, FairValuationModel entity)
        {
            return entityContext.Set<FairValuationModel>().Add(entity);
        }

        protected override FairValuationModel UpdateEntity(IFRSContext entityContext, FairValuationModel entity)
        {
            return (from e in entityContext.Set<FairValuationModel>()
                    where e.FairValueModelId == entity.FairValueModelId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<FairValuationModel> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<FairValuationModel>()
                   select e;
        }

        protected override FairValuationModel GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<FairValuationModel>()
                         where e.FairValueModelId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}