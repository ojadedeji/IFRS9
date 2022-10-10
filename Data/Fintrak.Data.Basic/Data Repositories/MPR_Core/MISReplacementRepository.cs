using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;
using Fintrak.Shared.Basic.Framework;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IMISReplacementRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MISReplacementRepository : DataRepositoryBase<MISReplacement>, IMISReplacementRepository
    {

        protected override MISReplacement AddEntity(BasicContext entityContext, MISReplacement entity)
        {
            return entityContext.Set<MISReplacement>().Add(entity);
        }

        protected override MISReplacement UpdateEntity(BasicContext entityContext, MISReplacement entity)
        {
            return (from e in entityContext.Set<MISReplacement>() 
                    where e.MISReplacementId == entity.MISReplacementId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<MISReplacement> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<MISReplacement>()
                   select e;
        }

        protected override MISReplacement GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<MISReplacement>()
                         where e.MISReplacementId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
