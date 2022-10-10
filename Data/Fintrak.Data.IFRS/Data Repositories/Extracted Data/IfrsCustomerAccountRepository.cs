using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IIfrsCustomerAccountRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsCustomerAccountRepository : DataRepositoryBase<IfrsCustomerAccount>, IIfrsCustomerAccountRepository
    {
        protected override IfrsCustomerAccount AddEntity(IFRSContext entityContext, IfrsCustomerAccount entity)
        {
            return entityContext.Set<IfrsCustomerAccount>().Add(entity);
        }

        protected override IfrsCustomerAccount UpdateEntity(IFRSContext entityContext, IfrsCustomerAccount entity)
        {
            return (from e in entityContext.Set<IfrsCustomerAccount>()
                    where e.CustAccountId == entity.CustAccountId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IfrsCustomerAccount> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<IfrsCustomerAccount>()
                   select e;
        }

        protected override IfrsCustomerAccount GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IfrsCustomerAccount>()
                         where e.CustAccountId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
        public IEnumerable<string> GetDistinctSector()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (entityContext.IfrsCustomerAccountSet.Select<IfrsCustomerAccount, string>(r => r.Sector)).Distinct();

                return query.ToFullyLoaded();
            }
        }
    }
}