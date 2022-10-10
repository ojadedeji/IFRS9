using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IRatioCaptionMappingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RatioCaptionMappingRepository : DataRepositoryBase<RatioCaptionMapping>, IRatioCaptionMappingRepository
    {

        protected override RatioCaptionMapping AddEntity(MPRContext entityContext, RatioCaptionMapping entity)
        {
            return entityContext.Set<RatioCaptionMapping>().Add(entity);
        }

        protected override RatioCaptionMapping UpdateEntity(MPRContext entityContext, RatioCaptionMapping entity)
        {
            return (from e in entityContext.Set<RatioCaptionMapping>()
                    where e.RatioCaptionMappingId == entity.RatioCaptionMappingId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<RatioCaptionMapping> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<RatioCaptionMapping>()
                   select e;
        }

        protected override RatioCaptionMapping GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<RatioCaptionMapping>()
                         where e.RatioCaptionMappingId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}
