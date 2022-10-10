using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;
using Fintrak.Shared.MPR.Framework;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IAccountTransferPriceRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AccountTransferPriceRepository : DataRepositoryBase<AccountTransferPrice>, IAccountTransferPriceRepository
    {

        protected override AccountTransferPrice AddEntity(MPRContext entityContext, AccountTransferPrice entity)
        {
            return entityContext.Set<AccountTransferPrice>().Add(entity);
        }

        protected override AccountTransferPrice UpdateEntity(MPRContext entityContext, AccountTransferPrice entity)
        {
            return (from e in entityContext.Set<AccountTransferPrice>() 
                    where e.AccountTransferPriceId == entity.AccountTransferPriceId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<AccountTransferPrice> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<AccountTransferPrice>()
                   select e;
        }

        protected override AccountTransferPrice GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<AccountTransferPrice>()
                         where e.AccountTransferPriceId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<AccountTransferPriceInfo> GetAccountTransferPrices()
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.AccountTransferPriceSet
                            join c in entityContext.CustAccountSet on a.AccountNo equals c.AccountNo into cparents
                            from cp in cparents.DefaultIfEmpty()
                            select new AccountTransferPriceInfo()
                            {
                                AccountTransferPrice = a,
                                CustAccount = cp
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
