using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.CDQM.Entities;
using Fintrak.Data.CDQM.Contracts;

namespace Fintrak.Data.CDQM
{
    [Export(typeof(ICDQMCustomerMISRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CDQMCustomerMISRepository : DataRepositoryBase<CDQMCustomerMIS>, ICDQMCustomerMISRepository
    {

        protected override CDQMCustomerMIS AddEntity(CDQMContext entityContext, CDQMCustomerMIS entity)
        {
            return entityContext.Set<CDQMCustomerMIS>().Add(entity);
        }

        protected override CDQMCustomerMIS UpdateEntity(CDQMContext entityContext, CDQMCustomerMIS entity)
        {
            return (from e in entityContext.Set<CDQMCustomerMIS>() 
                    where e.CustomerMISId == entity.CustomerMISId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CDQMCustomerMIS> GetEntities(CDQMContext entityContext)
        {
            return from e in entityContext.Set<CDQMCustomerMIS>()
                   select e;
        }

        protected override CDQMCustomerMIS GetEntity(CDQMContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CDQMCustomerMIS>()
                         where e.CustomerMISId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
