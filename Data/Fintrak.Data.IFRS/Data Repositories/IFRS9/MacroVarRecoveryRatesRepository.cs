using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IMacroVarRecoveryRatesRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MacroVarRecoveryRatesRepository : DataRepositoryBase<MacroVarRecoveryRates>, IMacroVarRecoveryRatesRepository
    {
        protected override MacroVarRecoveryRates AddEntity(IFRSContext entityContext, MacroVarRecoveryRates entity)
        {
            return entityContext.Set<MacroVarRecoveryRates>().Add(entity);
        }

        protected override MacroVarRecoveryRates UpdateEntity(IFRSContext entityContext, MacroVarRecoveryRates entity)
        {
            return (from e in entityContext.Set<MacroVarRecoveryRates>()
                    where e.RecoveryRatesId == entity.RecoveryRatesId
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<MacroVarRecoveryRates> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<MacroVarRecoveryRates>()
                   select e;
        }

        protected override MacroVarRecoveryRates GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<MacroVarRecoveryRates>()
                         where e.RecoveryRatesId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }


        public IEnumerable<MacroVarRecoveryRates> GetEntityByRecoveryRatesId(int id)
        {

            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.MacroVarRecoveryRatesSet
                            where a.RecoveryRatesId == id
                            select a;

                return query.ToFullyLoaded();
            }
        }
       
    }
}