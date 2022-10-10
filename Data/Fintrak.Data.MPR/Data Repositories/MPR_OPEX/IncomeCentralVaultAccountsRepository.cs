using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomeCentralVaultAccountsRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeCentralVaultAccountsRepository : DataRepositoryBase<IncomeCentralVaultAccounts>, IIncomeCentralVaultAccountsRepository
    {

        protected override IncomeCentralVaultAccounts AddEntity(MPRContext entityContext, IncomeCentralVaultAccounts entity)
        {
            return entityContext.Set<IncomeCentralVaultAccounts>().Add(entity);
        }

        protected override IncomeCentralVaultAccounts UpdateEntity(MPRContext entityContext, IncomeCentralVaultAccounts entity)
        {
            return (from e in entityContext.Set<IncomeCentralVaultAccounts>()
                    where e.IncomeCentralVaultAccountsId == entity.IncomeCentralVaultAccountsId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomeCentralVaultAccounts> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<IncomeCentralVaultAccounts>()
                   select e;
        }

        protected override IncomeCentralVaultAccounts GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeCentralVaultAccounts>()
                         where e.IncomeCentralVaultAccountsId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
