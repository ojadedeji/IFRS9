using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Budget.Entities;
using Fintrak.Data.Budget.Contracts;
using Fintrak.Shared.Budget.Framework.Enums;

namespace Fintrak.Data.Budget
{
    [Export(typeof(IRevenueCaptionRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RevenueCaptionRepository : DataRepositoryBase<RevenueCaption>, IRevenueCaptionRepository
    {

        protected override RevenueCaption AddEntity(BudgetContext entityContext, RevenueCaption entity)
        {
            return entityContext.Set<RevenueCaption>().Add(entity);
        }

        protected override RevenueCaption UpdateEntity(BudgetContext entityContext, RevenueCaption entity)
        {
            return (from e in entityContext.Set<RevenueCaption>() 
                    where e.RevenueCaptionId == entity.RevenueCaptionId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<RevenueCaption> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<RevenueCaption>()
                   select e;
        }

        protected override RevenueCaption GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<RevenueCaption>()
                         where e.RevenueCaptionId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<RevenueCaption> GetRevenueCaptions(string year, string reviewCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.RevenueCaptionSet
                            where a.Year == year && a.ReviewCode == reviewCode
                                select a;

                return query.ToFullyLoaded();
            }
        }
      
    }
}
