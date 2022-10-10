using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IForecastedMacroeconimcsSensitivityRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ForecastedMacroeconimcsSensitivityRepository : DataRepositoryBase<ForecastedMacroeconimcsSensitivity>, IForecastedMacroeconimcsSensitivityRepository
    {
        protected override ForecastedMacroeconimcsSensitivity AddEntity(IFRSContext entityContext, ForecastedMacroeconimcsSensitivity entity)
        {
            return entityContext.Set<ForecastedMacroeconimcsSensitivity>().Add(entity);
        }

        protected override ForecastedMacroeconimcsSensitivity UpdateEntity(IFRSContext entityContext, ForecastedMacroeconimcsSensitivity entity)
        {
            return (from e in entityContext.Set<ForecastedMacroeconimcsSensitivity>()
                    where e.ForecastedMacroeconimcsSensitivityId == entity.ForecastedMacroeconimcsSensitivityId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ForecastedMacroeconimcsSensitivity> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<ForecastedMacroeconimcsSensitivity>()
                   select e;
        }

        protected override ForecastedMacroeconimcsSensitivity GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ForecastedMacroeconimcsSensitivity>()
                         where e.ForecastedMacroeconimcsSensitivityId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ForecastedMacroeconimcsSensitivityInfo> GetForecastedMacroeconimcsSensitivitys()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.ForecastedMacroeconimcsSensitivitySet
                            join b in entityContext.SectorSet on a.Sector_Code equals b.Code
                            join c in entityContext.MacroEconomicVariableSet on a.Variable equals c.Name
                            select new ForecastedMacroeconimcsSensitivityInfo()
                            {
                                ForecastedMacroeconimcsSensitivity = a,
                                Sector = b,
                                MacroEconomicVariable = c
                            };

                return query.ToFullyLoaded();
            }
        }
       
    }
}