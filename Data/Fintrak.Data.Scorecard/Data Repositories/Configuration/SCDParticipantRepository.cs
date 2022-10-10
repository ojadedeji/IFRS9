using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Scorecard.Entities;
using Fintrak.Data.Scorecard.Contracts;

namespace Fintrak.Data.Scorecard
{
    [Export(typeof(ISCDParticipantRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SCDParticipantRepository : DataRepositoryBase<SCDParticipant>, ISCDParticipantRepository
    {

        protected override SCDParticipant AddEntity(ScorecardContext entityContext, SCDParticipant entity)
        {
            return entityContext.Set<SCDParticipant>().Add(entity);
        }

        protected override SCDParticipant UpdateEntity(ScorecardContext entityContext, SCDParticipant entity)
        {
            return (from e in entityContext.Set<SCDParticipant>()
                    where e.ParticipantId == entity.ParticipantId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<SCDParticipant> GetEntities(ScorecardContext entityContext)
        {
            return from e in entityContext.Set<SCDParticipant>()
                   select e;
        }

        protected override SCDParticipant GetEntity(ScorecardContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<SCDParticipant>()
                         where e.ParticipantId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<SCDParticipantInfo> GetSCDParticipants()
        {
            using (ScorecardContext entityContext = new ScorecardContext())
            {
                var query = from a in entityContext.SCDParticipantSet
                            join b in entityContext.SCDKPISet on a.KPICode equals b.Code
                            join c in entityContext.StaffSet on a.StaffCode equals c.StaffCode into staffs
                            from st in staffs.DefaultIfEmpty()
                            join d in entityContext.SCDTeamClassificationSet on a.TeamClassificationCode equals d.Code
                            select new SCDParticipantInfo()
                            {
                                SCDParticipant = a,
                                SCDKPI = b,
                                Staff = st,
                                Classification = d
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}

//KPICode	
//StaffCode