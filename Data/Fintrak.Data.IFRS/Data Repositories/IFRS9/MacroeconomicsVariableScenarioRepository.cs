using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IMacroeconomicsVariableScenarioRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MacroeconomicsVariableScenarioRepository : DataRepositoryBase<MacroeconomicsVariableScenario>, IMacroeconomicsVariableScenarioRepository
    {
        protected override MacroeconomicsVariableScenario AddEntity(IFRSContext entityContext, MacroeconomicsVariableScenario entity)
        {
            return entityContext.Set<MacroeconomicsVariableScenario>().Add(entity);
        }

        protected override MacroeconomicsVariableScenario UpdateEntity(IFRSContext entityContext, MacroeconomicsVariableScenario entity)
        {
            return (from e in entityContext.Set<MacroeconomicsVariableScenario>()
                    where e.MacroeconomicsId == entity.MacroeconomicsId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<MacroeconomicsVariableScenario> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<MacroeconomicsVariableScenario>()
                   select e;
        }

        protected override MacroeconomicsVariableScenario GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<MacroeconomicsVariableScenario>()
                         where e.MacroeconomicsId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<MacroeconomicsVariableScenario> GetEntitiesByFlag(int Flag)
        {

            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<MacroeconomicsVariableScenario>()
                             where e.Flag == Flag
                             select e);

                return query.ToArray();
            }
        }

    }
}