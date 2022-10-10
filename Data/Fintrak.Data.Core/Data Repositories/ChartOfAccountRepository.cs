using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Data.Core.Contracts;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Core.Entities;

namespace Fintrak.Data.Core
{
    [Export(typeof(IChartOfAccountRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ChartOfAccountRepository : DataRepositoryBase<ChartOfAccount>, IChartOfAccountRepository
    {
        protected override ChartOfAccount AddEntity(CoreContext entityContext, ChartOfAccount entity)
        {
            return entityContext.Set<ChartOfAccount>().Add(entity);
        }

        protected override ChartOfAccount UpdateEntity(CoreContext entityContext, ChartOfAccount entity)
        {
            return (from e in entityContext.Set<ChartOfAccount>()
                    where e.ChartOfAccountId == entity.ChartOfAccountId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ChartOfAccount> GetEntities(CoreContext entityContext)
        {
            return from e in entityContext.Set<ChartOfAccount>()
                   select e;
        }

        protected override ChartOfAccount GetEntity(CoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ChartOfAccount>()
                         where e.ChartOfAccountId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

       public IEnumerable<ChartOfAccountInfo> GetChartOfAccountInfo()
        {
            using (CoreContext entityContext = new CoreContext())
            {
                var query = from a in entityContext.ChartOfAccountSet
                            join b in entityContext.FinancialTypeSet on a.FinancialTypeId equals b.FinancialTypeId
                            join c in entityContext.ChartOfAccountSet on a.ParentId equals c.ChartOfAccountId into parents
                            from pt in parents.DefaultIfEmpty()
                            select new ChartOfAccountInfo()
                            {
                                ChartOfAccount = a,
                                FinancialType = b,
                                Parent = pt
                            };

                return query.ToFullyLoaded();
            }
        }
    }
}
