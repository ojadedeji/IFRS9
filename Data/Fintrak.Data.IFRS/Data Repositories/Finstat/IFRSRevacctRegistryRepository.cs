using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IIFRSRevacctRegistryRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IFRSRevacctRegistryRepository : DataRepositoryBase<IFRSRevacctRegistry>, IIFRSRevacctRegistryRepository
    {

        protected override IFRSRevacctRegistry AddEntity(IFRSContext entityContext, IFRSRevacctRegistry entity)
        {
            return entityContext.Set<IFRSRevacctRegistry>().Add(entity);
        }

        protected override IFRSRevacctRegistry UpdateEntity(IFRSContext entityContext, IFRSRevacctRegistry entity)
        {
            return (from e in entityContext.Set<IFRSRevacctRegistry>() 
                    where e.RevenueId == entity.RevenueId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IFRSRevacctRegistry> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<IFRSRevacctRegistry>()
                   select e;
        }

        protected override IFRSRevacctRegistry GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IFRSRevacctRegistry>()
                         where e.RevenueId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IFRSRevacctRegistryInfo> GetIFRSRevacctRegistries()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.IFRSRevacctRegistrySet
                            join b in entityContext.FinancialTypeSet on a.FinType equals b.Code
                            join c in entityContext.FinancialTypeSet on a.FinSubType equals c.Code
                            join d in entityContext.IFRSRevacctRegistrySet on a.ParentId equals d.RevenueId into parents
                            from pt in parents.DefaultIfEmpty()
                            select new IFRSRevacctRegistryInfo()
                            {
                                IFRSRevacctRegistry = a,
                                FinType = b,
                                FinSubType = c,
                                Parent = pt
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
