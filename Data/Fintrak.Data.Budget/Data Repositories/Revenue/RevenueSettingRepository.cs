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
    [Export(typeof(IRevenueSettingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RevenueSettingRepository : DataRepositoryBase<RevenueSetting>, IRevenueSettingRepository
    {

        protected override RevenueSetting AddEntity(BudgetContext entityContext, RevenueSetting entity)
        {
            return entityContext.Set<RevenueSetting>().Add(entity);
        }

        protected override RevenueSetting UpdateEntity(BudgetContext entityContext, RevenueSetting entity)
        {
            return (from e in entityContext.Set<RevenueSetting>() 
                    where e.RevenueSettingId == entity.RevenueSettingId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<RevenueSetting> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<RevenueSetting>()
                   select e;
        }

        protected override RevenueSetting GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<RevenueSetting>()
                         where e.RevenueSettingId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<RevenueSetting> GetRevenueSettings(string year, string reviewCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.RevenueSettingSet
                            where a.Year == year && a.ReviewCode == reviewCode
                                select a;

                return query.ToFullyLoaded();
            }
        }
      
    }
}
