using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Data.Core.Contracts;
using Fintrak.Shared.Core.Entities;

namespace Fintrak.Data.Core
{
    [Export(typeof(IStaffRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class StaffRepository : DataRepositoryBase<Staff>, IStaffRepository
    {
        protected override Staff AddEntity(CoreContext entityContext, Staff entity)
        {
            return entityContext.Set<Staff>().Add(entity);
        }

        protected override Staff UpdateEntity(CoreContext entityContext, Staff entity)
        {
            return (from e in entityContext.Set<Staff>()
                    where e.StaffId == entity.StaffId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Staff> GetEntities(CoreContext entityContext)
        {
            return from e in entityContext.Set<Staff>()
                   select e;
        }

        protected override Staff GetEntity(CoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<Staff>()
                         where e.StaffId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}
