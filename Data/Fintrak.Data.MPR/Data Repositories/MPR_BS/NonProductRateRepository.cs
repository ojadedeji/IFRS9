using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(INonProductRateRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class NonProductRateRepository : DataRepositoryBase<NonProductRate>, INonProductRateRepository
    {

        protected override NonProductRate AddEntity(MPRContext entityContext, NonProductRate entity)
        {
            return entityContext.Set<NonProductRate>().Add(entity);
        }

        protected override NonProductRate UpdateEntity(MPRContext entityContext, NonProductRate entity)
        {
            return (from e in entityContext.Set<NonProductRate>()
                    where e.NonProductRateId == entity.NonProductRateId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<NonProductRate> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<NonProductRate>()
                   select e;
        }

        protected override NonProductRate GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<NonProductRate>()
                         where e.NonProductRateId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<NonProductRateInfo> GetNonProductRates()
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.NonProductRateSet
                            join b in entityContext.ProductSet on a.ProductCode equals b.Code
                            select new NonProductRateInfo()
                            {
                                NonProductRate = a,
                                Product = b
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
