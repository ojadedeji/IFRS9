using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(ICollateralRealizationPeriodRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CollateralRealizationPeriodRepository : DataRepositoryBase<CollateralRealizationPeriod>, ICollateralRealizationPeriodRepository
    {

        protected override CollateralRealizationPeriod AddEntity(BasicContext entityContext, CollateralRealizationPeriod entity)
        {
            return entityContext.Set<CollateralRealizationPeriod>().Add(entity);
        }

        protected override CollateralRealizationPeriod UpdateEntity(BasicContext entityContext, CollateralRealizationPeriod entity)
        {
            return (from e in entityContext.Set<CollateralRealizationPeriod>()
                    where e.CollateralRealizationPeriodId == entity.CollateralRealizationPeriodId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CollateralRealizationPeriod> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<CollateralRealizationPeriod>()
                   select e;
        }

        protected override CollateralRealizationPeriod GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CollateralRealizationPeriod>()
                         where e.CollateralRealizationPeriodId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<CollateralRealizationPeriodInfo> GetCollateralRealizationPeriods()
        {
            using (BasicContext entityContext = new BasicContext())
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
