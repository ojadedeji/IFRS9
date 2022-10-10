using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IAssumptionRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AssumptionRepository : DataRepositoryBase<Assumption>, IAssumptionRepository
    {
        protected override Assumption AddEntity(IFRSContext entityContext, Assumption entity)
        {
            return entityContext.Set<Assumption>().Add(entity);
        }

        protected override Assumption UpdateEntity(IFRSContext entityContext, Assumption entity)
        {
            return (from e in entityContext.Set<Assumption>()
                    where e.InstrumentID == entity.InstrumentID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Assumption> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<Assumption>()
                   select e;
        }

        protected override Assumption GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<Assumption>()
                         where e.InstrumentID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}