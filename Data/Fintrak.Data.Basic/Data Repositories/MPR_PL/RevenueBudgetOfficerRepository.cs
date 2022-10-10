using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IRevenueBudgetOfficerRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RevenueBudgetOfficerRepository : DataRepositoryBase<RevenueBudgetOfficer>, IRevenueBudgetOfficerRepository
    {

        protected override RevenueBudgetOfficer AddEntity(BasicContext entityContext, RevenueBudgetOfficer entity)
        {
            return entityContext.Set<RevenueBudgetOfficer>().Add(entity);
        }

        protected override RevenueBudgetOfficer UpdateEntity(BasicContext entityContext, RevenueBudgetOfficer entity)
        {
            return (from e in entityContext.Set<RevenueBudgetOfficer>()
                    where e.BudgetId == entity.BudgetId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<RevenueBudgetOfficer> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<RevenueBudgetOfficer>()
                   select e;
        }

        protected override RevenueBudgetOfficer GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<RevenueBudgetOfficer>()
                         where e.BudgetId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<RevenueBudgetOfficer> GetRevenueBudgetOfficers(string year)
        {
            using (BasicContext entityContext = new BasicContext())
            {
                var query = from a in entityContext.RevenueBudgetOfficerSet
                            where a.Year == year
                            select a;

                return query.ToFullyLoaded();
            }
        }


      
    }
}
