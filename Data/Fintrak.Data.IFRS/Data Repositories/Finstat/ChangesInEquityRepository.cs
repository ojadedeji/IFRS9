using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IChangesInEquityRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ChangesInEquityRepository : DataRepositoryBase<ChangesInEquity>, IChangesInEquityRepository
    {

        protected override ChangesInEquity AddEntity(IFRSContext entityContext, ChangesInEquity entity)
        {
            return entityContext.Set<ChangesInEquity>().Add(entity);
        }

        protected override ChangesInEquity UpdateEntity(IFRSContext entityContext, ChangesInEquity entity)
        {
            return (from e in entityContext.Set<ChangesInEquity>() 
                    where e.ChangesInEquityId == entity.ChangesInEquityId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ChangesInEquity> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<ChangesInEquity>()
                   select e;
        }

        protected override ChangesInEquity GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ChangesInEquity>()
                         where e.ChangesInEquityId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}
