using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(INonProductMapRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class NonProductMapRepository : DataRepositoryBase<NonProductMap>, INonProductMapRepository
    {

        protected override NonProductMap AddEntity(MPRContext entityContext, NonProductMap entity)
        {
            return entityContext.Set<NonProductMap>().Add(entity);
        }

        protected override NonProductMap UpdateEntity(MPRContext entityContext, NonProductMap entity)
        {
            return (from e in entityContext.Set<NonProductMap>() 
                    where e.NonProductMapId == entity.NonProductMapId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<NonProductMap> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<NonProductMap>()
                   select e;
        }

        protected override NonProductMap GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<NonProductMap>()
                         where e.NonProductMapId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<NonProductMapInfo> GetNonProductMaps()
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.NonProductMapSet
                            join b in entityContext.ProductSet on a.NonProductCode equals b.Code
                            //join c in entityContext.ProductSet on a.ProductCode equals c.Code
                            join d in entityContext.BSCaptionSet on a.CaptionCode equals d.CaptionCode
                            select new NonProductMapInfo()
                            {
                                NonProductMap = a,
                                NonProduct = b,
                                //Product = c,
                                BSCaption = d
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
