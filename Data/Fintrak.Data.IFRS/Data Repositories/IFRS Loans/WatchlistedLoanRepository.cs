using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IWatchListedLoanRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class WatchListedLoanRepository : DataRepositoryBase<WatchListedLoan>, IWatchListedLoanRepository
    {

        protected override WatchListedLoan AddEntity(IFRSContext entityContext, WatchListedLoan entity)
        {
            return entityContext.Set<WatchListedLoan>().Add(entity);
        }

        protected override WatchListedLoan UpdateEntity(IFRSContext entityContext, WatchListedLoan entity)
        {
            return (from e in entityContext.Set<WatchListedLoan>() 
                    where e.WatchListedLoanId == entity.WatchListedLoanId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<WatchListedLoan> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<WatchListedLoan>()
                   select e;
        }

        protected override WatchListedLoan GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<WatchListedLoan>()
                         where e.WatchListedLoanId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
 
    }
}
