using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;
using Fintrak.Shared.MPR.Framework;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IExpenseProductMappingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ExpenseProductMappingRepository : DataRepositoryBase<ExpenseProductMapping>, IExpenseProductMappingRepository
    {

        protected override ExpenseProductMapping AddEntity(MPRContext entityContext, ExpenseProductMapping entity)
        {
            return entityContext.Set<ExpenseProductMapping>().Add(entity);
        }

        protected override ExpenseProductMapping UpdateEntity(MPRContext entityContext, ExpenseProductMapping entity)
        {
            return (from e in entityContext.Set<ExpenseProductMapping>() 
                    where e.ExpenseProductId == entity.ExpenseProductId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ExpenseProductMapping> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<ExpenseProductMapping>()
                   select e;
        }

        protected override ExpenseProductMapping GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ExpenseProductMapping>()
                         where e.ExpenseProductId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ExpenseProductMappingInfo> GetExpenseProductMappings()
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.ExpenseProductMappingSet
                            join b in entityContext.ProductSet on a.ProductCode equals b.Code
                            join c in entityContext.ExpenseBasisSet on a.BasisCode equals c.Code
                            select new ExpenseProductMappingInfo()
                            {
                                ExpenseProductMapping = a,
                                Product = b,
                                ExpenseBasis = c
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
