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
    [Export(typeof(IFeeCategoryRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FeeCategoryRepository : DataRepositoryBase<FeeCategory>, IFeeCategoryRepository
    {

        protected override FeeCategory AddEntity(BudgetContext entityContext, FeeCategory entity)
        {
            return entityContext.Set<FeeCategory>().Add(entity);
        }

        protected override FeeCategory UpdateEntity(BudgetContext entityContext, FeeCategory entity)
        {
            return (from e in entityContext.Set<FeeCategory>() 
                    where e.FeeCategoryId == entity.FeeCategoryId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<FeeCategory> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<FeeCategory>()
                   select e;
        }

        protected override FeeCategory GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<FeeCategory>()
                         where e.FeeCategoryId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<FeeCategory> GetFeeCategories(string year, string reviewCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.FeeCategorySet
                            
                            where a.Year == year && a.ReviewCode == reviewCode
                            select a;

                return query.ToFullyLoaded();
            }
        }
      
    }
}
