using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ISectorialExposureRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SectorialExposureRepository : DataRepositoryBase<SectorialExposure>, ISectorialExposureRepository
    {
        protected override SectorialExposure AddEntity(IFRSContext entityContext, SectorialExposure entity)
        {
            return entityContext.Set<SectorialExposure>().Add(entity);
        }

        protected override SectorialExposure UpdateEntity(IFRSContext entityContext, SectorialExposure entity)
        {
            return (from e in entityContext.Set<SectorialExposure>()
                    where e.SectorialExposureId == entity.SectorialExposureId
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<SectorialExposure> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<SectorialExposure>()
                   select e;
        }

        protected override SectorialExposure GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<SectorialExposure>()
                         where e.SectorialExposureId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}