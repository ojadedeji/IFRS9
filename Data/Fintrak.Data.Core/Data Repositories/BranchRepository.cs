using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Data.Core.Contracts;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Core.Entities;

namespace Fintrak.Data.Core
{
    [Export(typeof(IBranchRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BranchRepository : DataRepositoryBase<Branch>, IBranchRepository
    {
        protected override Branch AddEntity(CoreContext entityContext, Branch entity)
        {
            return entityContext.Set<Branch>().Add(entity);
        }

        protected override Branch UpdateEntity(CoreContext entityContext, Branch entity)
        {
            return (from e in entityContext.Set<Branch>()
                    where e.BranchId == entity.BranchId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Branch> GetEntities(CoreContext entityContext)
        {
            return from e in entityContext.Set<Branch>()
                   select e;
        }

        protected override Branch GetEntity(CoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<Branch>()
                         where e.BranchId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<BranchInfo> GetBranches()
        {
            using (CoreContext entityContext = new CoreContext())
            {
                var query = from a in entityContext.BranchSet
                            
                            select new BranchInfo()
                            {
                                Branch = a
                            };

                return query.ToFullyLoaded();
            }
        }
    }
}
