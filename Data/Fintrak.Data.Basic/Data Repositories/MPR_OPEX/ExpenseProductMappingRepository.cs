using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;
using Fintrak.Shared.Basic.Framework;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IExpenseProductMappingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ExpenseProductMappingRepository : DataRepositoryBase<ExpenseProductMapping>, IExpenseProductMappingRepository
    {

        protected override ExpenseProductMapping AddEntity(BasicContext entityContext, ExpenseProductMapping entity)
        {
            return entityContext.Set<ExpenseProductMapping>().Add(entity);
        }

        protected override ExpenseProductMapping UpdateEntity(BasicContext entityContext, ExpenseProductMapping entity)
        {
            return (from e in entityContext.Set<ExpenseProductMapping>() 
                    where e.ExpenseProductId == entity.ExpenseProductId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ExpenseProductMapping> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<ExpenseProductMapping>()
                   select e;
        }

        protected override ExpenseProductMapping GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ExpenseProductMapping>()
                         where e.ExpenseProductId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ExpenseProductMappingInfo> GetExpenseProductMappings()
        {
            using (BasicContext entityContext = new BasicContext())
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
