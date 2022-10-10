using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(ICostCentreDefinitionRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CostCentreDefinitionRepository : DataRepositoryBase<CostCentreDefinition>, ICostCentreDefinitionRepository
    {

        protected override CostCentreDefinition AddEntity(MPRContext entityContext, CostCentreDefinition entity)
        {
            return entityContext.Set<CostCentreDefinition>().Add(entity);
        }

        protected override CostCentreDefinition UpdateEntity(MPRContext entityContext, CostCentreDefinition entity)
        {
            return (from e in entityContext.Set<CostCentreDefinition>()
                    where e.CCDefinitionId == entity.CCDefinitionId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CostCentreDefinition> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<CostCentreDefinition>()
                   select e;
        }

        protected override CostCentreDefinition GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CostCentreDefinition>()
                         where e.CCDefinitionId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
