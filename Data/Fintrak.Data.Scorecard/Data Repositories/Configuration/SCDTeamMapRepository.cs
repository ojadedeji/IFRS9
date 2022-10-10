using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Scorecard.Entities;
using Fintrak.Data.Scorecard.Contracts;

namespace Fintrak.Data.Scorecard
{
    [Export(typeof(ISCDTeamMapRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SCDTeamMapRepository : DataRepositoryBase<SCDTeamMap>, ISCDTeamMapRepository
    {

        protected override SCDTeamMap AddEntity(ScorecardContext entityContext, SCDTeamMap entity)
        {
            return entityContext.Set<SCDTeamMap>().Add(entity);
        }

        protected override SCDTeamMap UpdateEntity(ScorecardContext entityContext, SCDTeamMap entity)
        {
            return (from e in entityContext.Set<SCDTeamMap>()
                    where e.TeamMapId == entity.TeamMapId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<SCDTeamMap> GetEntities(ScorecardContext entityContext)
        {
            return from e in entityContext.Set<SCDTeamMap>()
                   select e;
        }

        protected override SCDTeamMap GetEntity(ScorecardContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<SCDTeamMap>()
                         where e.TeamMapId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<SCDTeamMapInfo> GetSCDTeamMaps()
        {
            using (ScorecardContext entityContext = new ScorecardContext())
            {
                var query = from a in entityContext.SCDTeamMapSet
                            join c in entityContext.StaffSet on a.StaffCode equals c.StaffCode
                            join d in entityContext.SCDTeamClassificationSet on a.TeamClassificationCode equals d.Code
                            select new SCDTeamMapInfo()
                            {
                                SCDTeamMap = a,
                                Staff = c,
                                TeamClassification = d
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}



