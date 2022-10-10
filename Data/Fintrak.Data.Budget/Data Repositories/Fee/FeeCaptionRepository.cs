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
    [Export(typeof(IFeeCaptionRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FeeCaptionRepository : DataRepositoryBase<FeeCaption>, IFeeCaptionRepository
    {

        protected override FeeCaption AddEntity(BudgetContext entityContext, FeeCaption entity)
        {
            return entityContext.Set<FeeCaption>().Add(entity);
        }

        protected override FeeCaption UpdateEntity(BudgetContext entityContext, FeeCaption entity)
        {
            return (from e in entityContext.Set<FeeCaption>() 
                    where e.FeeCaptionId == entity.FeeCaptionId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<FeeCaption> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<FeeCaption>()
                   select e;
        }

        protected override FeeCaption GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<FeeCaption>()
                         where e.FeeCaptionId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<FeeCaption> GetFeeCaptions(string year, string reviewCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.FeeCaptionSet
                            where a.Year == year && a.ReviewCode == reviewCode
                            select a;

                return query.ToFullyLoaded();
            }
        }
      
    }
}
