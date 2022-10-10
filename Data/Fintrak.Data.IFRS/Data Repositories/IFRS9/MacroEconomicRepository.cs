using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IMacroEconomicRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MacroEconomicRepository : DataRepositoryBase<MacroEconomic>, IMacroEconomicRepository
    {
        protected override MacroEconomic AddEntity(IFRSContext entityContext, MacroEconomic entity)
        {
            return entityContext.Set<MacroEconomic>().Add(entity);
        }

        protected override MacroEconomic UpdateEntity(IFRSContext entityContext, MacroEconomic entity)
        {
            return (from e in entityContext.Set<MacroEconomic>()
                    where e.MacroEconomicId == entity.MacroEconomicId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<MacroEconomic> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<MacroEconomic>()
                   select e;
        }

        protected override MacroEconomic GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<MacroEconomic>()
                         where e.MacroEconomicId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}