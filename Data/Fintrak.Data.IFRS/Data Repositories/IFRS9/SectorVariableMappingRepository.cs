using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ISectorVariableMappingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SectorVariableMappingRepository : DataRepositoryBase<SectorVariableMapping>, ISectorVariableMappingRepository
    {
        protected override SectorVariableMapping AddEntity(IFRSContext entityContext, SectorVariableMapping entity)
        {
            return entityContext.Set<SectorVariableMapping>().Add(entity);
        }

        protected override SectorVariableMapping UpdateEntity(IFRSContext entityContext, SectorVariableMapping entity)
        {
            return (from e in entityContext.Set<SectorVariableMapping>()
                    where e.SectorVariableMappingId == entity.SectorVariableMappingId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<SectorVariableMapping> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<SectorVariableMapping>()
                   select e;
        }

        protected override SectorVariableMapping GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<SectorVariableMapping>()
                         where e.SectorVariableMappingId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<SectorVariableMappingInfo> GetSectorVariableMappings()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.SectorVariableMappingSet
                            join b in entityContext.SectorSet on a.Sector equals b.Code
                            join c in entityContext.MacroEconomicVariableSet on a.Variable equals c.Name
                            select new SectorVariableMappingInfo()
                            {
                                SectorVariableMapping = a,
                                Sector = b,
                                MacroEconomicVariable = c
                            };

                return query.ToFullyLoaded();
            }
        }
       
    }
}