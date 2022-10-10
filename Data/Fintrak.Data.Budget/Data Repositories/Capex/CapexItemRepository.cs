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
    [Export(typeof(ICapexItemRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CapexItemRepository : DataRepositoryBase<CapexItem>, ICapexItemRepository
    {

        protected override CapexItem AddEntity(BudgetContext entityContext, CapexItem entity)
        {
            return entityContext.Set<CapexItem>().Add(entity);
        }

        protected override CapexItem UpdateEntity(BudgetContext entityContext, CapexItem entity)
        {
            return (from e in entityContext.Set<CapexItem>() 
                    where e.CapexItemId == entity.CapexItemId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CapexItem> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<CapexItem>()
                   select e;
        }

        protected override CapexItem GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CapexItem>()
                         where e.CapexItemId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<CapexItemInfo> GetCapexItems(string year, string reviewCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.CapexItemSet
                            join b in entityContext.CapexCategorySet on a.CategoryCode equals b.Code into bparents
                            from bp in bparents.Where(bpt => (a.Year == bpt.Year && a.ReviewCode == bpt.ReviewCode)).DefaultIfEmpty()
                            where a.Year == year && a.ReviewCode == reviewCode
                            select new CapexItemInfo()
                            {
                                CapexItem = a,
                                CapexCategory = bp
                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<CapexItemInfo> GetCapexItems(string year, string reviewCode,string categoryCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.CapexItemSet
                            join b in entityContext.CapexCategorySet on a.CategoryCode equals b.Code into bparents
                            from bp in bparents.Where(bpt => (a.Year == bpt.Year && a.ReviewCode == bpt.ReviewCode)).DefaultIfEmpty()
                            where a.Year == year && a.ReviewCode == reviewCode && bp.Code == categoryCode
                            select new CapexItemInfo()
                            {
                                CapexItem = a,
                                CapexCategory = bp
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
