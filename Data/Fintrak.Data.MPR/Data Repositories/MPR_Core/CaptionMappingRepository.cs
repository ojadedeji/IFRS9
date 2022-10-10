using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(ICaptionMappingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CaptionMappingRepository : DataRepositoryBase<CaptionMapping>, ICaptionMappingRepository
    {

        protected override CaptionMapping AddEntity(MPRContext entityContext, CaptionMapping entity)
        {
            return entityContext.Set<CaptionMapping>().Add(entity);
        }

        protected override CaptionMapping UpdateEntity(MPRContext entityContext, CaptionMapping entity)
        {
            return (from e in entityContext.Set<CaptionMapping>()
                    where e.CaptionMappingId == entity.CaptionMappingId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CaptionMapping> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<CaptionMapping>()
                   select e;
        }

        protected override CaptionMapping GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CaptionMapping>()
                         where e.CaptionMappingId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}
