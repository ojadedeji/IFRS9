using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Scorecard.Entities;
using Fintrak.Data.Scorecard.Contracts;

namespace Fintrak.Data.Scorecard
{
    [Export(typeof(ISCDTeamClassificationRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SCDTeamClassificationRepository : DataRepositoryBase<SCDTeamClassification>, ISCDTeamClassificationRepository
    {

        protected override SCDTeamClassification AddEntity(ScorecardContext entityContext, SCDTeamClassification entity)
        {
            return entityContext.Set<SCDTeamClassification>().Add(entity);
        }

        protected override SCDTeamClassification UpdateEntity(ScorecardContext entityContext, SCDTeamClassification entity)
        {
            return (from e in entityContext.Set<SCDTeamClassification>()
                    where e.TeamClassificationId == entity.TeamClassificationId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<SCDTeamClassification> GetEntities(ScorecardContext entityContext)
        {
            return from e in entityContext.Set<SCDTeamClassification>()
                   select e;
        }

        protected override SCDTeamClassification GetEntity(ScorecardContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<SCDTeamClassification>()
                         where e.TeamClassificationId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }


    }
}
