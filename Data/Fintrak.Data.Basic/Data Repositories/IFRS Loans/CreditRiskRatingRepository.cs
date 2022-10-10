using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(ICreditRiskRatingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CreditRiskRatingRepository : DataRepositoryBase<CreditRiskRating>, ICreditRiskRatingRepository
    {
        protected override CreditRiskRating AddEntity(BasicContext entityContext, CreditRiskRating entity)
        {
            return entityContext.Set<CreditRiskRating>().Add(entity);
        }

        protected override CreditRiskRating UpdateEntity(BasicContext entityContext, CreditRiskRating entity)
        {
            return (from e in entityContext.Set<CreditRiskRating>()
                    where e.CreditRiskRatingId == entity.CreditRiskRatingId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CreditRiskRating> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<CreditRiskRating>()
                   select e;
        }

        protected override CreditRiskRating GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CreditRiskRating>()
                         where e.CreditRiskRatingId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
 
    }
}
