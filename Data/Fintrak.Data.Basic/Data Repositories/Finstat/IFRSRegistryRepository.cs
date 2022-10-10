using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IIFRSRegistryRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IFRSRegistryRepository : DataRepositoryBase<IFRSRegistry>, IIFRSRegistryRepository
    {

        protected override IFRSRegistry AddEntity(BasicContext entityContext, IFRSRegistry entity)
        {
            return entityContext.Set<IFRSRegistry>().Add(entity);
        }

        protected override IFRSRegistry UpdateEntity(BasicContext entityContext, IFRSRegistry entity)
        {
            return (from e in entityContext.Set<IFRSRegistry>() 
                    where e.RegistryId == entity.RegistryId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IFRSRegistry> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<IFRSRegistry>()
                   select e;
        }

        protected override IFRSRegistry GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IFRSRegistry>()
                         where e.RegistryId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IFRSRegistryInfo> GetIFRSRegistrys()
        {
            using (BasicContext entityContext = new BasicContext())
            {
                var query = from a in entityContext.IFRSRegistrySet
                            join b in entityContext.FinancialTypeSet on a.FinType equals b.Code
                            join c in entityContext.FinancialTypeSet on a.FinSubType equals c.Code
                            join d in entityContext.IFRSRegistrySet on a.ParentId equals d.RegistryId into parents
                            from pt in parents.DefaultIfEmpty()
                            select new IFRSRegistryInfo()
                            {
                               IFRSRegistry = a,
                               FinType = b,
                               FinSubType = c,
                               Parent = pt
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
