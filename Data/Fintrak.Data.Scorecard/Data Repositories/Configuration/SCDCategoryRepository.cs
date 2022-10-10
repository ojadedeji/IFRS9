using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Scorecard.Entities;
using Fintrak.Data.Scorecard.Contracts;

namespace Fintrak.Data.Scorecard
{
    [Export(typeof(ISCDCategoryRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SCDCategoryRepository : DataRepositoryBase<SCDCategory>, ISCDCategoryRepository
    {

        protected override SCDCategory AddEntity(ScorecardContext entityContext, SCDCategory entity)
        {
            return entityContext.Set<SCDCategory>().Add(entity);
        }

        protected override SCDCategory UpdateEntity(ScorecardContext entityContext, SCDCategory entity)
        {
            return (from e in entityContext.Set<SCDCategory>()
                    where e.CategoryId == entity.CategoryId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<SCDCategory> GetEntities(ScorecardContext entityContext)
        {
            return from e in entityContext.Set<SCDCategory>()
                   select e;
        }

        protected override SCDCategory GetEntity(ScorecardContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<SCDCategory>()
                         where e.CategoryId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<SCDCategoryInfo> GetSCDCategorys()
        {
            using (ScorecardContext entityContext = new ScorecardContext())
            {
                var query = from a in entityContext.SCDCategorySet
                            join b in entityContext.SCDCategorySet on a.ParentCode equals b.Code into parents
                            from pt in parents.DefaultIfEmpty()
                            select new SCDCategoryInfo()
                            {
                                SCDCategory = a,
                                Parent = pt 
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
