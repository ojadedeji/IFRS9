using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IHistoricalSectorRatingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class HistoricalSectorRatingRepository : DataRepositoryBase<HistoricalSectorRating>, IHistoricalSectorRatingRepository
    {
        protected override HistoricalSectorRating AddEntity(IFRSContext entityContext, HistoricalSectorRating entity)
        {
            return entityContext.Set<HistoricalSectorRating>().Add(entity);
        }

        protected override HistoricalSectorRating UpdateEntity(IFRSContext entityContext, HistoricalSectorRating entity)
        {
            return (from e in entityContext.Set<HistoricalSectorRating>()
                    where e.HistoricalSectorRatingId == entity.HistoricalSectorRatingId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<HistoricalSectorRating> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<HistoricalSectorRating>()
                   select e;
        }

        protected override HistoricalSectorRating GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<HistoricalSectorRating>()
                         where e.HistoricalSectorRatingId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}