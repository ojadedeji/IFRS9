using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IInternalRatingBasedRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class InternalRatingBasedRepository : DataRepositoryBase<InternalRatingBased>, IInternalRatingBasedRepository
    {
        protected override InternalRatingBased AddEntity(IFRSContext entityContext, InternalRatingBased entity)
        {
            return entityContext.Set<InternalRatingBased>().Add(entity);
        }

        protected override InternalRatingBased UpdateEntity(IFRSContext entityContext, InternalRatingBased entity)
        {
            return (from e in entityContext.Set<InternalRatingBased>()
                    where e.InternalRatingBasedId == entity.InternalRatingBasedId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<InternalRatingBased> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<InternalRatingBased>()
                   select e;
        }

        protected override InternalRatingBased GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<InternalRatingBased>()
                         where e.InternalRatingBasedId == id
                         select e).OrderBy(c => c.Rank).ToList();

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}