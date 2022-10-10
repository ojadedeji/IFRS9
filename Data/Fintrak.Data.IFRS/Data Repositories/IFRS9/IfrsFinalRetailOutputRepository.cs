using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IIfrsFinalRetailOutputRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsFinalRetailOutputRepository : DataRepositoryBase<IfrsFinalRetailOutput>, IIfrsFinalRetailOutputRepository
    {
        protected override IfrsFinalRetailOutput AddEntity(IFRSContext entityContext, IfrsFinalRetailOutput entity)
        {
            return entityContext.Set<IfrsFinalRetailOutput>().Add(entity);
        }

        protected override IfrsFinalRetailOutput UpdateEntity(IFRSContext entityContext, IfrsFinalRetailOutput entity)
        {
            return (from e in entityContext.Set<IfrsFinalRetailOutput>()
                    where e.FinalRetailId == entity.FinalRetailId
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<IfrsFinalRetailOutput> GetEntities(IFRSContext entityContext)
        {
            var query = from e in entityContext.Set<IfrsFinalRetailOutput>()
                        select e;
            query = query.OrderBy(a => a.Seq).GroupBy(e => e.Account_No).Select(a => a.FirstOrDefault()).Take(500);
            return query;
        }

        protected override IfrsFinalRetailOutput GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IfrsFinalRetailOutput>()
                         where e.FinalRetailId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }


        public IEnumerable<IfrsFinalRetailOutput> GetEntityByAccountNo(string accountNo)
        {

            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.IfrsFinalRetailOutputSet
                            select a;

                var query1 = query.Where(a => a.Account_No == accountNo);
                var query2 = query.Where(a => a.CustomerName.Contains(accountNo));

                query = query1.Count() > 0 ? query1 : query2;
                return query.ToFullyLoaded();
            }
        }

    }
}