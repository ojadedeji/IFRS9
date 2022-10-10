using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Data.Core.Contracts;
using Fintrak.Shared.Core.Entities;

namespace Fintrak.Data.Core
{
    [Export(typeof(IFiscalYearRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FiscalYearRepository : DataRepositoryBase<FiscalYear>, IFiscalYearRepository
    {
        protected override FiscalYear AddEntity(CoreContext entityContext, FiscalYear entity)
        {
            return entityContext.Set<FiscalYear>().Add(entity);
        }

        protected override FiscalYear UpdateEntity(CoreContext entityContext, FiscalYear entity)
        {
            return (from e in entityContext.Set<FiscalYear>()
                    where e.FiscalYearId == entity.FiscalYearId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<FiscalYear> GetEntities(CoreContext entityContext)
        {
            return from e in entityContext.Set<FiscalYear>().OrderByDescending(c=>c.StartDate)
                   select e;
        }

        protected override FiscalYear GetEntity(CoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<FiscalYear>()
                         where e.FiscalYearId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public FiscalYear GetOpenFiscalYear()
        {
            using (CoreContext entityContext = new CoreContext())
            {
                var query = from a in entityContext.FiscalYearSet
                            where a.Closed == false
                                select a;

                return query.FirstOrDefault();
            }
        }
    }
}
