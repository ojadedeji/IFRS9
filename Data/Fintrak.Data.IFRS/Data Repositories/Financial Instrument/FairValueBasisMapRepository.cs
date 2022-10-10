using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IFairValueBasisMapRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FairValueBasisMapRepository : DataRepositoryBase<FairValueBasisMap>, IFairValueBasisMapRepository
    {

        protected override FairValueBasisMap AddEntity(IFRSContext entityContext, FairValueBasisMap entity)
        {
            return entityContext.Set<FairValueBasisMap>().Add(entity);
        }

        protected override FairValueBasisMap UpdateEntity(IFRSContext entityContext, FairValueBasisMap entity)
        {
            return (from e in entityContext.Set<FairValueBasisMap>() 
                    where e.FairValueBasisMapId == entity.FairValueBasisMapId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<FairValueBasisMap> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<FairValueBasisMap>()
                   select e;
        }

        protected override FairValueBasisMap GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<FairValueBasisMap>()
                         where e.FairValueBasisMapId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
 
    }
}
