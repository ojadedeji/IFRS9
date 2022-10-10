using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IOpexBusinessRuleRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OpexBusinessRuleRepository : DataRepositoryBase<OpexBusinessRule>, IOpexBusinessRuleRepository
    {

        protected override OpexBusinessRule AddEntity(BasicContext entityContext, OpexBusinessRule entity)
        {
            return entityContext.Set<OpexBusinessRule>().Add(entity);
        }

        protected override OpexBusinessRule UpdateEntity(BasicContext entityContext, OpexBusinessRule entity)
        {
            return (from e in entityContext.Set<OpexBusinessRule>()
                    where e.OpexBusinessRuleId == entity.OpexBusinessRuleId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<OpexBusinessRule> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<OpexBusinessRule>()
                   select e;
        }

        protected override OpexBusinessRule GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<OpexBusinessRule>()
                         where e.OpexBusinessRuleId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
