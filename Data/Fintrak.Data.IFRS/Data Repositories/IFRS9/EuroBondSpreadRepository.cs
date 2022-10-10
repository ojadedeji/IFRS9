using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IEuroBondSpreadRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EuroBondSpreadRepository : DataRepositoryBase<EuroBondSpread>, IEuroBondSpreadRepository
    {
        protected override EuroBondSpread AddEntity(IFRSContext entityContext, EuroBondSpread entity)
        {
            return entityContext.Set<EuroBondSpread>().Add(entity);
        }

        protected override EuroBondSpread UpdateEntity(IFRSContext entityContext, EuroBondSpread entity)
        {
            return (from e in entityContext.Set<EuroBondSpread>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<EuroBondSpread> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<EuroBondSpread>()
                   select e;
        }

        protected override EuroBondSpread GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<EuroBondSpread>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}