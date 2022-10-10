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
    [Export(typeof(ICapexCategoryRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CapexCategoryRepository : DataRepositoryBase<CapexCategory>, ICapexCategoryRepository
    {

        protected override CapexCategory AddEntity(BudgetContext entityContext, CapexCategory entity)
        {
            return entityContext.Set<CapexCategory>().Add(entity);
        }

        protected override CapexCategory UpdateEntity(BudgetContext entityContext, CapexCategory entity)
        {
            return (from e in entityContext.Set<CapexCategory>() 
                    where e.CapexCategoryId == entity.CapexCategoryId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CapexCategory> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<CapexCategory>()
                   select e;
        }

        protected override CapexCategory GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CapexCategory>()
                         where e.CapexCategoryId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<CapexCategoryInfo> GetCapexCategories(string year, string reviewCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.CapexCategorySet
                            join b in entityContext.CapexCategorySet on a.ParentCode equals b.Code into bparents
                            from bp in bparents.Where(bpt => (a.Year == bpt.Year && a.ReviewCode == bpt.ReviewCode)).DefaultIfEmpty()
                            where a.Year == year && a.ReviewCode == reviewCode
                            select new CapexCategoryInfo()
                            {
                                CapexCategory = a,
                                Parent = bp
                            };

                return query.ToFullyLoaded();
            }
        }
        public IEnumerable<CapexCategoryInfo> GetAllCapexCategories()
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.CapexCategorySet
                            join b in entityContext.CapexCategorySet on a.ParentCode equals b.Code into bparents
                            from bp in bparents.Where(bpt => (a.Year == bpt.Year && a.ReviewCode == bpt.ReviewCode)).DefaultIfEmpty()                           
                            select new CapexCategoryInfo()
                            {
                                CapexCategory = a,
                                Parent = bp
                            };

                return query.ToFullyLoaded();
            }
        }
    }
}
