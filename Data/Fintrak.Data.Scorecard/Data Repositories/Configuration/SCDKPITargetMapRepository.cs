using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Scorecard.Entities;
using Fintrak.Data.Scorecard.Contracts;

namespace Fintrak.Data.Scorecard
{
    [Export(typeof(ISCDKPITargetMapRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SCDKPITargetMapRepository : DataRepositoryBase<SCDKPITargetMap>, ISCDKPITargetMapRepository
    {

        protected override SCDKPITargetMap AddEntity(ScorecardContext entityContext, SCDKPITargetMap entity)
        {
            return entityContext.Set<SCDKPITargetMap>().Add(entity);
        }

        protected override SCDKPITargetMap UpdateEntity(ScorecardContext entityContext, SCDKPITargetMap entity)
        {
            return (from e in entityContext.Set<SCDKPITargetMap>()
                    where e.MapId == entity.MapId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<SCDKPITargetMap> GetEntities(ScorecardContext entityContext)
        {
            return from e in entityContext.Set<SCDKPITargetMap>()
                   select e;
        }

        protected override SCDKPITargetMap GetEntity(ScorecardContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<SCDKPITargetMap>()
                         where e.MapId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }


        public IEnumerable<SCDKPITargetMapInfo> GetSCDKPITargetMaps()
        {
            using (ScorecardContext entityContext = new ScorecardContext())
            {
                var query = from a in entityContext.SCDKPITargetMapSet
                            join b in entityContext.SCDKPISet on a.KPICode equals b.Code
                            select new SCDKPITargetMapInfo()
                            {
                                scdkpitargetmap = a,
                                scdkpi = b
                            };

                return query.ToFullyLoaded();
            }
        }


    }
}
