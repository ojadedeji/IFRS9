using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(INEABranchSBUSharesRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class NEABranchSBUSharesRepository : DataRepositoryBase<NEABranchSBUShares>, INEABranchSBUSharesRepository
    {

        protected override NEABranchSBUShares AddEntity(MPRContext entityContext, NEABranchSBUShares entity)
        {
            return entityContext.Set<NEABranchSBUShares>().Add(entity);
        }

        protected override NEABranchSBUShares UpdateEntity(MPRContext entityContext, NEABranchSBUShares entity)
        {
            return (from e in entityContext.Set<NEABranchSBUShares>()
                    where e.NEABranchSBUSharesId == entity.NEABranchSBUSharesId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<NEABranchSBUShares> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<NEABranchSBUShares>()
                   select e;
        }

        protected override NEABranchSBUShares GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<NEABranchSBUShares>()
                         where e.NEABranchSBUSharesId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
