using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Data.Core.Contracts;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Core.Entities;

namespace Fintrak.Data.Core
{
    [Export(typeof(IFiscalPeriodRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FiscalPeriodRepository : DataRepositoryBase<FiscalPeriod>, IFiscalPeriodRepository
    {
        protected override FiscalPeriod AddEntity(CoreContext entityContext, FiscalPeriod entity)
        {
            return entityContext.Set<FiscalPeriod>().Add(entity);
        }

        protected override FiscalPeriod UpdateEntity(CoreContext entityContext, FiscalPeriod entity)
        {
            return (from e in entityContext.Set<FiscalPeriod>()
                    where e.FiscalPeriodId == entity.FiscalPeriodId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<FiscalPeriod> GetEntities(CoreContext entityContext)
        {
            return from e in entityContext.Set<FiscalPeriod>()
                   select e;
        }

        protected override FiscalPeriod GetEntity(CoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<FiscalPeriod>()
                         where e.FiscalPeriodId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public FiscalPeriodInfo GetOpenFiscalPeriodInfo()
        {
            using (CoreContext entityContext = new CoreContext())
            {
                var query = from a in entityContext.FiscalPeriodSet
                            join b in entityContext.FiscalYearSet on a.FiscalYearId equals b.FiscalYearId
                            where a.Closed == false
                            select new FiscalPeriodInfo()
                            {
                                FiscalPeriod = a,
                                FiscalYear = b
                            };

                return query.FirstOrDefault();
            }
        }

        public IEnumerable<FiscalPeriodInfo> GetFiscalPeriodInfo(int fiscalYearId)
        {
            using (CoreContext entityContext = new CoreContext())
            {
                var query = from a in entityContext.FiscalPeriodSet
                            join b in entityContext.FiscalYearSet on a.FiscalYearId equals b.FiscalYearId
                            where b.FiscalYearId == fiscalYearId
                            select new FiscalPeriodInfo()
                            {
                                FiscalPeriod = a,
                                FiscalYear = b
                            };

                return query.ToFullyLoaded();
            }
        }
    }
}
