using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(INEASharingRatioFcyRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class NEASharingRatioFcyRepository : DataRepositoryBase<NEASharingRatioFcy>, INEASharingRatioFcyRepository
    {

        protected override NEASharingRatioFcy AddEntity(MPRContext entityContext, NEASharingRatioFcy entity)
        {
            return entityContext.Set<NEASharingRatioFcy>().Add(entity);
        }

        protected override NEASharingRatioFcy UpdateEntity(MPRContext entityContext, NEASharingRatioFcy entity)
        {
            return (from e in entityContext.Set<NEASharingRatioFcy>()
                    where e.NEASharingRatioFcyId == entity.NEASharingRatioFcyId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<NEASharingRatioFcy> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<NEASharingRatioFcy>()
                   select e;
        }

        protected override NEASharingRatioFcy GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<NEASharingRatioFcy>()
                         where e.NEASharingRatioFcyId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}
