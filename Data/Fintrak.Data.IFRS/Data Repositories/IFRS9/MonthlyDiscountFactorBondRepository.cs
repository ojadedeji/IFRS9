using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IMonthlyDiscountFactorBondRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MonthlyDiscountFactorBondRepository : DataRepositoryBase<MonthlyDiscountFactorBond>, IMonthlyDiscountFactorBondRepository
    {
        protected override MonthlyDiscountFactorBond AddEntity(IFRSContext entityContext, MonthlyDiscountFactorBond entity)
        {
            return entityContext.Set<MonthlyDiscountFactorBond>().Add(entity);
        }

        protected override MonthlyDiscountFactorBond UpdateEntity(IFRSContext entityContext, MonthlyDiscountFactorBond entity)
        {
            return (from e in entityContext.Set<MonthlyDiscountFactorBond>()
                    where e.MonthlyDiscountFactorBond_Id == entity.MonthlyDiscountFactorBond_Id
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<MonthlyDiscountFactorBond> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<MonthlyDiscountFactorBond>()
                   select e;
        }

        protected override MonthlyDiscountFactorBond GetEntity(IFRSContext entityContext, int MonthlyDiscountFactorBond_Id)
        {
            var query = (from e in entityContext.Set<MonthlyDiscountFactorBond>()
                         where e.MonthlyDiscountFactorBond_Id == MonthlyDiscountFactorBond_Id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<MonthlyDiscountFactorBond> GetMonthlyDiscountFactorBondByRefNo(string RefNo)
        {
            using(IFRSContext entityContext = new IFRSContext())
            {
            var query = (from e in entityContext.Set<MonthlyDiscountFactorBond>()
                         where e.RefNo.Contains(RefNo)
                         select e);

            return query.ToArray();
            }
        }
       
    }
}