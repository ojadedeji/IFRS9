using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ICollateralTypeRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CollateralTypeRepository : DataRepositoryBase<CollateralType>, ICollateralTypeRepository
    {

        protected override CollateralType AddEntity(IFRSContext entityContext, CollateralType entity)
        {
            return entityContext.Set<CollateralType>().Add(entity);
        }

        protected override CollateralType UpdateEntity(IFRSContext entityContext, CollateralType entity)
        {
            return (from e in entityContext.Set<CollateralType>()
                    where e.CollateralTypeId == entity.CollateralTypeId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CollateralType> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<CollateralType>()
                   select e;
        }

        protected override CollateralType GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CollateralType>()
                         where e.CollateralTypeId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<CollateralTypeInfo> GetCollateralTypes()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.CollateralTypeSet
                            join b in entityContext.CollateralCategorySet on a.CategoryCode equals b.Code
                            select new CollateralTypeInfo()
                            {
                                CollateralType = a,
                                CollateralCategory  = b
                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<CollateralTypeInfo> GetCollateralTypes(string categoryCode)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.CollateralTypeSet
                            join b in entityContext.CollateralCategorySet on a.CategoryCode equals b.Code
                            where a.CategoryCode == categoryCode
                            select new CollateralTypeInfo()
                            {
                                CollateralType = a,
                                CollateralCategory = b
                            };

                return query.ToFullyLoaded();
            }
        }

    }
}