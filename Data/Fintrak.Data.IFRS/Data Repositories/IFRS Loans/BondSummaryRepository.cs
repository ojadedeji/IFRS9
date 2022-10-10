using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IBondSummaryRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BondSummaryRepository : DataRepositoryBase<BondSummary>, IBondSummaryRepository
    {

        protected override BondSummary AddEntity(IFRSContext entityContext, BondSummary entity)
        {
            return entityContext.Set<BondSummary>().Add(entity);
        }

        protected override BondSummary UpdateEntity(IFRSContext entityContext, BondSummary entity)
        {
            return (from e in entityContext.Set<BondSummary>() 
                    where e.BondId == entity.BondId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<BondSummary> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<BondSummary>()
                   select e;
        }

        protected override BondSummary GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<BondSummary>()
                         where e.BondId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
 
    }
}
