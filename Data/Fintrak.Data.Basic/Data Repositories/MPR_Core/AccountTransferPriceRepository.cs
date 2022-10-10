using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;
using Fintrak.Shared.Basic.Framework;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IAccountTransferPriceRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AccountTransferPriceRepository : DataRepositoryBase<AccountTransferPrice>, IAccountTransferPriceRepository
    {

        protected override AccountTransferPrice AddEntity(BasicContext entityContext, AccountTransferPrice entity)
        {
            return entityContext.Set<AccountTransferPrice>().Add(entity);
        }

        protected override AccountTransferPrice UpdateEntity(BasicContext entityContext, AccountTransferPrice entity)
        {
            return (from e in entityContext.Set<AccountTransferPrice>() 
                    where e.AccountTransferPriceId == entity.AccountTransferPriceId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<AccountTransferPrice> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<AccountTransferPrice>()
                   select e;
        }

        protected override AccountTransferPrice GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<AccountTransferPrice>()
                         where e.AccountTransferPriceId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<AccountTransferPriceInfo> GetAccountTransferPrices()
        {
            using (BasicContext entityContext = new BasicContext())
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
