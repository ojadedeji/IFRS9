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
    [Export(typeof(IProductGroupRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProductGroupRepository : DataRepositoryBase<ProductGroup>, IProductGroupRepository
    {

        protected override ProductGroup AddEntity(BudgetContext entityContext, ProductGroup entity)
        {
            return entityContext.Set<ProductGroup>().Add(entity);
        }

        protected override ProductGroup UpdateEntity(BudgetContext entityContext, ProductGroup entity)
        {
            return (from e in entityContext.Set<ProductGroup>() 
                    where e.ProductGroupId == entity.ProductGroupId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ProductGroup> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<ProductGroup>()
                   select e;
        }

        protected override ProductGroup GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ProductGroup>()
                         where e.ProductGroupId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ProductGroupInfo> GetProductGroups(string year, string reviewCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.ProductGroupSet
                            join b in entityContext.ProductGroupSet on a.ParentCode equals b.Code into bparents
                            from bp in bparents.Where(bpt => (a.Year == bpt.Year && a.ReviewCode == bpt.ReviewCode)).DefaultIfEmpty()
                            where a.Year == year && a.ReviewCode == reviewCode
                            select new ProductGroupInfo()
                            {
                                ProductGroup = a,
                                Parent = bp
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
