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
    [Export(typeof(IProductClassificationRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProductClassificationRepository : DataRepositoryBase<ProductClassification>, IProductClassificationRepository
    {

        protected override ProductClassification AddEntity(BudgetContext entityContext, ProductClassification entity)
        {
            return entityContext.Set<ProductClassification>().Add(entity);
        }

        protected override ProductClassification UpdateEntity(BudgetContext entityContext, ProductClassification entity)
        {
            return (from e in entityContext.Set<ProductClassification>() 
                    where e.ProductClassificationId == entity.ProductClassificationId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ProductClassification> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<ProductClassification>()
                   select e;
        }

        protected override ProductClassification GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ProductClassification>()
                         where e.ProductClassificationId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ProductClassification> GetProductClassifications(string year, string reviewCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.ProductClassificationSet
                            where a.Year == year && a.ReviewCode == reviewCode
                                select a;

                return query.ToFullyLoaded();
            }
        }
      
    }
}
