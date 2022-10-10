using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(INEASharingRatioRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class NEASharingRatioRepository : DataRepositoryBase<NEASharingRatio>, INEASharingRatioRepository
    {

        protected override NEASharingRatio AddEntity(MPRContext entityContext, NEASharingRatio entity)
        {
            return entityContext.Set<NEASharingRatio>().Add(entity);
        }

        protected override NEASharingRatio UpdateEntity(MPRContext entityContext, NEASharingRatio entity)
        {
            return (from e in entityContext.Set<NEASharingRatio>()
                    where e.NEASharingRatioId == entity.NEASharingRatioId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<NEASharingRatio> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<NEASharingRatio>()
                   select e;
        }

        protected override NEASharingRatio GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<NEASharingRatio>()
                         where e.NEASharingRatioId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
