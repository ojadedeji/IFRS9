using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IAbcRatioRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AbcRatioRepository : DataRepositoryBase<AbcRatio>, IAbcRatioRepository
    {

        protected override AbcRatio AddEntity(MPRContext entityContext, AbcRatio entity)
        {
            return entityContext.Set<AbcRatio>().Add(entity);
        }

        protected override AbcRatio UpdateEntity(MPRContext entityContext, AbcRatio entity)
        {
            return (from e in entityContext.Set<AbcRatio>()
                    where e.AbcRatioId == entity.AbcRatioId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<AbcRatio> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<AbcRatio>()
                   select e;
        }

        protected override AbcRatio GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<AbcRatio>()
                         where e.AbcRatioId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}
