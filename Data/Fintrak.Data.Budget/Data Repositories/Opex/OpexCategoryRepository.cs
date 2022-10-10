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
    [Export(typeof(IOpexCategoryRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OpexCategoryRepository : DataRepositoryBase<OpexCategory>, IOpexCategoryRepository
    {

        protected override OpexCategory AddEntity(BudgetContext entityContext, OpexCategory entity)
        {
            return entityContext.Set<OpexCategory>().Add(entity);
        }

        protected override OpexCategory UpdateEntity(BudgetContext entityContext, OpexCategory entity)
        {
            return (from e in entityContext.Set<OpexCategory>() 
                    where e.OpexCategoryId == entity.OpexCategoryId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<OpexCategory> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<OpexCategory>()
                   select e;
        }

        protected override OpexCategory GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<OpexCategory>()
                         where e.OpexCategoryId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<OpexCategoryInfo> GetOpexCategories(string year, string reviewCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.OpexCategorySet
                            join b in entityContext.OpexCategorySet on a.ParentCode equals b.Code into bparents
                            from bp in bparents.Where(bpt => (a.Year == bpt.Year && a.ReviewCode == bpt.ReviewCode)).DefaultIfEmpty()
                            where a.Year == year && a.ReviewCode == reviewCode
                            select new OpexCategoryInfo()
                            {
                                OpexCategory = a,
                                Parent = bp
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
