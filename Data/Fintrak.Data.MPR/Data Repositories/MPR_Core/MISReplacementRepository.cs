using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;
using Fintrak.Shared.MPR.Framework;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IMISReplacementRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MISReplacementRepository : DataRepositoryBase<MISReplacement>, IMISReplacementRepository
    {

        protected override MISReplacement AddEntity(MPRContext entityContext, MISReplacement entity)
        {
            return entityContext.Set<MISReplacement>().Add(entity);
        }

        protected override MISReplacement UpdateEntity(MPRContext entityContext, MISReplacement entity)
        {
            return (from e in entityContext.Set<MISReplacement>() 
                    where e.MISReplacementId == entity.MISReplacementId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<MISReplacement> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<MISReplacement>()
                   select e;
        }

        protected override MISReplacement GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<MISReplacement>()
                         where e.MISReplacementId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
