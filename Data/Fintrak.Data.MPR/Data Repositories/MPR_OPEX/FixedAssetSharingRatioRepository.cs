using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IFixedAssetSharingRatioRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FixedAssetSharingRatioRepository : DataRepositoryBase<FixedAssetSharingRatio>, IFixedAssetSharingRatioRepository
    {

        protected override FixedAssetSharingRatio AddEntity(MPRContext entityContext, FixedAssetSharingRatio entity)
        {
            return entityContext.Set<FixedAssetSharingRatio>().Add(entity);
        }

        protected override FixedAssetSharingRatio UpdateEntity(MPRContext entityContext, FixedAssetSharingRatio entity)
        {
            return (from e in entityContext.Set<FixedAssetSharingRatio>()
                    where e.FixedAssetSharingRatioId == entity.FixedAssetSharingRatioId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<FixedAssetSharingRatio> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<FixedAssetSharingRatio>()
                   select e;
        }

        protected override FixedAssetSharingRatio GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<FixedAssetSharingRatio>()
                         where e.FixedAssetSharingRatioId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
