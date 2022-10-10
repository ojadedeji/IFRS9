using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IGLMappingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GLMappingRepository : DataRepositoryBase<GLMapping>, IGLMappingRepository
    {

        protected override GLMapping AddEntity(BasicContext entityContext, GLMapping entity)
        {
            return entityContext.Set<GLMapping>().Add(entity);
        }

        protected override GLMapping UpdateEntity(BasicContext entityContext, GLMapping entity)
        {
            return (from e in entityContext.Set<GLMapping>() 
                    where e.GLMappingId == entity.GLMappingId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<GLMapping> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<GLMapping>()
                   select e;
        }

        protected override GLMapping GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<GLMapping>()
                         where e.GLMappingId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<GLMappingInfo> GetGLMappings()
        {
            using (BasicContext entityContext = new BasicContext())
            {
                var query = from a in entityContext.GLMappingSet
                            join b in entityContext.IFRSRegistrySet on a.CaptionCode equals b.Code 
                            select new GLMappingInfo()
                            {
                                GLMapping = a,
                                IFRSRegistry = b
                            };

                return query.ToFullyLoaded();
            }
        }

 
    }
}
