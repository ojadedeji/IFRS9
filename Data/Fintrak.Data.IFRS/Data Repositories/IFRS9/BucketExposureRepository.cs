using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IBucketExposureRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BucketExposureRepository : DataRepositoryBase<BucketExposure>, IBucketExposureRepository
    {
        protected override BucketExposure AddEntity(IFRSContext entityContext, BucketExposure entity)
        {
            return entityContext.Set<BucketExposure>().Add(entity);
        }

        protected override BucketExposure UpdateEntity(IFRSContext entityContext, BucketExposure entity)
        {
            return (from e in entityContext.Set<BucketExposure>()
                    where e.BucketExposureId == entity.BucketExposureId
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<BucketExposure> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<BucketExposure>()
                   select e;
        }

        protected override BucketExposure GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<BucketExposure>()
                         where e.BucketExposureId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}