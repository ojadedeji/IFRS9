using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(ITrialBalanceGapRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TrialBalanceGapRepository : DataRepositoryBase<TrialBalanceGap>, ITrialBalanceGapRepository
    {

        protected override TrialBalanceGap AddEntity(BasicContext entityContext, TrialBalanceGap entity)
        {
            return entityContext.Set<TrialBalanceGap>().Add(entity);
        }

        protected override TrialBalanceGap UpdateEntity(BasicContext entityContext, TrialBalanceGap entity)
        {
            return (from e in entityContext.Set<TrialBalanceGap>() 
                    where e.TrialBalanceGAPId == entity.TrialBalanceGAPId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<TrialBalanceGap> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<TrialBalanceGap>()
                   select e;
        }

        protected override TrialBalanceGap GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<TrialBalanceGap>()
                         where e.TrialBalanceGAPId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<TrialBalanceGap> GetTrialBalances(DateTime runDate )
        {
            using (BasicContext entityContext = new BasicContext())
            {
                var query = from a in entityContext.TrialBalanceGapSet
                            where a.TransDate == runDate                       
                            select a;

                return query.ToFullyLoaded();
            }
        }
      
    }
}
