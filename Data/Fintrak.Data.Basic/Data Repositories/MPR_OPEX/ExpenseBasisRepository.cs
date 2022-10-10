using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IExpenseBasisRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ExpenseBasisRepository : DataRepositoryBase<ExpenseBasis>, IExpenseBasisRepository
    {

        protected override ExpenseBasis AddEntity(BasicContext entityContext, ExpenseBasis entity)
        {
            return entityContext.Set<ExpenseBasis>().Add(entity);
        }

        protected override ExpenseBasis UpdateEntity(BasicContext entityContext, ExpenseBasis entity)
        {
            return (from e in entityContext.Set<ExpenseBasis>() 
                    where e.ExpenseBasisId == entity.ExpenseBasisId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ExpenseBasis> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<ExpenseBasis>()
                   select e;
        }

        protected override ExpenseBasis GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ExpenseBasis>()
                         where e.ExpenseBasisId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
