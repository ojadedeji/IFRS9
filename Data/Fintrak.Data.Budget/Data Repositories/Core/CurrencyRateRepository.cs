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
    [Export(typeof(ICurrencyRateRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CurrencyRateRepository : DataRepositoryBase<CurrencyRate>, ICurrencyRateRepository
    {

        protected override CurrencyRate AddEntity(BudgetContext entityContext, CurrencyRate entity)
        {
            return entityContext.Set<CurrencyRate>().Add(entity);
        }

        protected override CurrencyRate UpdateEntity(BudgetContext entityContext, CurrencyRate entity)
        {
            return (from e in entityContext.Set<CurrencyRate>() 
                    where e.CurrencyRateId == entity.CurrencyRateId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CurrencyRate> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<CurrencyRate>()
                   select e;
        }

        protected override CurrencyRate GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CurrencyRate>()
                         where e.CurrencyRateId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

         public CurrencyRate GetCurrencyRate(string year, string reviewCode, RateTypeEnum rateType,string currencyCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.CurrencyRateSet
                            where a.Year == year && a.ReviewCode == reviewCode && a.RateType == rateType && a.CurrencyCode == currencyCode  
                                select a;

                return query.FirstOrDefault();
            }
        }

        public IEnumerable<CurrencyRate> GetCurrencyRates(string year, string reviewCode, RateTypeEnum rateType)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.CurrencyRateSet
                            where a.Year == year && a.ReviewCode == reviewCode && a.RateType == rateType 
                                select a;

                return query.ToFullyLoaded();
            }
        }
      
    }
}
