using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ICreditRiskRatingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CreditRiskRatingRepository : DataRepositoryBase<CreditRiskRating>, ICreditRiskRatingRepository
    {
        protected override CreditRiskRating AddEntity(IFRSContext entityContext, CreditRiskRating entity)
        {
            return entityContext.Set<CreditRiskRating>().Add(entity);
        }

        protected override CreditRiskRating UpdateEntity(IFRSContext entityContext, CreditRiskRating entity)
        {
            return (from e in entityContext.Set<CreditRiskRating>()
                    where e.CreditRiskRatingId == entity.CreditRiskRatingId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CreditRiskRating> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<CreditRiskRating>()
                   select e;
        }

        protected override CreditRiskRating GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CreditRiskRating>()
                         where e.CreditRiskRatingId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<string> GetRiskRatingCode()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (entityContext.CreditRiskRatingSet.Select<CreditRiskRating, string>(r => r.Code)).Distinct();

                return query.ToFullyLoaded();
            }
        }
        
    }
}
