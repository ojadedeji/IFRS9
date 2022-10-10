using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;
using Fintrak.Shared.MPR.Framework;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IBranchDefaultMISRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BranchDefaultMISRepository : DataRepositoryBase<BranchDefaultMIS>, IBranchDefaultMISRepository
    {

        protected override BranchDefaultMIS AddEntity(MPRContext entityContext, BranchDefaultMIS entity)
        {
            return entityContext.Set<BranchDefaultMIS>().Add(entity);
        }

        protected override BranchDefaultMIS UpdateEntity(MPRContext entityContext, BranchDefaultMIS entity)
        {
            return (from e in entityContext.Set<BranchDefaultMIS>() 
                    where e.BranchDefaultMISId == entity.BranchDefaultMISId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<BranchDefaultMIS> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<BranchDefaultMIS>()
                   select e;
        }

        protected override BranchDefaultMIS GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<BranchDefaultMIS>()
                         where e.BranchDefaultMISId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
