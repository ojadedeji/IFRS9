using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Budget.Framework.Enums;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Budget.Entities;
using Fintrak.Data.Budget.Contracts;

namespace Fintrak.Data.Budget
{
    [Export(typeof(IDepreciationRateRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DepreciationRateRepository : DataRepositoryBase<DepreciationRate>, IDepreciationRateRepository
    {

        protected override DepreciationRate AddEntity(BudgetContext entityContext, DepreciationRate entity)
        {
            return entityContext.Set<DepreciationRate>().Add(entity);
        }

        protected override DepreciationRate UpdateEntity(BudgetContext entityContext, DepreciationRate entity)
        {
            return (from e in entityContext.Set<DepreciationRate>() 
                    where e.DepreciationRateId == entity.DepreciationRateId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<DepreciationRate> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<DepreciationRate>()
                   select e;
        }

        protected override DepreciationRate GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<DepreciationRate>()
                         where e.DepreciationRateId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<DepreciationRateInfo> GetDepreciationRates(string year, string reviewCode, string categoryCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.DepreciationRateSet
                            join b in entityContext.CapexCategorySet on a.CategoryCode equals b.Code into bparents
                            from bp in bparents.Where(bpt => (a.Year == bpt.Year && a.ReviewCode == bpt.ReviewCode)).DefaultIfEmpty()

                            where a.Year == year && a.ReviewCode == reviewCode && bp.Code == categoryCode

                            select new DepreciationRateInfo()
                            {
                                DepreciationRate = a,
                                CapexCategory = bp
                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<DepreciationRateInfo> GetDepreciationRates(string year, string reviewCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.DepreciationRateSet
                            join b in entityContext.CapexCategorySet on a.CategoryCode equals b.Code into bparents
                            from bp in bparents.Where(bpt => (a.Year == bpt.Year && a.ReviewCode == bpt.ReviewCode)).DefaultIfEmpty()
                            
                            where a.Year == year && a.ReviewCode == reviewCode 

                            select new DepreciationRateInfo()
                            {
                                DepreciationRate = a,
                                CapexCategory = bp
                            };

                return query.ToFullyLoaded();
            }
        }
    }
}
