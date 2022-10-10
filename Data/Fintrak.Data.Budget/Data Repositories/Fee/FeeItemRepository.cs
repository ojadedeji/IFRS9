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
    [Export(typeof(IFeeItemRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FeeItemRepository : DataRepositoryBase<FeeItem>, IFeeItemRepository
    {

        protected override FeeItem AddEntity(BudgetContext entityContext, FeeItem entity)
        {
            return entityContext.Set<FeeItem>().Add(entity);
        }

        protected override FeeItem UpdateEntity(BudgetContext entityContext, FeeItem entity)
        {
            return (from e in entityContext.Set<FeeItem>() 
                    where e.FeeItemId == entity.FeeItemId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<FeeItem> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<FeeItem>()
                   select e;
        }

        protected override FeeItem GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<FeeItem>()
                         where e.FeeItemId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<FeeItemInfo> GetFeeItems(string year, string reviewCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.FeeItemSet
                            join b in entityContext.FeeGroupSet on a.GroupCode equals b.Code into bparents
                            from bp in bparents.Where(bpt => (a.Year == bpt.Year && a.ReviewCode == bpt.ReviewCode)).DefaultIfEmpty()
                            join c in entityContext.FeeCaptionSet on a.CaptionCode equals c.Code into cparents
                            from cp in cparents.Where(cpt => (a.Year == cpt.Year && a.ReviewCode == cpt.ReviewCode)).DefaultIfEmpty()
                            join d in entityContext.FeeCategorySet on a.CategoryCode equals d.Code into dparents
                            from dp in dparents.Where(dpt => (a.Year == dpt.Year && a.ReviewCode == dpt.ReviewCode)).DefaultIfEmpty()

                            where a.Year == year && a.ReviewCode == reviewCode
                            select new FeeItemInfo()
                            {
                                FeeItem = a,
                                FeeGroup = bp,
                                FeeCaption = cp,
                                FeeCategory = dp
                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<FeeItemInfo> GetFeeItems(string year, string reviewCode,string categoryCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.FeeItemSet
                            join b in entityContext.FeeGroupSet on a.GroupCode equals b.Code into bparents
                            from bp in bparents.Where(bpt => (a.Year == bpt.Year && a.ReviewCode == bpt.ReviewCode)).DefaultIfEmpty()
                            join c in entityContext.FeeCaptionSet on a.CaptionCode equals c.Code into cparents
                            from cp in cparents.Where(cpt => (a.Year == cpt.Year && a.ReviewCode == cpt.ReviewCode)).DefaultIfEmpty()
                            join d in entityContext.FeeCategorySet on a.CategoryCode equals d.Code into dparents
                            from dp in dparents.Where(dpt => (a.Year == dpt.Year && a.ReviewCode == dpt.ReviewCode)).DefaultIfEmpty()

                            where a.Year == year && a.ReviewCode == reviewCode && a.CategoryCode == categoryCode 
                            select new FeeItemInfo()
                            {
                                FeeItem = a,
                                FeeGroup = bp,
                                FeeCaption = cp,
                                FeeCategory = dp
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
