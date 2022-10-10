using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Budget.Entities;
using Fintrak.Data.Budget.Contracts;

namespace Fintrak.Data.Budget
{
    [Export(typeof(IFeeCalculationTypeRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FeeCalculationTypeRepository : DataRepositoryBase<FeeCalculationType>, IFeeCalculationTypeRepository
    {

        protected override FeeCalculationType AddEntity(BudgetContext entityContext, FeeCalculationType entity)
        {
            return entityContext.Set<FeeCalculationType>().Add(entity);
        }

        protected override FeeCalculationType UpdateEntity(BudgetContext entityContext, FeeCalculationType entity)
        {
            return (from e in entityContext.Set<FeeCalculationType>() 
                    where e.FeeCalculationTypeId == entity.FeeCalculationTypeId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<FeeCalculationType> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<FeeCalculationType>()
                   select e;
        }

        protected override FeeCalculationType GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<FeeCalculationType>()
                         where e.FeeCalculationTypeId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

      
    }
}
