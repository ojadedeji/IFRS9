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
    [Export(typeof(IOpexItemRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OpexItemRepository : DataRepositoryBase<OpexItem>, IOpexItemRepository
    {

        protected override OpexItem AddEntity(BudgetContext entityContext, OpexItem entity)
        {
            return entityContext.Set<OpexItem>().Add(entity);
        }

        protected override OpexItem UpdateEntity(BudgetContext entityContext, OpexItem entity)
        {
            return (from e in entityContext.Set<OpexItem>() 
                    where e.OpexItemId == entity.OpexItemId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<OpexItem> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<OpexItem>()
                   select e;
        }

        protected override OpexItem GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<OpexItem>()
                         where e.OpexItemId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<OpexItemInfo> GetOpexItems(string year, string reviewCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.OpexItemSet
                            join b in entityContext.OpexCategorySet on a.CategoryCode equals b.Code into bparents
                            from bp in bparents.Where(bpt => (a.Year == bpt.Year && a.ReviewCode == bpt.ReviewCode)).DefaultIfEmpty()
                            where a.Year == year && a.ReviewCode == reviewCode
                            select new OpexItemInfo()
                            {
                                OpexItem = a,
                                OpexCategory = bp
                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<OpexItemInfo> GetOpexItems(string year, string reviewCode,string categoryCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.OpexItemSet
                            join b in entityContext.OpexCategorySet on a.CategoryCode equals b.Code into bparents
                            from bp in bparents.Where(bpt => (a.Year == bpt.Year && a.ReviewCode == bpt.ReviewCode)).DefaultIfEmpty()
                            where a.Year == year && a.ReviewCode == reviewCode && bp.Code == categoryCode
                            select new OpexItemInfo()
                            {
                                OpexItem = a,
                                OpexCategory = bp
                            };

                return query.ToFullyLoaded();
            }
        }

    }
}
