using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Data.Core;
using Fintrak.Data.Core.Contracts;
using Fintrak.Shared.Core.Entities;

namespace Fintrak.Data.Core
{
    [Export(typeof(IPackageSetupRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PackageSetupRepository : DataRepositoryBase<PackageSetup>, IPackageSetupRepository
    {
        protected override PackageSetup AddEntity(CoreContext entityContext, PackageSetup entity)
        {
            return entityContext.Set<PackageSetup>().Add(entity);
        }

        protected override PackageSetup UpdateEntity(CoreContext entityContext, PackageSetup entity)
        {
            return (from e in entityContext.Set<PackageSetup>()
                    where e.PackageSetupId == entity.PackageSetupId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<PackageSetup> GetEntities(CoreContext entityContext)
        {
            return from e in entityContext.Set<PackageSetup>()
                   select e;
        }

        protected override PackageSetup GetEntity(CoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<PackageSetup>()
                         where e.PackageSetupId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}
