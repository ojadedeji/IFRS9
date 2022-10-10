using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IInstrumentTypeGLMapRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class InstrumentTypeGLMapRepository : DataRepositoryBase<InstrumentTypeGLMap>, IInstrumentTypeGLMapRepository
    {

        protected override InstrumentTypeGLMap AddEntity(IFRSContext entityContext, InstrumentTypeGLMap entity)
        {
            return entityContext.Set<InstrumentTypeGLMap>().Add(entity);
        }

        protected override InstrumentTypeGLMap UpdateEntity(IFRSContext entityContext, InstrumentTypeGLMap entity)
        {
            return (from e in entityContext.Set<InstrumentTypeGLMap>() 
                    where e.InstrumentTypeGLMapId == entity.InstrumentTypeGLMapId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<InstrumentTypeGLMap> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<InstrumentTypeGLMap>()
                   select e;
        }

        protected override InstrumentTypeGLMap GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<InstrumentTypeGLMap>()
                         where e.InstrumentTypeGLMapId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<InstrumentTypeGLMapInfo> GetInstrumentTypeGLMaps()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.InstrumentTypeGLMapSet
                            join b in entityContext.InstrumentTypeSet on a.InstrumentTypeId equals b.InstrumentTypeId
                            join c in entityContext.GLTypeSet on a.GLTypeId equals c.GLTypeId
                            join d in entityContext.GLMappingSet on a.GLCode equals d.GLCode
                            select new InstrumentTypeGLMapInfo()
                            {
                                InstrumentTypeGLMap = a,
                                InstrumentType = b,
                                GLType = c,
                                GLMapping = d
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
