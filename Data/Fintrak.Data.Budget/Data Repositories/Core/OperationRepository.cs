using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Budget.Entities;
using Fintrak.Data.Budget.Contracts;

namespace Fintrak.Data.Budget
{
    [Export(typeof(IOperationRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OperationRepository : DataRepositoryBase<Operation>, IOperationRepository
    {

        protected override Operation AddEntity(BudgetContext entityContext, Operation entity)
        {
            return entityContext.Set<Operation>().Add(entity);
        }

        protected override Operation UpdateEntity(BudgetContext entityContext, Operation entity)
        {
            return (from e in entityContext.Set<Operation>() 
                    where e.OperationId == entity.OperationId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Operation> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<Operation>()
                   select e;
        }

        protected override Operation GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<Operation>()
                         where e.OperationId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

      
    }
}
