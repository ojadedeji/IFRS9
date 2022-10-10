using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ISectorialRegressedPDRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SectorialRegressedPDRepository : DataRepositoryBase<SectorialRegressedPD>, ISectorialRegressedPDRepository
    {
        protected override SectorialRegressedPD AddEntity(IFRSContext entityContext, SectorialRegressedPD entity)
        {
            return entityContext.Set<SectorialRegressedPD>().Add(entity);
        }

        protected override SectorialRegressedPD UpdateEntity(IFRSContext entityContext, SectorialRegressedPD entity)
        {
            return (from e in entityContext.Set<SectorialRegressedPD>()
                    where e.SectorialRegressedPDId == entity.SectorialRegressedPDId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<SectorialRegressedPD> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<SectorialRegressedPD>()
                   select e;
        }

        protected override SectorialRegressedPD GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<SectorialRegressedPD>()
                         where e.SectorialRegressedPDId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}