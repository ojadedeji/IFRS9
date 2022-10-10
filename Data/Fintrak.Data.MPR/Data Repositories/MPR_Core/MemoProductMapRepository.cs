using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IMemoProductMapRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MemoProductMapRepository : DataRepositoryBase<MemoProductMap>, IMemoProductMapRepository
    {

        protected override MemoProductMap AddEntity(MPRContext entityContext, MemoProductMap entity)
        {
            return entityContext.Set<MemoProductMap>().Add(entity);
        }

        protected override MemoProductMap UpdateEntity(MPRContext entityContext, MemoProductMap entity)
        {
            return (from e in entityContext.Set<MemoProductMap>()
                    where e.MemoProductMapId == entity.MemoProductMapId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<MemoProductMap> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<MemoProductMap>()
                   select e;
        }

        protected override MemoProductMap GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<MemoProductMap>()
                         where e.MemoProductMapId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<MemoProductMapInfo> GetMemoProductMaps()
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.MemoProductMapSet
                            join b in entityContext.ProductSet on a.ProductCode equals b.Code into bparents
                            from bp in bparents.DefaultIfEmpty()
                            join c in entityContext.MemoUnitsSet on a.Code equals c.Code into cparents
                            from cp in cparents.DefaultIfEmpty()
                            select new MemoProductMapInfo()
                            {
                                MemoProductMap = a,
                                Product = bp,
                                MemoUnits = cp
                            };

                return query.ToFullyLoaded();
            }
        }

    }
}
