using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IHoExemptionMISCodeRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class HoExemptionMISCodeRepository : DataRepositoryBase<HoExemptionMISCode>, IHoExemptionMISCodeRepository
    {

        protected override HoExemptionMISCode AddEntity(MPRContext entityContext, HoExemptionMISCode entity)
        {
            return entityContext.Set<HoExemptionMISCode>().Add(entity);
        }

        protected override HoExemptionMISCode UpdateEntity(MPRContext entityContext, HoExemptionMISCode entity)
        {
            return (from e in entityContext.Set<HoExemptionMISCode>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<HoExemptionMISCode> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<HoExemptionMISCode>()
                   select e;
        }

        protected override HoExemptionMISCode GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<HoExemptionMISCode>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
