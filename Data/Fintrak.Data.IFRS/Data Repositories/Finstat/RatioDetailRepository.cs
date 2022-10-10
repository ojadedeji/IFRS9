using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IRatioDetailRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RatioDetailRepository : DataRepositoryBase<RatioDetail>, IRatioDetailRepository
    {
        protected override RatioDetail AddEntity(IFRSContext entityContext, RatioDetail entity)
        {
            return entityContext.Set<RatioDetail>().Add(entity);
        }

        protected override RatioDetail UpdateEntity(IFRSContext entityContext, RatioDetail entity)
        {
            return (from e in entityContext.Set<RatioDetail>()
                    where e.RatioID == entity.RatioID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<RatioDetail> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<RatioDetail>()
                   select e;
        }

        protected override RatioDetail GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<RatioDetail>()
                         where e.RatioID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}