using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(ICostCentreDefinitionRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CostCentreDefinitionRepository : DataRepositoryBase<CostCentreDefinition>, ICostCentreDefinitionRepository
    {

        protected override CostCentreDefinition AddEntity(BasicContext entityContext, CostCentreDefinition entity)
        {
            return entityContext.Set<CostCentreDefinition>().Add(entity);
        }

        protected override CostCentreDefinition UpdateEntity(BasicContext entityContext, CostCentreDefinition entity)
        {
            return (from e in entityContext.Set<CostCentreDefinition>()
                    where e.CCDefinitionId == entity.CCDefinitionId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CostCentreDefinition> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<CostCentreDefinition>()
                   select e;
        }

        protected override CostCentreDefinition GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CostCentreDefinition>()
                         where e.CCDefinitionId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
