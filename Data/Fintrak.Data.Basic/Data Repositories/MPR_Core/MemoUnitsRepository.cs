using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IMemoUnitsRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MemoUnitsRepository : DataRepositoryBase<MemoUnits>, IMemoUnitsRepository
    {

        protected override MemoUnits AddEntity(BasicContext entityContext, MemoUnits entity)
        {
            return entityContext.Set<MemoUnits>().Add(entity);
        }

        protected override MemoUnits UpdateEntity(BasicContext entityContext, MemoUnits entity)
        {
            return (from e in entityContext.Set<MemoUnits>()
                    where e.MemoUnitsId == entity.MemoUnitsId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<MemoUnits> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<MemoUnits>()
                   select e;
        }

        protected override MemoUnits GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<MemoUnits>()
                         where e.MemoUnitsId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}
