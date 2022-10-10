using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(INEABranchSharingRatioRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class NEABranchSharingRatioRepository : DataRepositoryBase<NEABranchSharingRatio>, INEABranchSharingRatioRepository
    {

        protected override NEABranchSharingRatio AddEntity(MPRContext entityContext, NEABranchSharingRatio entity)
        {
            return entityContext.Set<NEABranchSharingRatio>().Add(entity);
        }

        protected override NEABranchSharingRatio UpdateEntity(MPRContext entityContext, NEABranchSharingRatio entity)
        {
            return (from e in entityContext.Set<NEABranchSharingRatio>()
                    where e.NEABranchSharingRatioId == entity.NEABranchSharingRatioId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<NEABranchSharingRatio> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<NEABranchSharingRatio>()
                   select e;
        }

        protected override NEABranchSharingRatio GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<NEABranchSharingRatio>()
                         where e.NEABranchSharingRatioId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
