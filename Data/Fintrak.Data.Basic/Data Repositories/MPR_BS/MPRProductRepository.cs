using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IMPRProductRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MPRProductRepository : DataRepositoryBase<MPRProduct>, IMPRProductRepository
    {

        protected override MPRProduct AddEntity(BasicContext entityContext, MPRProduct entity)
        {
            return entityContext.Set<MPRProduct>().Add(entity);
        }

        protected override MPRProduct UpdateEntity(BasicContext entityContext, MPRProduct entity)
        {
            return (from e in entityContext.Set<MPRProduct>() 
                    where e.ProductId == entity.ProductId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<MPRProduct> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<MPRProduct>()
                   select e;
        }

        protected override MPRProduct GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<MPRProduct>()
                         where e.ProductId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<MPRProductInfo> GetMPRProducts()
        {
            using (BasicContext entityContext = new BasicContext())
            {
                var query = from a in entityContext.MPRProductSet
                            join b in entityContext.BSCaptionSet on a.CaptionCode equals b.CaptionCode
                            join c in entityContext.ProductSet on a.ProductCode equals c.Code
                            select new MPRProductInfo()
                            {
                                MPRProduct = a,
                                BSCaption  = b,
                                Product = c
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
