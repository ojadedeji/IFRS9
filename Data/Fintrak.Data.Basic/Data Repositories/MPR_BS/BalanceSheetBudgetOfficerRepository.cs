using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IBalanceSheetBudgetOfficerRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BalanceSheetBudgetOfficerRepository : DataRepositoryBase<BalanceSheetBudgetOfficer>, IBalanceSheetBudgetOfficerRepository
    {

        protected override BalanceSheetBudgetOfficer AddEntity(BasicContext entityContext, BalanceSheetBudgetOfficer entity)
        {
            return entityContext.Set<BalanceSheetBudgetOfficer>().Add(entity);
        }

        protected override BalanceSheetBudgetOfficer UpdateEntity(BasicContext entityContext, BalanceSheetBudgetOfficer entity)
        {
            return (from e in entityContext.Set<BalanceSheetBudgetOfficer>()
                    where e.BudgetId == entity.BudgetId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<BalanceSheetBudgetOfficer> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<BalanceSheetBudgetOfficer>()
                   select e;
        }

        protected override BalanceSheetBudgetOfficer GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<BalanceSheetBudgetOfficer>()
                         where e.BudgetId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }


        public IEnumerable<BalanceSheetBudgetOfficer> GetBalanceSheetBudgetOfficers(string year)
        {
            using (BasicContext entityContext = new BasicContext())
            {
                var query = from a in entityContext.BalanceSheetBudgetOfficerSet
                            where a.Year == year
                            select a;

                return query.ToFullyLoaded();
            }
        }


    }
}
