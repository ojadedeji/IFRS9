using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IForecastedMacroeconimcsScenarioRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ForecastedMacroeconimcsScenarioRepository : DataRepositoryBase<ForecastedMacroeconimcsScenario>, IForecastedMacroeconimcsScenarioRepository
    {
        protected override ForecastedMacroeconimcsScenario AddEntity(IFRSContext entityContext, ForecastedMacroeconimcsScenario entity)
        {
            return entityContext.Set<ForecastedMacroeconimcsScenario>().Add(entity);
        }

        protected override ForecastedMacroeconimcsScenario UpdateEntity(IFRSContext entityContext, ForecastedMacroeconimcsScenario entity)
        {
            return (from e in entityContext.Set<ForecastedMacroeconimcsScenario>()
                    where e.ForecastedMacroeconimcsScenarioId == entity.ForecastedMacroeconimcsScenarioId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ForecastedMacroeconimcsScenario> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<ForecastedMacroeconimcsScenario>()
                   select e;
        }

        protected override ForecastedMacroeconimcsScenario GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ForecastedMacroeconimcsScenario>()
                         where e.ForecastedMacroeconimcsScenarioId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ForecastedMacroeconimcsScenarioInfo> GetForecastedMacroeconimcsScenarios()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.ForecastedMacroeconimcsScenarioSet
                            join b in entityContext.SectorSet on a.Sector_Code equals b.Code
                            join c in entityContext.MacroEconomicVariableSet on a.Variable equals c.Name
                            select new ForecastedMacroeconimcsScenarioInfo()
                            {
                                ForecastedMacroeconimcsScenario = a,
                                Sector = b,
                                MacroEconomicVariable = c
                            };

                return query.ToFullyLoaded();
            }
        }
       
    }
}