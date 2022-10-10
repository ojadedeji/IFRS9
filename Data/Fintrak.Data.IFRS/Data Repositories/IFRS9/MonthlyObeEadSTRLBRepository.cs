using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IMonthlyObeEadSTRLBRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MonthlyObeEadSTRLBRepository : DataRepositoryBase<MonthlyObeEadSTRLB>, IMonthlyObeEadSTRLBRepository
    {
        protected override MonthlyObeEadSTRLB AddEntity(IFRSContext entityContext, MonthlyObeEadSTRLB entity)
        {
            return entityContext.Set<MonthlyObeEadSTRLB>().Add(entity);
        }

        protected override MonthlyObeEadSTRLB UpdateEntity(IFRSContext entityContext, MonthlyObeEadSTRLB entity)
        {
            return (from e in entityContext.Set<MonthlyObeEadSTRLB>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<MonthlyObeEadSTRLB> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<MonthlyObeEadSTRLB>()
                   select e;
        }

        protected override MonthlyObeEadSTRLB GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<MonthlyObeEadSTRLB>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }



        /////////////////////////////////////////// Methods from IRepo..

        public IEnumerable<MonthlyObeEadSTRLB> GetMonthlyObeEadSTRLBBySearch(string searchParam) {
            using (IFRSContext entityContext = new IFRSContext()) {
                var query = (from e in entityContext.Set<MonthlyObeEadSTRLB>()
                             where e.Refno == searchParam               //orderby e.RefNo, e.datepmt                             
                             select e);
                return query.ToArray();
            }
        }

        public IEnumerable<MonthlyObeEadSTRLB> GetMonthlyObeEadSTRLBs(int defaultCount) {
            using (IFRSContext entityContext = new IFRSContext()) {
                var query = (from e in entityContext.Set<MonthlyObeEadSTRLB>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                             select e);
                return query.ToArray();
            }
        }


    }
}