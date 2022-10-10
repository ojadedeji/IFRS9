using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ICollateralGrowthRateRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CollateralGrowthRateRepository : DataRepositoryBase<CollateralGrowthRate>, ICollateralGrowthRateRepository
    {
        protected override CollateralGrowthRate AddEntity(IFRSContext entityContext, CollateralGrowthRate entity)
        {
            return entityContext.Set<CollateralGrowthRate>().Add(entity);
        }

        protected override CollateralGrowthRate UpdateEntity(IFRSContext entityContext, CollateralGrowthRate entity)
        {
            return (from e in entityContext.Set<CollateralGrowthRate>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CollateralGrowthRate> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<CollateralGrowthRate>()
                   select e;
        }

        protected override CollateralGrowthRate GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CollateralGrowthRate>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }



        /////////////////////////////////////////// Methods from IRepo..

        public IEnumerable<CollateralGrowthRate> GetCollateralGrowthRateBySearch(string searchParam) {
            using (IFRSContext entityContext = new IFRSContext()) {
                var query = (from e in entityContext.Set<CollateralGrowthRate>()
                             where e.TypeCode == searchParam
                             orderby e.seq                         
                             select e);
                return query.ToArray();
            }
        }

        public IEnumerable<CollateralGrowthRate> GetCollateralGrowthRates(int defaultCount) {
            using (IFRSContext entityContext = new IFRSContext()) {
                var query = (from e in entityContext.Set<CollateralGrowthRate>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                             select e);
                return query.ToArray();
            }
        }


    }
}