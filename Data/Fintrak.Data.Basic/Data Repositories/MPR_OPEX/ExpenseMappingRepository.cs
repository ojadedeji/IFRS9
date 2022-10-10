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
    [Export(typeof(IExpenseMappingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ExpenseMappingRepository : DataRepositoryBase<ExpenseMapping>, IExpenseMappingRepository
    {

        protected override ExpenseMapping AddEntity(BasicContext entityContext, ExpenseMapping entity)
        {
            return entityContext.Set<ExpenseMapping>().Add(entity);
        }

        protected override ExpenseMapping UpdateEntity(BasicContext entityContext, ExpenseMapping entity)
        {
            return (from e in entityContext.Set<ExpenseMapping>()
                    where e.ExpenseMappingId == entity.ExpenseMappingId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ExpenseMapping> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<ExpenseMapping>()
                   select e;
        }

        protected override ExpenseMapping GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ExpenseMapping>()
                         where e.ExpenseMappingId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ExpenseMappingInfo> GetExpenseMappings()
        {
            using (BasicContext entityContext = new BasicContext())
            {
                var query = from a in entityContext.ExpenseMappingSet
                            join c in entityContext.ExpenseBasisSet on a.BasisCode equals c.Code
                            join d in entityContext.TeamSet on a.MISCode equals d.Code
                            select new ExpenseMappingInfo()
                            {
                                ExpenseMapping = a,
                                ExpenseBasis = c,
                                Team  = d
                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<ExpenseMappingInfo> GetExpenseMappings(string Year)
        {
            using (BasicContext entityContext = new BasicContext())
            {
                var query = from a in entityContext.ExpenseMappingSet
                            join c in entityContext.ExpenseBasisSet on a.BasisCode equals c.Code
                            join d in entityContext.TeamSet on a.MISCode equals d.Code
                            where d.Year == Year
                            select new ExpenseMappingInfo()
                            {
                                ExpenseMapping = a,
                                ExpenseBasis = c,
                                Team = d
                            };

                return query.ToFullyLoaded();
            }
        }
    }
}
