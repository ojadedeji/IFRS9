using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Scorecard.Entities;
using Fintrak.Data.Scorecard.Contracts;

namespace Fintrak.Data.Scorecard
{
    [Export(typeof(ISCDKPIEntryRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SCDKPIEntryRepository : DataRepositoryBase<SCDKPIEntry>, ISCDKPIEntryRepository
    {

        protected override SCDKPIEntry AddEntity(ScorecardContext entityContext, SCDKPIEntry entity)
        {
            return entityContext.Set<SCDKPIEntry>().Add(entity);
        }

        protected override SCDKPIEntry UpdateEntity(ScorecardContext entityContext, SCDKPIEntry entity)
        {
            return (from e in entityContext.Set<SCDKPIEntry>()
                    where e.EntryId == entity.EntryId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<SCDKPIEntry> GetEntities(ScorecardContext entityContext)
        {
            return from e in entityContext.Set<SCDKPIEntry>()
                   select e;
        }

        protected override SCDKPIEntry GetEntity(ScorecardContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<SCDKPIEntry>()
                         where e.EntryId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<SCDKPIEntryInfo> GetSCDKPIEntrys()
        {
            using (ScorecardContext entityContext = new ScorecardContext())
            {
                var query = from a in entityContext.SCDKPIEntrySet
                            join b in entityContext.StaffSet on a.StaffCode equals b.StaffCode
                            join c in entityContext.SCDTeamMapSet on a.MISCode equals c.MISCode
                            join d in entityContext.SCDKPISet on a.KPICode equals d.Code
                            select new SCDKPIEntryInfo()
                            {
                                SCDKPIEntry = a,
                                Staff = b,
                                SCDTeamMap = c,
                                SCDKPI = d
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
