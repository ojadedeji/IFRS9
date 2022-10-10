using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Scorecard.Entities;
using Fintrak.Data.Scorecard.Contracts;

namespace Fintrak.Data.Scorecard
{
    [Export(typeof(ISCDKPIActualMapRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SCDKPIActualMapRepository : DataRepositoryBase<SCDKPIActualMap>, ISCDKPIActualMapRepository
    {

        protected override SCDKPIActualMap AddEntity(ScorecardContext entityContext, SCDKPIActualMap entity)
        {
            return entityContext.Set<SCDKPIActualMap>().Add(entity);
        }

        protected override SCDKPIActualMap UpdateEntity(ScorecardContext entityContext, SCDKPIActualMap entity)
        {
            return (from e in entityContext.Set<SCDKPIActualMap>()
                    where e.MapId == entity.MapId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<SCDKPIActualMap> GetEntities(ScorecardContext entityContext)
        {
            return from e in entityContext.Set<SCDKPIActualMap>()
                   select e;
        }

        protected override SCDKPIActualMap GetEntity(ScorecardContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<SCDKPIActualMap>()
                         where e.MapId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }


        public IEnumerable<SCDKPIActualMapInfo> GetSCDKPIActualMaps()
        {
            using (ScorecardContext entityContext = new ScorecardContext())
            {
                var query = from a in entityContext.SCDKPIActualMapSet
                            join b in entityContext.SCDKPISet on a.KPICode equals b.Code
                            select new SCDKPIActualMapInfo()
                            {
                                scdkpiactualmap = a,
                                scdkpi = b
                            };

                return query.ToFullyLoaded();
            }
        }


    }
}
