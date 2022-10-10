using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IFairValueBasisExemptionRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FairValueBasisExemptionRepository : DataRepositoryBase<FairValueBasisExemption>, IFairValueBasisExemptionRepository
    {

        protected override FairValueBasisExemption AddEntity(IFRSContext entityContext, FairValueBasisExemption entity)
        {
            return entityContext.Set<FairValueBasisExemption>().Add(entity);
        }

        protected override FairValueBasisExemption UpdateEntity(IFRSContext entityContext, FairValueBasisExemption entity)
        {
            return (from e in entityContext.Set<FairValueBasisExemption>() 
                    where e.FairValueBasisExemptionId == entity.FairValueBasisExemptionId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<FairValueBasisExemption> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<FairValueBasisExemption>()
                   select e;
        }

        protected override FairValueBasisExemption GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<FairValueBasisExemption>()
                         where e.FairValueBasisExemptionId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
 
    }
}
