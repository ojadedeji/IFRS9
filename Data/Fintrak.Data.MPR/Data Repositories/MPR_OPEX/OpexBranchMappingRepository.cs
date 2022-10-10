using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IOpexBranchMappingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OpexBranchMappingRepository : DataRepositoryBase<OpexBranchMapping>, IOpexBranchMappingRepository
    {

        protected override OpexBranchMapping AddEntity(MPRContext entityContext, OpexBranchMapping entity)
        {
            return entityContext.Set<OpexBranchMapping>().Add(entity);
        }

        protected override OpexBranchMapping UpdateEntity(MPRContext entityContext, OpexBranchMapping entity)
        {
            return (from e in entityContext.Set<OpexBranchMapping>()
                    where e.OpexBranchMappingId == entity.OpexBranchMappingId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<OpexBranchMapping> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<OpexBranchMapping>()
                   select e;
        }

        protected override OpexBranchMapping GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<OpexBranchMapping>()
                         where e.OpexBranchMappingId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}
