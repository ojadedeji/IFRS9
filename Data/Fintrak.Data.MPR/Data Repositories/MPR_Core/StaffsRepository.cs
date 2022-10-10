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
    [Export(typeof(IStaffsRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class StaffsRepository : DataRepositoryBase<Staffs>, IStaffsRepository
    {
        protected override Staffs AddEntity(MPRContext entityContext, Staffs entity)
        {
            return entityContext.Set<Staffs>().Add(entity);
        }

        protected override Staffs UpdateEntity(MPRContext entityContext, Staffs entity)
        {
            return (from e in entityContext.Set<Staffs>()
                    where e.StaffId == entity.StaffId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Staffs> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<Staffs>()
                   select e;
        }

        protected override Staffs GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<Staffs>()
                         where e.StaffId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}
