using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IIFRSProductRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IFRSProductRepository : DataRepositoryBase<IFRSProduct>, IIFRSProductRepository
    {

        protected override IFRSProduct AddEntity(IFRSContext entityContext, IFRSProduct entity)
        {
            return entityContext.Set<IFRSProduct>().Add(entity);
        }

        protected override IFRSProduct UpdateEntity(IFRSContext entityContext, IFRSProduct entity)
        {
            return (from e in entityContext.Set<IFRSProduct>() 
                    where e.ProductId == entity.ProductId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IFRSProduct> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<IFRSProduct>()
                   select e;
        }

        protected override IFRSProduct GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IFRSProduct>()
                         where e.ProductId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IFRSProductInfo> GetIFRSProducts()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.IFRSProductSet
                            join b in entityContext.ProductSet on a.ProductCode equals b.Code
                            join c in entityContext.ScheduleTypeSet on a.ScheduleTypeCode equals c.Code
                            select new IFRSProductInfo()
                            {
                                IFRSProduct = a,
                                Product = b,
                                ScheduleType = c
                            };

                return query.ToFullyLoaded();
            }
        }
 
    }
}