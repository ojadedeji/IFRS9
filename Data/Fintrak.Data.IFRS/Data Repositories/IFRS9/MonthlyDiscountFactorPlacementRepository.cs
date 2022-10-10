using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IMonthlyDiscountFactorPlacementRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MonthlyDiscountFactorPlacementRepository : DataRepositoryBase<MonthlyDiscountFactorPlacement>, IMonthlyDiscountFactorPlacementRepository
    {
        protected override MonthlyDiscountFactorPlacement AddEntity(IFRSContext entityContext, MonthlyDiscountFactorPlacement entity)
        {
            return entityContext.Set<MonthlyDiscountFactorPlacement>().Add(entity);
        }

        protected override MonthlyDiscountFactorPlacement UpdateEntity(IFRSContext entityContext, MonthlyDiscountFactorPlacement entity)
        {
            return (from e in entityContext.Set<MonthlyDiscountFactorPlacement>()
                    where e.MonthlyDiscountFactorPlacement_Id == entity.MonthlyDiscountFactorPlacement_Id
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<MonthlyDiscountFactorPlacement> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<MonthlyDiscountFactorPlacement>()
                   select e;
        }

        protected override MonthlyDiscountFactorPlacement GetEntity(IFRSContext entityContext, int MonthlyDiscountFactorPlacement_Id)
        {
            var query = (from e in entityContext.Set<MonthlyDiscountFactorPlacement>()
                         where e.MonthlyDiscountFactorPlacement_Id == MonthlyDiscountFactorPlacement_Id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<MonthlyDiscountFactorPlacement> GetMonthlyDiscountFactorPlacementByRefNo(string RefNo)
        {
            using(IFRSContext entityContext = new IFRSContext())
            {
            var query = (from e in entityContext.Set<MonthlyDiscountFactorPlacement>()
                         where e.RefNo.Contains(RefNo)
                         select e);

            return query.ToArray();
            }
        }
       
    }
}