using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IMacroEconomicVariableRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MacroEconomicVariableRepository : DataRepositoryBase<MacroEconomicVariable>, IMacroEconomicVariableRepository
    {
        protected override MacroEconomicVariable AddEntity(IFRSContext entityContext, MacroEconomicVariable entity)
        {
            return entityContext.Set<MacroEconomicVariable>().Add(entity);
        }

        protected override MacroEconomicVariable UpdateEntity(IFRSContext entityContext, MacroEconomicVariable entity)
        {
            return (from e in entityContext.Set<MacroEconomicVariable>()
                    where e.MacroEconomicVariableId == entity.MacroEconomicVariableId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<MacroEconomicVariable> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<MacroEconomicVariable>()
                   select e;
        }

        protected override MacroEconomicVariable GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<MacroEconomicVariable>()
                         where e.MacroEconomicVariableId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}