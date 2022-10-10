using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Budget.Entities;
using Fintrak.Data.Budget.Contracts;
using Fintrak.Shared.Budget.Framework.Enums;

namespace Fintrak.Data.Budget
{
    [Export(typeof(IGradeRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GradeRepository : DataRepositoryBase<Grade>, IGradeRepository
    {

        protected override Grade AddEntity(BudgetContext entityContext, Grade entity)
        {
            return entityContext.Set<Grade>().Add(entity);
        }

        protected override Grade UpdateEntity(BudgetContext entityContext, Grade entity)
        {
            return (from e in entityContext.Set<Grade>() 
                    where e.GradeId == entity.GradeId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Grade> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<Grade>()
                   select e;
        }

        protected override Grade GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<Grade>()
                         where e.GradeId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<Grade> GetGrades(string year, string reviewCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.GradeSet
                            where a.Year == year && a.ReviewCode == reviewCode
                                select a;

                return query.ToFullyLoaded();
            }
        }
      
    }
}
