using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ISandPRatingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SandPRatingRepository : DataRepositoryBase<SandPRating>, ISandPRatingRepository
    {
        protected override SandPRating AddEntity(IFRSContext entityContext, SandPRating entity)
        {
            return entityContext.Set<SandPRating>().Add(entity);
        }

        protected override SandPRating UpdateEntity(IFRSContext entityContext, SandPRating entity)
        {
            return (from e in entityContext.Set<SandPRating>()
                    where e.SandPRating_Id == entity.SandPRating_Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<SandPRating> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<SandPRating>()
                   select e;
        }

        protected override SandPRating GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<SandPRating>()
                         where e.SandPRating_Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}