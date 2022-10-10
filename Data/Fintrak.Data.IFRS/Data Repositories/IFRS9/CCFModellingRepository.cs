using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ICCFModellingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CCFModellingRepository : DataRepositoryBase<CCFModelling>, ICCFModellingRepository
    {
        protected override CCFModelling AddEntity(IFRSContext entityContext, CCFModelling entity)
        {
            return entityContext.Set<CCFModelling>().Add(entity);
        }

        protected override CCFModelling UpdateEntity(IFRSContext entityContext, CCFModelling entity)
        {
            return (from e in entityContext.Set<CCFModelling>()
                    where e.CCFModellingId == entity.CCFModellingId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CCFModelling> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<CCFModelling>()
                   select e;
        }

        protected override CCFModelling GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CCFModelling>()
                         where e.CCFModellingId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}