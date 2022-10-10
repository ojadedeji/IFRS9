using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ICollateralRealizationPeriodRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CollateralRealizationPeriodRepository : DataRepositoryBase<CollateralRealizationPeriod>, ICollateralRealizationPeriodRepository
    {

        protected override CollateralRealizationPeriod AddEntity(IFRSContext entityContext, CollateralRealizationPeriod entity)
        {
            return entityContext.Set<CollateralRealizationPeriod>().Add(entity);
        }

        protected override CollateralRealizationPeriod UpdateEntity(IFRSContext entityContext, CollateralRealizationPeriod entity)
        {
            return (from e in entityContext.Set<CollateralRealizationPeriod>()
                    where e.CollateralRealizationPeriodId == entity.CollateralRealizationPeriodId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CollateralRealizationPeriod> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<CollateralRealizationPeriod>()
                   select e;
        }

        protected override CollateralRealizationPeriod GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CollateralRealizationPeriod>()
                         where e.CollateralRealizationPeriodId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<CollateralRealizationPeriodInfo> GetCollateralRealizationPeriods()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.CollateralRealizationPeriodSet
                            join b in entityContext.CollateralTypeSet on a.TypeCode equals b.Code
                            select new CollateralRealizationPeriodInfo()
                            {
                                CollateralRealizationPeriod = a,
                                CollateralType  = b
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
