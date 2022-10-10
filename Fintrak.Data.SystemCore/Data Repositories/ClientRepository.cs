using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Data.SystemCore.Contracts;
using Fintrak.Shared.SystemCore.Entities;

namespace Fintrak.Data.SystemCore
{
    [Export(typeof(IClientRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ClientRepository : DataRepositoryBase<Client>, IClientRepository
    {

        protected override Client AddEntity(SystemCoreContext entityContext, Client entity)
        {
            return entityContext.Set<Client>().Add(entity);
        }

        protected override Client UpdateEntity(SystemCoreContext entityContext, Client entity)
        {
            return (from e in entityContext.Set<Client>() 
                    where e.ClientId == entity.ClientId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Client> GetEntities(SystemCoreContext entityContext)
        {
            return from e in entityContext.Set<Client>()
                   select e;
        }

        protected override Client GetEntity(SystemCoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<Client>()
                         where e.ClientId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
