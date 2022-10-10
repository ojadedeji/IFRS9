using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IMacroEconomicHistoricalRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MacroEconomicHistoricalRepository : DataRepositoryBase<MacroEconomicHistorical>, IMacroEconomicHistoricalRepository
    {
        protected override MacroEconomicHistorical AddEntity(IFRSContext entityContext, MacroEconomicHistorical entity)
        {
            return entityContext.Set<MacroEconomicHistorical>().Add(entity);
        }

        protected override MacroEconomicHistorical UpdateEntity(IFRSContext entityContext, MacroEconomicHistorical entity)
        {
            return (from e in entityContext.Set<MacroEconomicHistorical>()
                    where e.MacroEconomicHistoricalId == entity.MacroEconomicHistoricalId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<MacroEconomicHistorical> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<MacroEconomicHistorical>()
                   select e;
        }

        protected override MacroEconomicHistorical GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<MacroEconomicHistorical>()
                         where e.MacroEconomicHistoricalId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<MacroEconomicHistoricalInfo> GetMacroEconomicHistoricals()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.MacroEconomicHistoricalSet
                            join b in entityContext.SectorSet on a.Sector_Code equals b.Code
                            join c in entityContext.MacroEconomicVariableSet on a.Variable equals c.Name
                            select new MacroEconomicHistoricalInfo()
                            {
                                MacroEconomicHistorical = a,
                                Sector = b,
                                MacroEconomicVariable = c
                            };

                return query.ToFullyLoaded();
            }
        }
       
    }
}