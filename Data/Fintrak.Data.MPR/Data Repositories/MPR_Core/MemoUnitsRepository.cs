using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IMemoUnitsRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MemoUnitsRepository : DataRepositoryBase<MemoUnits>, IMemoUnitsRepository
    {

        protected override MemoUnits AddEntity(MPRContext entityContext, MemoUnits entity)
        {
            return entityContext.Set<MemoUnits>().Add(entity);
        }

        protected override MemoUnits UpdateEntity(MPRContext entityContext, MemoUnits entity)
        {
            return (from e in entityContext.Set<MemoUnits>()
                    where e.MemoUnitsId == entity.MemoUnitsId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<MemoUnits> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<MemoUnits>()
                   select e;
        }

        protected override MemoUnits GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<MemoUnits>()
                         where e.MemoUnitsId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}
