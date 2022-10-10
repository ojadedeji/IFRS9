using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Scorecard.Entities;
using Fintrak.Data.Scorecard.Contracts;

namespace Fintrak.Data.Scorecard
{
    [Export(typeof(ISCDKPIClassificationRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SCDKPIClassificationRepository : DataRepositoryBase<SCDKPIClassification>, ISCDKPIClassificationRepository
    {

        protected override SCDKPIClassification AddEntity(ScorecardContext entityContext, SCDKPIClassification entity)
        {
            return entityContext.Set<SCDKPIClassification>().Add(entity);
        }

        protected override SCDKPIClassification UpdateEntity(ScorecardContext entityContext, SCDKPIClassification entity)
        {
            return (from e in entityContext.Set<SCDKPIClassification>()
                    where e.ClassificationId == entity.ClassificationId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<SCDKPIClassification> GetEntities(ScorecardContext entityContext)
        {
            return from e in entityContext.Set<SCDKPIClassification>()
                   select e;
        }

        protected override SCDKPIClassification GetEntity(ScorecardContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<SCDKPIClassification>()
                         where e.ClassificationId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<SCDKPIClassificationInfo> GetSCDKPIClassifications()
        {
            using (ScorecardContext entityContext = new ScorecardContext())
            {
                var query = from a in entityContext.SCDKPIClassificationSet
                            join b in entityContext.SCDKPISet on a.KPICode equals b.Code
                            join c in entityContext.SCDTeamClassificationSet on a.TeamClassificationCode equals c.Code
                            join d in entityContext.SCDCategorySet on a.CategoryCode equals d.Code
                            select new SCDKPIClassificationInfo()
                            {
                                SCDKPIClassification = a,
                                SCDKPI = b ,
                                SCDTeamClassification = c,
                                SCDCategory=d
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}

