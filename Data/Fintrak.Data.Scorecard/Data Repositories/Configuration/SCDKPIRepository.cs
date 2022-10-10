using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Scorecard.Entities;
using Fintrak.Data.Scorecard.Contracts;

namespace Fintrak.Data.Scorecard
{
    [Export(typeof(ISCDKPIRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SCDKPIRepository : DataRepositoryBase<SCDKPI>, ISCDKPIRepository
    {

        protected override SCDKPI AddEntity(ScorecardContext entityContext, SCDKPI entity)
        {
            return entityContext.Set<SCDKPI>().Add(entity);
        }

        protected override SCDKPI UpdateEntity(ScorecardContext entityContext, SCDKPI entity)
        {
            return (from e in entityContext.Set<SCDKPI>()
                    where e.KPIId == entity.KPIId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<SCDKPI> GetEntities(ScorecardContext entityContext)
        {
            return from e in entityContext.Set<SCDKPI>()
                   select e;
        }

        protected override SCDKPI GetEntity(ScorecardContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<SCDKPI>()
                         where e.KPIId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<SCDKPIInfo> GetSCDKPIs()
        {
            using (ScorecardContext entityContext = new ScorecardContext())
            {
                var query = from a in entityContext.SCDKPISet
                            join b in entityContext.SCDCategorySet on a.CategoryCode equals b.Code into ab
                            from abi in ab.DefaultIfEmpty()
                            select new SCDKPIInfo()
                            {
                                SCDKPI = a,
                                SCDCategory = abi 
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
