using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Budget.Entities;
using Fintrak.Data.Budget.Contracts;

namespace Fintrak.Data.Budget
{
    [Export(typeof(IFeeMovementRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FeeMovementRepository : DataRepositoryBase<FeeMovement>, IFeeMovementRepository
    {

        protected override FeeMovement AddEntity(BudgetContext entityContext, FeeMovement entity)
        {
            return entityContext.Set<FeeMovement>().Add(entity);
        }

        protected override FeeMovement UpdateEntity(BudgetContext entityContext, FeeMovement entity)
        {
            return (from e in entityContext.Set<FeeMovement>() 
                    where e.FeeMovementId == entity.FeeMovementId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<FeeMovement> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<FeeMovement>()
                   select e;
        }

        protected override FeeMovement GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<FeeMovement>()
                         where e.FeeMovementId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

      
    }
}
