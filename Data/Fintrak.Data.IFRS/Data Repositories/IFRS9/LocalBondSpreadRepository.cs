using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ILocalBondSpreadRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LocalBondSpreadRepository : DataRepositoryBase<LocalBondSpread>, ILocalBondSpreadRepository
    {
        protected override LocalBondSpread AddEntity(IFRSContext entityContext, LocalBondSpread entity)
        {
            return entityContext.Set<LocalBondSpread>().Add(entity);
        }

        protected override LocalBondSpread UpdateEntity(IFRSContext entityContext, LocalBondSpread entity)
        {
            return (from e in entityContext.Set<LocalBondSpread>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<LocalBondSpread> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<LocalBondSpread>()
                   select e;
        }

        protected override LocalBondSpread GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<LocalBondSpread>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}