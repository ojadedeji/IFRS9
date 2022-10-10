using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IBSGLMappingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BSGLMappingRepository : DataRepositoryBase<BSGLMapping>, IBSGLMappingRepository
    {

        protected override BSGLMapping AddEntity(MPRContext entityContext, BSGLMapping entity)
        {
            return entityContext.Set<BSGLMapping>().Add(entity);
        }

        protected override BSGLMapping UpdateEntity(MPRContext entityContext, BSGLMapping entity)
        {
            return (from e in entityContext.Set<BSGLMapping>()
                    where e.BSGLMappingId == entity.BSGLMappingId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<BSGLMapping> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<BSGLMapping>()
                   select e;
        }

        protected override BSGLMapping GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<BSGLMapping>()
                         where e.BSGLMappingId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<BSGLMappingInfo> GetBSGLMappings()
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.BSGLMappingSet
                            join b in entityContext.ProductSet on a.ProductCode equals b.Code into ab
                            from abi in ab.DefaultIfEmpty()
                            join c in entityContext.GLDefinitionSet on a.GLCode equals c.GL_Code into ac
                            from aci in ac.DefaultIfEmpty()
                            
                            select new BSGLMappingInfo()
                            {
                                BSGLMapping = a,
                                Product = abi,
                                GLDefinition = aci
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
