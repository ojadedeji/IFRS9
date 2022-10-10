using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IHistoricalDefaultedAccountsRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class HistoricalDefaultedAccountsRepository : DataRepositoryBase<HistoricalDefaultedAccounts>, IHistoricalDefaultedAccountsRepository
    {
        protected override HistoricalDefaultedAccounts AddEntity(IFRSContext entityContext, HistoricalDefaultedAccounts entity)
        {
            return entityContext.Set<HistoricalDefaultedAccounts>().Add(entity);
        }

        protected override HistoricalDefaultedAccounts UpdateEntity(IFRSContext entityContext, HistoricalDefaultedAccounts entity)
        {
            return (from e in entityContext.Set<HistoricalDefaultedAccounts>()
                    where e.DefaultedAccountId == entity.DefaultedAccountId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<HistoricalDefaultedAccounts> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<HistoricalDefaultedAccounts>()
                   select e;
        }

        protected override HistoricalDefaultedAccounts GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<HistoricalDefaultedAccounts>()
                         where e.DefaultedAccountId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}