using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IFairValueBasisExemptionRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FairValueBasisExemptionRepository : DataRepositoryBase<FairValueBasisExemption>, IFairValueBasisExemptionRepository
    {

        protected override FairValueBasisExemption AddEntity(BasicContext entityContext, FairValueBasisExemption entity)
        {
            return entityContext.Set<FairValueBasisExemption>().Add(entity);
        }

        protected override FairValueBasisExemption UpdateEntity(BasicContext entityContext, FairValueBasisExemption entity)
        {
            return (from e in entityContext.Set<FairValueBasisExemption>() 
                    where e.FairValueBasisExemptionId == entity.FairValueBasisExemptionId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<FairValueBasisExemption> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<FairValueBasisExemption>()
                   select e;
        }

        protected override FairValueBasisExemption GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<FairValueBasisExemption>()
                         where e.FairValueBasisExemptionId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
 
    }
}
