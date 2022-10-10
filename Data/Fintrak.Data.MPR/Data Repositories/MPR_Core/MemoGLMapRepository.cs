using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IMemoGLMapRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MemoGLMapRepository : DataRepositoryBase<MemoGLMap>, IMemoGLMapRepository
    {

        protected override MemoGLMap AddEntity(MPRContext entityContext, MemoGLMap entity)
        {
            return entityContext.Set<MemoGLMap>().Add(entity);
        }

        protected override MemoGLMap UpdateEntity(MPRContext entityContext, MemoGLMap entity)
        {
            return (from e in entityContext.Set<MemoGLMap>() 
                    where e.MemoGLMapId == entity.MemoGLMapId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<MemoGLMap> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<MemoGLMap>()
                   select e;
        }

        protected override MemoGLMap GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<MemoGLMap>()
                         where e.MemoGLMapId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<MemoGLMapInfo> GetMemoGLMaps()
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.MemoGLMapSet
                            //join b in entityContext.GLDefinitionSet on a.GLCode equals b.GL_Code
                            join b in entityContext.GLDefinitionSet on a.GLCode equals b.GL_Code into bparents
                            from bp in bparents.DefaultIfEmpty()
                            join c in entityContext.MemoUnitsSet on a.Code equals c.Code into cparents
                            from cp in cparents.DefaultIfEmpty()
                            select new MemoGLMapInfo()
                            {
                                MemoGLMap = a,
                                GLDefinition = bp,
                                MemoUnits = cp
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
