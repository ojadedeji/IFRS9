using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Budget.Entities;
using Fintrak.Data.Budget.Contracts;

namespace Fintrak.Data.Budget
{
    [Export(typeof(IProductRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProductRepository : DataRepositoryBase<Product>, IProductRepository
    {

        protected override Product AddEntity(BudgetContext entityContext, Product entity)
        {
            return entityContext.Set<Product>().Add(entity);
        }

        protected override Product UpdateEntity(BudgetContext entityContext, Product entity)
        {
            return (from e in entityContext.Set<Product>() 
                    where e.ProductId == entity.ProductId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Product> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<Product>()
                   select e;
        }

        protected override Product GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<Product>()
                         where e.ProductId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ProductInfo> GetProducts(string year, string reviewCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.ProductSet

                            join b in entityContext.ProductGroupSet on a.GroupCode equals b.Code into bparents
                            from bp in bparents.Where(bpt => (a.Year == bpt.Year && a.ReviewCode == bpt.ReviewCode)).DefaultIfEmpty()

                            join c in entityContext.ProductCategorySet on a.CategoryCode equals c.Code into cparents
                            from cp in cparents.Where(cpt => (a.Year == cpt.Year && a.ReviewCode == cpt.ReviewCode)).DefaultIfEmpty()

                            join d in entityContext.ProductClassificationSet on a.ClassificationCode equals d.Code into dparents
                            from dp in dparents.Where(dpt => (a.Year == dpt.Year && a.ReviewCode == dpt.ReviewCode)).DefaultIfEmpty()

                            join e in entityContext.RevenueCaptionSet on a.ClassificationCode equals e.Code into eparents
                            from ep in eparents.Where(ept => (a.Year == ept.Year && a.ReviewCode == ept.ReviewCode)).DefaultIfEmpty()

                            where a.Year == year && a.ReviewCode == reviewCode
                            select new ProductInfo()
                            {
                                Product = a,
                                ProductGroup = bp,
                                ProductCategory = cp,
                                ProductClassification = dp,
                                RevenueCaption = ep
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
