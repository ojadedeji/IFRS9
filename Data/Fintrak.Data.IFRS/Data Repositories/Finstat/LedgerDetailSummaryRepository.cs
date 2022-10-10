using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ILedgerDetailSummaryRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LedgerDetailSummaryRepository : DataRepositoryBase<LedgerDetailSummary>, ILedgerDetailSummaryRepository
    {

        protected override LedgerDetailSummary AddEntity(IFRSContext entityContext, LedgerDetailSummary entity)
        {
            return entityContext.Set<LedgerDetailSummary>().Add(entity);
        }

        protected override LedgerDetailSummary UpdateEntity(IFRSContext entityContext, LedgerDetailSummary entity)
        {
            return (from e in entityContext.Set<LedgerDetailSummary>()
                    where e.SummaryId == entity.SummaryId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<LedgerDetailSummary> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<LedgerDetailSummary>()
                   select e;
        }

        protected override LedgerDetailSummary GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<LedgerDetailSummary>()
                         where e.SummaryId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
