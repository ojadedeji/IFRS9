using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;
using Fintrak.Shared.Basic.Framework;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IBranchDefaultMISRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BranchDefaultMISRepository : DataRepositoryBase<BranchDefaultMIS>, IBranchDefaultMISRepository
    {

        protected override BranchDefaultMIS AddEntity(BasicContext entityContext, BranchDefaultMIS entity)
        {
            return entityContext.Set<BranchDefaultMIS>().Add(entity);
        }

        protected override BranchDefaultMIS UpdateEntity(BasicContext entityContext, BranchDefaultMIS entity)
        {
            return (from e in entityContext.Set<BranchDefaultMIS>() 
                    where e.BranchDefaultMISId == entity.BranchDefaultMISId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<BranchDefaultMIS> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<BranchDefaultMIS>()
                   select e;
        }

        protected override BranchDefaultMIS GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<BranchDefaultMIS>()
                         where e.BranchDefaultMISId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
