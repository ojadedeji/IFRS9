using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ITrialBalanceGapRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TrialBalanceGapRepository : DataRepositoryBase<TrialBalanceGap>, ITrialBalanceGapRepository
    {

        protected override TrialBalanceGap AddEntity(IFRSContext entityContext, TrialBalanceGap entity)
        {
            return entityContext.Set<TrialBalanceGap>().Add(entity);
        }

        protected override TrialBalanceGap UpdateEntity(IFRSContext entityContext, TrialBalanceGap entity)
        {
            return (from e in entityContext.Set<TrialBalanceGap>() 
                    where e.TrialBalanceGAPId == entity.TrialBalanceGAPId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<TrialBalanceGap> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<TrialBalanceGap>()
                   select e;
        }

        protected override TrialBalanceGap GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<TrialBalanceGap>()
                         where e.TrialBalanceGAPId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<TrialBalanceGap> GetTrialBalances(DateTime runDate )
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.TrialBalanceGapSet
                            where a.TransDate == runDate                       
                            select a;

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<TrialBalanceGap> GetGapTrialBalancesByBranch(DateTime runDate, string branchCode)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.TrialBalanceGapSet
                            where a.TransDate == runDate && a.BranchCode == branchCode                      
                            select a;

                return query.ToFullyLoaded();
            }
        }
      
    }
}
