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
    [Export(typeof(IProductCategoryRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProductCategoryRepository : DataRepositoryBase<ProductCategory>, IProductCategoryRepository
    {

        protected override ProductCategory AddEntity(BudgetContext entityContext, ProductCategory entity)
        {
            return entityContext.Set<ProductCategory>().Add(entity);
        }

        protected override ProductCategory UpdateEntity(BudgetContext entityContext, ProductCategory entity)
        {
            return (from e in entityContext.Set<ProductCategory>() 
                    where e.ProductCategoryId == entity.ProductCategoryId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ProductCategory> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<ProductCategory>()
                   select e;
        }

        protected override ProductCategory GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ProductCategory>()
                         where e.ProductCategoryId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ProductCategory> GetProductCategories(string year, string reviewCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.ProductCategorySet
                            where a.Year == year && a.ReviewCode == reviewCode
                                select a;

                return query.ToFullyLoaded();
            }
        }
      
    }
}
