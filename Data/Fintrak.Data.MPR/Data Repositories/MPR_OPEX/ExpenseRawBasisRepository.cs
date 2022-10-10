using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IExpenseRawBasisRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ExpenseRawBasisRepository : DataRepositoryBase<ExpenseRawBasis>, IExpenseRawBasisRepository
    {

        protected override ExpenseRawBasis AddEntity(MPRContext entityContext, ExpenseRawBasis entity)
        {
            return entityContext.Set<ExpenseRawBasis>().Add(entity);
        }

        protected override ExpenseRawBasis UpdateEntity(MPRContext entityContext, ExpenseRawBasis entity)
        {
            return (from e in entityContext.Set<ExpenseRawBasis>()
                    where e.ExpenseRawBasisId == entity.ExpenseRawBasisId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ExpenseRawBasis> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<ExpenseRawBasis>()
                   select e;
        }

        protected override ExpenseRawBasis GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ExpenseRawBasis>()
                         where e.ExpenseRawBasisId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ExpenseRawBasisInfo> GetExpenseRawBasis()
        {
            using (MPRContext entityContext = new MPRContext())
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
