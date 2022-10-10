using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(ITrialBalanceRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TrialBalanceRepository : DataRepositoryBase<TrialBalance>, ITrialBalanceRepository
    {

        protected override TrialBalance AddEntity(BasicContext entityContext, TrialBalance entity)
        {
            return entityContext.Set<TrialBalance>().Add(entity);
        }

        protected override TrialBalance UpdateEntity(BasicContext entityContext, TrialBalance entity)
        {
            return (from e in entityContext.Set<TrialBalance>() 
                    where e.TrialBalanceId == entity.TrialBalanceId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<TrialBalance> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<TrialBalance>()
                   select e;
        }

        protected override TrialBalance GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<TrialBalance>()
                         where e.TrialBalanceId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<TrialBalance> GetTrialBalances(DateTime runDate)
        {
            using (BasicContext entityContext = new BasicContext())
            {
                var query = from a in entityContext.TrialBalanceSet
                            where a.TransDate == runDate
                            select a;

                return query.ToFullyLoaded();
            }
        }
      
    }
}
