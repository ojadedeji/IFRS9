using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(ICollateralInformationRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CollateralInformationRepository : DataRepositoryBase<CollateralInformation>, ICollateralInformationRepository
    {

        protected override CollateralInformation AddEntity(BasicContext entityContext, CollateralInformation entity)
        {
            return entityContext.Set<CollateralInformation>().Add(entity);
        }

        protected override CollateralInformation UpdateEntity(BasicContext entityContext, CollateralInformation entity)
        {
            return (from e in entityContext.Set<CollateralInformation>()
                    where e.CollateralInformationId == entity.CollateralInformationId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CollateralInformation> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<CollateralInformation>()
                   select e;
        }

        protected override CollateralInformation GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CollateralInformation>()
                         where e.CollateralInformationId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
        public IEnumerable<CollateralDetailsInfo> GetCollateralDetails()
        {
            using (BasicContext entityContext = new BasicContext())
            {
                var query = from a in entityContext.CollateralTypeSet
                            join b in entityContext.CollateralCategorySet on a.CategoryCode equals b.Code
                            join c in entityContext.CollateralInformationSet on a.CategoryCode equals c.Category
                            select new CollateralDetailsInfo()
                            {
                                CollateralType = a,
                                CollateralCategory = b,
                                CollateralInformation = c
                            };

                return query.ToFullyLoaded();
            }
        }
 
    }
}
