using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IGLTypeRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GLTypeRepository : DataRepositoryBase<GLType>, IGLTypeRepository
    {

        protected override GLType AddEntity(IFRSContext entityContext, GLType entity)
        {
            return entityContext.Set<GLType>().Add(entity);
        }

        protected override GLType UpdateEntity(IFRSContext entityContext, GLType entity)
        {
            return (from e in entityContext.Set<GLType>()
                    where e.GLTypeId == entity.GLTypeId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<GLType> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<GLType>()
                   select e;
        }

        protected override GLType GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<GLType>()
                         where e.GLTypeId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
        public IEnumerable<IFRSRegistryInfo> GetIFRSRegistrys()
        {
            using (IFRSContext entityContext = new IFRSContext())
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
