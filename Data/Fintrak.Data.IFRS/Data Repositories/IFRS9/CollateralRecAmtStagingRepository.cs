using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ICollateralRecAmtStagingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CollateralRecAmtStagingRepository : DataRepositoryBase<CollateralRecAmtStaging>, ICollateralRecAmtStagingRepository
    {
        protected override CollateralRecAmtStaging AddEntity(IFRSContext entityContext, CollateralRecAmtStaging entity)
        {
            return entityContext.Set<CollateralRecAmtStaging>().Add(entity);
        }

        protected override CollateralRecAmtStaging UpdateEntity(IFRSContext entityContext, CollateralRecAmtStaging entity)
        {
            return (from e in entityContext.Set<CollateralRecAmtStaging>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CollateralRecAmtStaging> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<CollateralRecAmtStaging>()
                   select e;
        }

        protected override CollateralRecAmtStaging GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CollateralRecAmtStaging>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }


        /////////////////////////////////////////// Methods from IRepo..

        public IEnumerable<CollateralRecAmtStaging> GetCollateralRecAmtStagingBySearch(string searchParam) {
            using (IFRSContext entityContext = new IFRSContext()) {
                var query = (from e in entityContext.Set<CollateralRecAmtStaging>()
                             where e.Refno == searchParam || e.CustomerName == searchParam orderby e.date_pmt                            
                             select e);
                return query.ToArray();
            }
        }

        public IEnumerable<CollateralRecAmtStaging> GetCollateralRecAmtStagings(int defaultCount) {
            using (IFRSContext entityContext = new IFRSContext()) {
                var query = (from e in entityContext.Set<CollateralRecAmtStaging>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                             select e);
                return query.ToArray();
            }
        }


    }
}