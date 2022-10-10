using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ICollateralInputRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CollateralInputRepository : DataRepositoryBase<CollateralInput>, ICollateralInputRepository
    {
        protected override CollateralInput AddEntity(IFRSContext entityContext, CollateralInput entity)
        {
            return entityContext.Set<CollateralInput>().Add(entity);
        }

        protected override CollateralInput UpdateEntity(IFRSContext entityContext, CollateralInput entity)
        {
            return (from e in entityContext.Set<CollateralInput>()
                    where e.Collateral_Id == entity.Collateral_Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CollateralInput> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<CollateralInput>()
                   select e;
        }

        protected override CollateralInput GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CollateralInput>()
                         where e.Collateral_Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}