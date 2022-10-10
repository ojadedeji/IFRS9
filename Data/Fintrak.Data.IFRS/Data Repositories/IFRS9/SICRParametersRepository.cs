using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;
using Fintrak.Shared.Common.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ISICRParametersRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SICRParametersRepository : DataRepositoryBase<SICRParameters>, ISICRParametersRepository
    {
        protected override SICRParameters AddEntity(IFRSContext entityContext, SICRParameters entity)
        {
            return entityContext.Set<SICRParameters>().Add(entity);
        }

        protected override SICRParameters UpdateEntity(IFRSContext entityContext, SICRParameters entity)
        {
            return (from e in entityContext.Set<SICRParameters>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<SICRParameters> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<SICRParameters>()
                   select e;
        }

        protected override SICRParameters GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<SICRParameters>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
  }
}
