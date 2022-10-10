using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IUnquotedEquityFairvalueResultRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UnquotedEquityFairvalueResultRepository : DataRepositoryBase<UnquotedEquityFairvalueResult>, IUnquotedEquityFairvalueResultRepository
    {
        protected override UnquotedEquityFairvalueResult AddEntity(IFRSContext entityContext, UnquotedEquityFairvalueResult entity)
        {
            return entityContext.Set<UnquotedEquityFairvalueResult>().Add(entity);
        }

        protected override UnquotedEquityFairvalueResult UpdateEntity(IFRSContext entityContext, UnquotedEquityFairvalueResult entity)
        {
            return (from e in entityContext.Set<UnquotedEquityFairvalueResult>()
                    where e.UnquotedEquityFairvalueResultId == entity.UnquotedEquityFairvalueResultId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<UnquotedEquityFairvalueResult> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<UnquotedEquityFairvalueResult>()
                   select e;
        }

        protected override UnquotedEquityFairvalueResult GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<UnquotedEquityFairvalueResult>()
                         where e.UnquotedEquityFairvalueResultId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}