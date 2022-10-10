using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IIfrsInvestmentRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsInvestmentRepository : DataRepositoryBase<IfrsInvestment>, IIfrsInvestmentRepository
    {
        protected override IfrsInvestment AddEntity(IFRSContext entityContext, IfrsInvestment entity)
        {
            return entityContext.Set<IfrsInvestment>().Add(entity);
        }

        protected override IfrsInvestment UpdateEntity(IFRSContext entityContext, IfrsInvestment entity)
        {
            return (from e in entityContext.Set<IfrsInvestment>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IfrsInvestment> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<IfrsInvestment>()
                   select e;
        }

        protected override IfrsInvestment GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IfrsInvestment>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}
