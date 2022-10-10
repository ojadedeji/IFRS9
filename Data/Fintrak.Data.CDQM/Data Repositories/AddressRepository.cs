using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.CDQM.Entities;
using Fintrak.Data.CDQM.Contracts;

namespace Fintrak.Data.CDQM
{
    [Export(typeof(ICDQMAddressRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CDQMAddressRepository : DataRepositoryBase<CDQMAddress>, ICDQMAddressRepository
    {

        protected override CDQMAddress AddEntity(CDQMContext entityContext, CDQMAddress entity)
        {
            return entityContext.Set<CDQMAddress>().Add(entity);
        }

        protected override CDQMAddress UpdateEntity(CDQMContext entityContext, CDQMAddress entity)
        {
            return (from e in entityContext.Set<CDQMAddress>() 
                    where e.AddressId == entity.AddressId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CDQMAddress> GetEntities(CDQMContext entityContext)
        {
            return from e in entityContext.Set<CDQMAddress>()
                   select e;
        }

        protected override CDQMAddress GetEntity(CDQMContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CDQMAddress>()
                         where e.AddressId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
