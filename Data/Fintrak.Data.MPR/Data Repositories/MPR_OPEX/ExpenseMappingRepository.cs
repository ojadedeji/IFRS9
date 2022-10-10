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
    [Export(typeof(IExpenseMappingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ExpenseMappingRepository : DataRepositoryBase<ExpenseMapping>, IExpenseMappingRepository
    {

        protected override ExpenseMapping AddEntity(MPRContext entityContext, ExpenseMapping entity)
        {
            return entityContext.Set<ExpenseMapping>().Add(entity);
        }

        protected override ExpenseMapping UpdateEntity(MPRContext entityContext, ExpenseMapping entity)
        {
            return (from e in entityContext.Set<ExpenseMapping>()
                    where e.ExpenseMappingId == entity.ExpenseMappingId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ExpenseMapping> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<ExpenseMapping>()
                   select e;
        }

        protected override ExpenseMapping GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ExpenseMapping>()
                         where e.ExpenseMappingId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ExpenseMappingInfo> GetExpenseMappings()
        {
            using (MPRContext entityContext = new MPRContext())
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

        public IEnumerable<ExpenseMappingInfo> GetExpenseMappings(string year)
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.ExpenseMappingSet
                            join c in entityContext.ExpenseBasisSet on a.BasisCode equals c.Code
                            join d in entityContext.TeamSet on a.MISCode equals d.Code into teams
                            from t in teams.Where(tm => (tm.Year == year)).DefaultIfEmpty()
                            where t.Year == year
                            select new ExpenseMappingInfo()
                            {
                                ExpenseMapping = a,
                                ExpenseBasis = c,
                                Team = t
                            };

                return query.ToFullyLoaded();
            }
        }
    }
}
