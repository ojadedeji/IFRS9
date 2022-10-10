using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IIfrsEquityUnqoutedRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsEquityUnqoutedRepository : DataRepositoryBase<IfrsEquityUnqouted>, IIfrsEquityUnqoutedRepository
    {
        protected override IfrsEquityUnqouted AddEntity(IFRSContext entityContext, IfrsEquityUnqouted entity)
        {
            return entityContext.Set<IfrsEquityUnqouted>().Add(entity);
        }

        protected override IfrsEquityUnqouted UpdateEntity(IFRSContext entityContext, IfrsEquityUnqouted entity)
        {
            return (from e in entityContext.Set<IfrsEquityUnqouted>()
                    where e.IfrsEquityUnqoutedId == entity.IfrsEquityUnqoutedId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IfrsEquityUnqouted> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<IfrsEquityUnqouted>()
                   select e;
        }

        protected override IfrsEquityUnqouted GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IfrsEquityUnqouted>()
                         where e.IfrsEquityUnqoutedId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}