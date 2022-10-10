using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IIfrsCustomerRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsCustomerRepository : DataRepositoryBase<IfrsCustomer>, IIfrsCustomerRepository
    {
        protected override IfrsCustomer AddEntity(IFRSContext entityContext, IfrsCustomer entity)
        {
            return entityContext.Set<IfrsCustomer>().Add(entity);
        }

        protected override IfrsCustomer UpdateEntity(IFRSContext entityContext, IfrsCustomer entity)
        {
            return (from e in entityContext.Set<IfrsCustomer>()
                    where e.CustomerId == entity.CustomerId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IfrsCustomer> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<IfrsCustomer>().Take(10).ToArray()
                   select e;
        }

        protected override IfrsCustomer GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IfrsCustomer>()
                         where e.CustomerId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IfrsCustomer> GetCustomerInfoBySearch(string searchParam)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<IfrsCustomer>()
                             where e.CustomerNo == searchParam || e.CustomerName == searchParam
                             select e);
                return query.ToArray();
            }
        }

        public IEnumerable<IfrsCustomer> GetCustomers(int defaultCount)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<IfrsCustomer>().Take(defaultCount)
                             select e);
                return query.ToArray();
            }
        }
    }
}