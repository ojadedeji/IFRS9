using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Data.Core.Contracts;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Core.Entities;

namespace Fintrak.Data.Core
{
    [Export(typeof(IClosedPeriodTemplateRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ClosedPeriodTemplateRepository : DataRepositoryBase<ClosedPeriodTemplate>, IClosedPeriodTemplateRepository
    {
        protected override ClosedPeriodTemplate AddEntity(CoreContext entityContext, ClosedPeriodTemplate entity)
        {
            return entityContext.Set<ClosedPeriodTemplate>().Add(entity);
        }

        protected override ClosedPeriodTemplate UpdateEntity(CoreContext entityContext, ClosedPeriodTemplate entity)
        {
            return (from e in entityContext.Set<ClosedPeriodTemplate>()
                    where e.ClosedPeriodTemplateId == entity.ClosedPeriodTemplateId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ClosedPeriodTemplate> GetEntities(CoreContext entityContext)
        {
            return from e in entityContext.Set<ClosedPeriodTemplate>()
                   select e;
        }

        protected override ClosedPeriodTemplate GetEntity(CoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ClosedPeriodTemplate>()
                         where e.ClosedPeriodTemplateId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ClosedPeriodTemplateInfo> GetClosedPeriodTemplates()
        {
            using (CoreContext entityContext = new CoreContext())
            {
                var query = from a in entityContext.ClosedPeriodTemplateSet
                          
                            select new ClosedPeriodTemplateInfo()
                            {
                                ClosedPeriodTemplate = a
                            };

                return query.ToFullyLoaded();
            }
        }
    }
}
