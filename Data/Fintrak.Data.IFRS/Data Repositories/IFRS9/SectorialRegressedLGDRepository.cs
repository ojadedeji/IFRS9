using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ISectorialRegressedLGDRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SectorialRegressedLGDRepository : DataRepositoryBase<SectorialRegressedLGD>, ISectorialRegressedLGDRepository
    {
        protected override SectorialRegressedLGD AddEntity(IFRSContext entityContext, SectorialRegressedLGD entity)
        {
            return entityContext.Set<SectorialRegressedLGD>().Add(entity);
        }

        protected override SectorialRegressedLGD UpdateEntity(IFRSContext entityContext, SectorialRegressedLGD entity)
        {
            return (from e in entityContext.Set<SectorialRegressedLGD>()
                    where e.SectorialRegressedLGDId == entity.SectorialRegressedLGDId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<SectorialRegressedLGD> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<SectorialRegressedLGD>()
                   select e;
        }

        protected override SectorialRegressedLGD GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<SectorialRegressedLGD>()
                         where e.SectorialRegressedLGDId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}