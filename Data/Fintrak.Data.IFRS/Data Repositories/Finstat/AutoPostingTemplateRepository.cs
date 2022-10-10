using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IAutoPostingTemplateRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AutoPostingTemplateRepository : DataRepositoryBase<AutoPostingTemplate>, IAutoPostingTemplateRepository
    {

        protected override AutoPostingTemplate AddEntity(IFRSContext entityContext, AutoPostingTemplate entity)
        {
            return entityContext.Set<AutoPostingTemplate>().Add(entity);
        }

        protected override AutoPostingTemplate UpdateEntity(IFRSContext entityContext, AutoPostingTemplate entity)
        {
            return (from e in entityContext.Set<AutoPostingTemplate>() 
                    where e.AutoPostingTemplateId == entity.AutoPostingTemplateId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<AutoPostingTemplate> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<AutoPostingTemplate>()
                   select e;
        }

        protected override AutoPostingTemplate GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<AutoPostingTemplate>()
                         where e.AutoPostingTemplateId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
