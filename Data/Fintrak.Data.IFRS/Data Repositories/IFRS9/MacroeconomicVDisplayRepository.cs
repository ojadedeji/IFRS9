using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IMacroeconomicVDisplayRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MacroeconomicVDisplayRepository : DataRepositoryBase<MacroeconomicVDisplay>, IMacroeconomicVDisplayRepository
    {
        protected override MacroeconomicVDisplay AddEntity(IFRSContext entityContext, MacroeconomicVDisplay entity)
        {
            return entityContext.Set<MacroeconomicVDisplay>().Add(entity);
        }

        protected override MacroeconomicVDisplay UpdateEntity(IFRSContext entityContext, MacroeconomicVDisplay entity)
        {
            return (from e in entityContext.Set<MacroeconomicVDisplay>()
                    where e.MacroVariableDisplayId == entity.MacroVariableDisplayId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<MacroeconomicVDisplay> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<MacroeconomicVDisplay>()
                   select e;
        }

        protected override MacroeconomicVDisplay GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<MacroeconomicVDisplay>()
                         where e.MacroVariableDisplayId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<MacroeconomicVDisplay> GetMacroeconomicVDisplayByYear(int yr)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.MacroeconomicVDisplaySet
                            where a.Year == yr
                            select a;

                return query.ToFullyLoaded();
            }
        }
       
    }
}