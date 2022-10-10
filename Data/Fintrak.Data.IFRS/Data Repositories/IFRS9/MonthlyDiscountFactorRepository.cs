using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IMonthlyDiscountFactorRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MonthlyDiscountFactorRepository : DataRepositoryBase<MonthlyDiscountFactor>, IMonthlyDiscountFactorRepository
    {
        protected override MonthlyDiscountFactor AddEntity(IFRSContext entityContext, MonthlyDiscountFactor entity)
        {
            return entityContext.Set<MonthlyDiscountFactor>().Add(entity);
        }

        protected override MonthlyDiscountFactor UpdateEntity(IFRSContext entityContext, MonthlyDiscountFactor entity)
        {
            return (from e in entityContext.Set<MonthlyDiscountFactor>()
                    where e.MonthlyDiscountFactor_Id == entity.MonthlyDiscountFactor_Id
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<MonthlyDiscountFactor> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<MonthlyDiscountFactor>()
                   select e;
        }

        protected override MonthlyDiscountFactor GetEntity(IFRSContext entityContext, int MonthlyDiscountFactor_Id)
        {
            var query = (from e in entityContext.Set<MonthlyDiscountFactor>()
                         where e.MonthlyDiscountFactor_Id == MonthlyDiscountFactor_Id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<MonthlyDiscountFactor> GetMonthlyDiscountFactorByRefNo(string RefNo)
        {
            using(IFRSContext entityContext = new IFRSContext())
            {
            var query = (from e in entityContext.Set<MonthlyDiscountFactor>()
                         where e.RefNo.Contains(RefNo)
                         select e);

            return query.ToArray();
            }
        }
       
    }
}