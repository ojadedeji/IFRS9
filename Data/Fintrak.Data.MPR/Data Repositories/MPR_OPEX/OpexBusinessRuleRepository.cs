using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IOpexBusinessRuleRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OpexBusinessRuleRepository : DataRepositoryBase<OpexBusinessRule>, IOpexBusinessRuleRepository
    {

        protected override OpexBusinessRule AddEntity(MPRContext entityContext, OpexBusinessRule entity)
        {
            return entityContext.Set<OpexBusinessRule>().Add(entity);
        }

        protected override OpexBusinessRule UpdateEntity(MPRContext entityContext, OpexBusinessRule entity)
        {
            return (from e in entityContext.Set<OpexBusinessRule>()
                    where e.OpexBusinessRuleId == entity.OpexBusinessRuleId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<OpexBusinessRule> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<OpexBusinessRule>()
                   select e;
        }

        protected override OpexBusinessRule GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<OpexBusinessRule>()
                         where e.OpexBusinessRuleId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
