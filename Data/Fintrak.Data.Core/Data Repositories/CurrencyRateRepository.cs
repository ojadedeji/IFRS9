using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Data.Core.Contracts;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Core.Entities;

namespace Fintrak.Data.Core
{
    [Export(typeof(ICurrencyRateRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CurrencyRateRepository : DataRepositoryBase<CurrencyRate>, ICurrencyRateRepository
    {
        protected override CurrencyRate AddEntity(CoreContext entityContext, CurrencyRate entity)
        {
            return entityContext.Set<CurrencyRate>().Add(entity);
        }

        protected override CurrencyRate UpdateEntity(CoreContext entityContext, CurrencyRate entity)
        {
            return (from e in entityContext.Set<CurrencyRate>()
                    where e.CurrencyRateId == entity.CurrencyRateId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CurrencyRate> GetEntities(CoreContext entityContext)
        {
            return from e in entityContext.Set<CurrencyRate>()
                   select e;
        }

        protected override CurrencyRate GetEntity(CoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CurrencyRate>()
                         where e.CurrencyRateId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
        public IEnumerable<CurrencyRateInfo> GetCurrencyRateInfo()
        {
            using (CoreContext entityContext = new CoreContext())
            {
                var query = from a in entityContext.CurrencyRateSet
                            join b in entityContext.CurrencySet on a.CurrencyId equals b.CurrencyId
                            join c in entityContext.RateTypeSet on a.RateTypeId equals c.RateTypeId
                            select new CurrencyRateInfo()
                            {
                                CurrencyRate = a,
                                Currency = b,
                                RateType = c
                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<CurrencyRateInfo> GetCurrencyRateByDate(DateTime runDate, string curSymbol)
        {
            using (CoreContext entityContext = new CoreContext())
            {
                var query = from a in entityContext.CurrencyRateSet
                            join b in entityContext.CurrencySet on a.CurrencyId equals b.CurrencyId
                            //join c in entityContext.RateTypeSet on a.RateTypeId equals c.RateTypeId
                            where a.Date == runDate && b.Symbol == curSymbol && a.RateTypeId == 1
                            select new CurrencyRateInfo()
                            {
                                CurrencyRate = a,
                                Currency = b
                                //RateType = c
                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<CurrencyRateInfo> GetCurrencyRateByCurrencyId(int currencyId)
        {
            using (CoreContext entityContext = new CoreContext())
            {
                var query = from a in entityContext.CurrencyRateSet
                            join b in entityContext.CurrencySet on a.CurrencyId equals b.CurrencyId
                            join c in entityContext.RateTypeSet on a.RateTypeId equals c.RateTypeId
                            where a.CurrencyId == currencyId 
                            select new CurrencyRateInfo()
                            {
                                CurrencyRate = a,
                                Currency = b,
                                RateType = c
                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<CurrencyRateInfo> GetCurrencyRateByRateTypeId(int currencyId,int rateTypeId)
        {
            using (CoreContext entityContext = new CoreContext())
            {
                var query = from a in entityContext.CurrencyRateSet
                            join b in entityContext.CurrencySet on a.CurrencyId equals b.CurrencyId
                            join c in entityContext.RateTypeSet on a.RateTypeId equals c.RateTypeId
                            where a.RateTypeId == rateTypeId && a.CurrencyId == currencyId
                            select new CurrencyRateInfo()
                            {
                                CurrencyRate = a,
                                Currency = b,
                                RateType = c
                            };

                return query.ToFullyLoaded();
            }
        }
    }
}
