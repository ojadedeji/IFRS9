using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(INonProductRateRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class NonProductRateRepository : DataRepositoryBase<NonProductRate>, INonProductRateRepository
    {

        protected override NonProductRate AddEntity(BasicContext entityContext, NonProductRate entity)
        {
            return entityContext.Set<NonProductRate>().Add(entity);
        }

        protected override NonProductRate UpdateEntity(BasicContext entityContext, NonProductRate entity)
        {
            return (from e in entityContext.Set<NonProductRate>()
                    where e.NonProductRateId == entity.NonProductRateId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<NonProductRate> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<NonProductRate>()
                   select e;
        }

        protected override NonProductRate GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<NonProductRate>()
                         where e.NonProductRateId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<NonProductRateInfo> GetNonProductRates()
        {
            using (BasicContext entityContext = new BasicContext())
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
