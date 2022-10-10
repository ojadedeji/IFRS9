using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IRevenueGLMappingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RevenueGLMappingRepository : DataRepositoryBase<RevenueGLMapping>, IRevenueGLMappingRepository
    {

        protected override RevenueGLMapping AddEntity(IFRSContext entityContext, RevenueGLMapping entity)
        {
            return entityContext.Set<RevenueGLMapping>().Add(entity);
        }

        protected override RevenueGLMapping UpdateEntity(IFRSContext entityContext, RevenueGLMapping entity)
        {
            return (from e in entityContext.Set<RevenueGLMapping>()
                    where e.GLMappingId == entity.GLMappingId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<RevenueGLMapping> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<RevenueGLMapping>()
                   select e;
        }

        protected override RevenueGLMapping GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<RevenueGLMapping>()
                         where e.GLMappingId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
