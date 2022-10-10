using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Scorecard.Entities;
using Fintrak.Data.Scorecard.Contracts;

namespace Fintrak.Data.Scorecard
{
    [Export(typeof(ISCDThresholdRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SCDThresholdRepository : DataRepositoryBase<SCDThreshold>, ISCDThresholdRepository
    {

        protected override SCDThreshold AddEntity(ScorecardContext entityContext, SCDThreshold entity)
        {
            return entityContext.Set<SCDThreshold>().Add(entity);
        }

        protected override SCDThreshold UpdateEntity(ScorecardContext entityContext, SCDThreshold entity)
        {
            return (from e in entityContext.Set<SCDThreshold>()
                    where e.ThresholdId == entity.ThresholdId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<SCDThreshold> GetEntities(ScorecardContext entityContext)
        {
            return from e in entityContext.Set<SCDThreshold>()
                   select e;
        }

        protected override SCDThreshold GetEntity(ScorecardContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<SCDThreshold>()
                         where e.ThresholdId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<SCDThresholdInfo> GetSCDThresholds()
        {
            using (ScorecardContext entityContext = new ScorecardContext())
            {
                var query = from a in entityContext.SCDThresholdSet
                            join b in entityContext.SCDTeamClassificationSet on a.TeamClassificationCode equals b.Code
                            join c in entityContext.SCDKPISet on a.KPICode equals c.Code
                            join d in entityContext.StaffSet on a.StaffCode equals d.StaffCode into staffs
                            from st in staffs.DefaultIfEmpty()
                            select new SCDThresholdInfo()
                            {
                                SCDThreshold = a,
                                SCDTeamClassification = b,
                                SCDKPI = c,
                                Staff = st
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}

