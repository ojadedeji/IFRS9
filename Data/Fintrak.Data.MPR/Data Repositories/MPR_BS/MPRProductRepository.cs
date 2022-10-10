using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IMPRProductRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MPRProductRepository : DataRepositoryBase<MPRProduct>, IMPRProductRepository
    {

        protected override MPRProduct AddEntity(MPRContext entityContext, MPRProduct entity)
        {
            return entityContext.Set<MPRProduct>().Add(entity);
        }

        protected override MPRProduct UpdateEntity(MPRContext entityContext, MPRProduct entity)
        {
            return (from e in entityContext.Set<MPRProduct>()
                    where e.ProductId == entity.ProductId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<MPRProduct> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<MPRProduct>().Take(100)
                   select e;
        }

        protected override MPRProduct GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<MPRProduct>()
                         where e.ProductId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<MPRProductInfo> GetMPRProducts()
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.MPRProductSet
                            join b in entityContext.BSCaptionSet on a.CaptionCode equals b.CaptionCode
                            join c in entityContext.ProductSet on a.ProductCode equals c.Code
                            select new MPRProductInfo()
                            {
                                MPRProduct = a,
                                BSCaption = b,
                                Product = c
                            };

                return query.ToFullyLoaded().Take(100);
            }
        }

        public IEnumerable<MPRProductInfo> GetMPRProducts(string productCode)
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.MPRProductSet
                            join b in entityContext.BSCaptionSet on a.CaptionCode equals b.CaptionCode
                            join c in entityContext.ProductSet on a.ProductCode equals c.Code
                            where a.ProductCode.Contains(productCode)
                            select new MPRProductInfo()
                            {
                                MPRProduct = a,
                                BSCaption = b,
                                Product = c
                            };

                return query.ToFullyLoaded();
            }
        }

    }
}
