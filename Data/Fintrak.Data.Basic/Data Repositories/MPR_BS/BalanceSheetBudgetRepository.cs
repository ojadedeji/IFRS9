using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IBalanceSheetBudgetRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BalanceSheetBudgetRepository : DataRepositoryBase<BalanceSheetBudget>, IBalanceSheetBudgetRepository
    {

        protected override BalanceSheetBudget AddEntity(BasicContext entityContext, BalanceSheetBudget entity)
        {
            return entityContext.Set<BalanceSheetBudget>().Add(entity);
        }

        protected override BalanceSheetBudget UpdateEntity(BasicContext entityContext, BalanceSheetBudget entity)
        {
            return (from e in entityContext.Set<BalanceSheetBudget>()
                    where e.BudgetId == entity.BudgetId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<BalanceSheetBudget> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<BalanceSheetBudget>()
                   select e;
        }

        protected override BalanceSheetBudget GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<BalanceSheetBudget>()
                         where e.BudgetId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<BalanceSheetBudget> GetBalanceSheetBudgets(string year)
        {
            using (BasicContext entityContext = new BasicContext())
            {
                var query = from a in entityContext.BalanceSheetBudgetSet
                            where a.Year == year
                            select a;

                return query.ToFullyLoaded();
            }
        }
      
    }
}
