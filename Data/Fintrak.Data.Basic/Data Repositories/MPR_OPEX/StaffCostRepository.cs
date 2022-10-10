using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IStaffCostRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class StaffCostRepository : DataRepositoryBase<StaffCost>, IStaffCostRepository
    {

        protected override StaffCost AddEntity(BasicContext entityContext, StaffCost entity)
        {
            return entityContext.Set<StaffCost>().Add(entity);
        }

        protected override StaffCost UpdateEntity(BasicContext entityContext, StaffCost entity)
        {
            return (from e in entityContext.Set<StaffCost>()
                    where e.StaffCostId == entity.StaffCostId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<StaffCost> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<StaffCost>()
                   select e;
        }

        protected override StaffCost GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<StaffCost>()
                         where e.StaffCostId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
        public IEnumerable<StaffCostInfo> GetStaffCosts()
        {
            using (BasicContext entityContext = new BasicContext())
            {
                var query = from a in entityContext.StaffCostSet
                            join b in entityContext.CostCentreSet on a.MISCode equals b.Code
                            select new StaffCostInfo()
                            {
                                StaffCost = a,
                                CostCentre = b
                            };

                return query.ToFullyLoaded();
            }
        }
    }
}
