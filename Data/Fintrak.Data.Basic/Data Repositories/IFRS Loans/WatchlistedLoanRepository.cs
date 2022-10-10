using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IWatchListedLoanRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class WatchListedLoanRepository : DataRepositoryBase<WatchListedLoan>, IWatchListedLoanRepository
    {

        protected override WatchListedLoan AddEntity(BasicContext entityContext, WatchListedLoan entity)
        {
            return entityContext.Set<WatchListedLoan>().Add(entity);
        }

        protected override WatchListedLoan UpdateEntity(BasicContext entityContext, WatchListedLoan entity)
        {
            return (from e in entityContext.Set<WatchListedLoan>() 
                    where e.WatchListedLoanId == entity.WatchListedLoanId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<WatchListedLoan> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<WatchListedLoan>()
                   select e;
        }

        protected override WatchListedLoan GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<WatchListedLoan>()
                         where e.WatchListedLoanId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
 
    }
}
