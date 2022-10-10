using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IIntegralFeeRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IntegralFeeRepository : DataRepositoryBase<IntegralFee>, IIntegralFeeRepository
    {
        protected override IntegralFee AddEntity(IFRSContext entityContext, IntegralFee entity)
        {
            return entityContext.Set<IntegralFee>().Add(entity);
        }

        protected override IntegralFee UpdateEntity(IFRSContext entityContext, IntegralFee entity)
        {
            return (from e in entityContext.Set<IntegralFee>()
                    where e.IntegralFeeId == entity.IntegralFeeId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IntegralFee> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<IntegralFee>()
                   select e;
        }

        protected override IntegralFee GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IntegralFee>()
                         where e.IntegralFeeId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}