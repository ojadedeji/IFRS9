using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Budget.Entities;
using Fintrak.Data.Budget.Contracts;

namespace Fintrak.Data.Budget
{
    [Export(typeof(IGeneralSettingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GeneralSettingRepository : DataRepositoryBase<GeneralSetting>, IGeneralSettingRepository
    {

        protected override GeneralSetting AddEntity(BudgetContext entityContext, GeneralSetting entity)
        {
            return entityContext.Set<GeneralSetting>().Add(entity);
        }

        protected override GeneralSetting UpdateEntity(BudgetContext entityContext, GeneralSetting entity)
        {
            return (from e in entityContext.Set<GeneralSetting>() 
                    where e.GeneralSettingId == entity.GeneralSettingId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<GeneralSetting> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<GeneralSetting>()
                   select e;
        }

        protected override GeneralSetting GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<GeneralSetting>()
                         where e.GeneralSettingId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

      
    }
}
