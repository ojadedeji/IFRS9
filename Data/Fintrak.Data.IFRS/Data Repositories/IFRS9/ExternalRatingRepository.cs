using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IExternalRatingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ExternalRatingRepository : DataRepositoryBase<ExternalRating>, IExternalRatingRepository
    {
        protected override ExternalRating AddEntity(IFRSContext entityContext, ExternalRating entity)
        {
            return entityContext.Set<ExternalRating>().Add(entity);
        }

        protected override ExternalRating UpdateEntity(IFRSContext entityContext, ExternalRating entity)
        {
            return (from e in entityContext.Set<ExternalRating>()
                    where e.ExternalRatingId == entity.ExternalRatingId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ExternalRating> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<ExternalRating>()
                   select e;
        }

        protected override ExternalRating GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ExternalRating>()
                         where e.ExternalRatingId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}