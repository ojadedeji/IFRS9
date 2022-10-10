using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.CDQM.Entities;
using Fintrak.Data.CDQM.Contracts;

namespace Fintrak.Data.CDQM
{
    [Export(typeof(ICDQMMerchantRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CDQMMerchantRepository : DataRepositoryBase<CDQMMerchant>, ICDQMMerchantRepository
    {

        protected override CDQMMerchant AddEntity(CDQMContext entityContext, CDQMMerchant entity)
        {
            return entityContext.Set<CDQMMerchant>().Add(entity);
        }

        protected override CDQMMerchant UpdateEntity(CDQMContext entityContext, CDQMMerchant entity)
        {
            return (from e in entityContext.Set<CDQMMerchant>() 
                    where e.MerchantId == entity.MerchantId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CDQMMerchant> GetEntities(CDQMContext entityContext)
        {
            return from e in entityContext.Set<CDQMMerchant>()
                   select e;
        }

        protected override CDQMMerchant GetEntity(CDQMContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CDQMMerchant>()
                         where e.MerchantId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
