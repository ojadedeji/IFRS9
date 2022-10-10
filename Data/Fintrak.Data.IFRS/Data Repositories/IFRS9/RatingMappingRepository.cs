using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IRatingMappingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RatingMappingRepository : DataRepositoryBase<RatingMapping>, IRatingMappingRepository
    {
        protected override RatingMapping AddEntity(IFRSContext entityContext, RatingMapping entity)
        {
            return entityContext.Set<RatingMapping>().Add(entity);
        }

        protected override RatingMapping UpdateEntity(IFRSContext entityContext, RatingMapping entity)
        {
            return (from e in entityContext.Set<RatingMapping>()
                    where e.RatingMappingId == entity.RatingMappingId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<RatingMapping> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<RatingMapping>()
                   select e;
        }

        protected override RatingMapping GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<RatingMapping>()
                         where e.RatingMappingId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<RatingMappingInfo> GetRatingMappings()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.RatingMappingSet
                            join b in entityContext.ExternalRatingSet on a.External_Rating_Id equals b.ExternalRatingId
                            join c in entityContext.InternalRatingBasedSet on a.Credit_Risk_Id equals c.InternalRatingBasedId
                            select new RatingMappingInfo()
                            {
                                RatingMapping = a,
                                ExternalRating = b,
                                InternalRatingBased = c
                            };

                return query.ToFullyLoaded();
            }
        }
       
    }
}