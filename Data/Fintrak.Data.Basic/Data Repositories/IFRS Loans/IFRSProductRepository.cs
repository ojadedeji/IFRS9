using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IIFRSProductRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IFRSProductRepository : DataRepositoryBase<IFRSProduct>, IIFRSProductRepository
    {

        protected override IFRSProduct AddEntity(BasicContext entityContext, IFRSProduct entity)
        {
            return entityContext.Set<IFRSProduct>().Add(entity);
        }

        protected override IFRSProduct UpdateEntity(BasicContext entityContext, IFRSProduct entity)
        {
            return (from e in entityContext.Set<IFRSProduct>() 
                    where e.ProductId == entity.ProductId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IFRSProduct> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<IFRSProduct>()
                   select e;
        }

        protected override IFRSProduct GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IFRSProduct>()
                         where e.ProductId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IFRSProductInfo> GetIFRSProducts()
        {
            using (BasicContext entityContext = new BasicContext())
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