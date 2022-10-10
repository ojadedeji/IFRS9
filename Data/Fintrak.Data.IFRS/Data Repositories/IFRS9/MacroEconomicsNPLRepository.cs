using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IMacroEconomicsNPLRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MacroEconomicsNPLRepository : DataRepositoryBase<MacroEconomicsNPL>, IMacroEconomicsNPLRepository
    {
        protected override MacroEconomicsNPL AddEntity(IFRSContext entityContext, MacroEconomicsNPL entity)
        {
            return entityContext.Set<MacroEconomicsNPL>().Add(entity);
        }

        protected override MacroEconomicsNPL UpdateEntity(IFRSContext entityContext, MacroEconomicsNPL entity)
        {
            return (from e in entityContext.Set<MacroEconomicsNPL>()
                    where e.macroeconomicnplId == entity.macroeconomicnplId
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<MacroEconomicsNPL> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<MacroEconomicsNPL>()
                   select e;
        }

        protected override MacroEconomicsNPL GetEntity(IFRSContext entityContext, int macroeconomicnplId)
        {
            var query = (from e in entityContext.Set<MacroEconomicsNPL>()
                         where e.macroeconomicnplId == macroeconomicnplId
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}