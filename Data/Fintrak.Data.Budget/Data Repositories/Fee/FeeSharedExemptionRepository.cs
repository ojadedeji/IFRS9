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
    [Export(typeof(IFeeSharedExemptionRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FeeSharedExemptionRepository : DataRepositoryBase<FeeSharedExemption>, IFeeSharedExemptionRepository
    {

        protected override FeeSharedExemption AddEntity(BudgetContext entityContext, FeeSharedExemption entity)
        {
            return entityContext.Set<FeeSharedExemption>().Add(entity);
        }

        protected override FeeSharedExemption UpdateEntity(BudgetContext entityContext, FeeSharedExemption entity)
        {
            return (from e in entityContext.Set<FeeSharedExemption>() 
                    where e.FeeSharedExemptionId == entity.FeeSharedExemptionId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<FeeSharedExemption> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<FeeSharedExemption>()
                   select e;
        }

        protected override FeeSharedExemption GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<FeeSharedExemption>()
                         where e.FeeSharedExemptionId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<FeeSharedExemptionInfo> GetFeeSharedExemptions(string year, string reviewCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.FeeSharedExemptionSet
                            join b in entityContext.FeeItemSet on a.ItemCode equals b.Code into bparents
                            from bp in bparents.Where(bpt => (a.Year == bpt.Year && a.ReviewCode == bpt.ReviewCode)).DefaultIfEmpty()
                            where a.Year == year && a.ReviewCode == reviewCode
                            select new FeeSharedExemptionInfo()
                            {
                                FeeSharedExemption = a,
                                FeeItem = bp
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
