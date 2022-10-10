using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IExpenseRawBasisRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ExpenseRawBasisRepository : DataRepositoryBase<ExpenseRawBasis>, IExpenseRawBasisRepository
    {

        protected override ExpenseRawBasis AddEntity(BasicContext entityContext, ExpenseRawBasis entity)
        {
            return entityContext.Set<ExpenseRawBasis>().Add(entity);
        }

        protected override ExpenseRawBasis UpdateEntity(BasicContext entityContext, ExpenseRawBasis entity)
        {
            return (from e in entityContext.Set<ExpenseRawBasis>()
                    where e.ExpenseRawBasisId == entity.ExpenseRawBasisId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ExpenseRawBasis> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<ExpenseRawBasis>()
                   select e;
        }

        protected override ExpenseRawBasis GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ExpenseRawBasis>()
                         where e.ExpenseRawBasisId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ExpenseRawBasisInfo> GetExpenseRawBasis()
        {
            using (BasicContext entityContext = new BasicContext())
            {
                var query = from a in entityContext.ExpenseRawBasisSet
                            join b in entityContext.CostCentreSet on a.MISCode equals b.Code
                            join c in entityContext.ExpenseBasisSet on a.BasisCode equals c.Code
                            select new ExpenseRawBasisInfo()
                            {
                                ExpenseRawBasis = a,
                                CostCentre = b,
                                ExpenseBasis = c
                            };
                return query.ToFullyLoaded();
            }
        }
    }
}
