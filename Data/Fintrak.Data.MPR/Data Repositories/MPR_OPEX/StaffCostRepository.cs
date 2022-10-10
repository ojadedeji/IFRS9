using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IStaffCostRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class StaffCostRepository : DataRepositoryBase<StaffCost>, IStaffCostRepository
    {

        protected override StaffCost AddEntity(MPRContext entityContext, StaffCost entity)
        {
            return entityContext.Set<StaffCost>().Add(entity);
        }

        protected override StaffCost UpdateEntity(MPRContext entityContext, StaffCost entity)
        {
            return (from e in entityContext.Set<StaffCost>()
                    where e.StaffCostId == entity.StaffCostId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<StaffCost> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<StaffCost>()
                   select e;
        }

        protected override StaffCost GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<StaffCost>()
                         where e.StaffCostId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
        public IEnumerable<StaffCostInfo> GetStaffCosts()
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.StaffCostSet
                            join b in entityContext.CostCentreSet on a.MISCode equals b.Code into ad
                            from adi in ad.DefaultIfEmpty()
                            //join b in entityContext.CostCentreSet on a.MISCode equals b.Code
                            select new StaffCostInfo()
                            {
                                StaffCost = a,
                                CostCentre = adi
                            };

                return query.ToFullyLoaded();
            }
        }
    }
}
