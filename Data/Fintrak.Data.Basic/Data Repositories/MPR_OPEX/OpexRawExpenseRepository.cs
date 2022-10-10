using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IOpexRawExpenseRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OpexRawExpenseRepository : DataRepositoryBase<OpexRawExpense>, IOpexRawExpenseRepository
    {

        protected override OpexRawExpense AddEntity(BasicContext entityContext, OpexRawExpense entity)
        {
            return entityContext.Set<OpexRawExpense>().Add(entity);
        }

        protected override OpexRawExpense UpdateEntity(BasicContext entityContext, OpexRawExpense entity)
        {
            return (from e in entityContext.Set<OpexRawExpense>()
                    where e.OpexRawExpenseId == entity.OpexRawExpenseId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<OpexRawExpense> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<OpexRawExpense>()
                   select e;
        }

        protected override OpexRawExpense GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<OpexRawExpense>()
                         where e.OpexRawExpenseId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
