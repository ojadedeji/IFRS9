using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ILoanPryMoratoriumRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LoanPryMoratoriumRepository : DataRepositoryBase<LoanPryMoratorium>, ILoanPryMoratoriumRepository
    {
        protected override LoanPryMoratorium AddEntity(IFRSContext entityContext, LoanPryMoratorium entity)
        {
            return entityContext.Set<LoanPryMoratorium>().Add(entity);
        }

        protected override LoanPryMoratorium UpdateEntity(IFRSContext entityContext, LoanPryMoratorium entity)
        {
            return (from e in entityContext.Set<LoanPryMoratorium>()
                    where e.LoanPryMoratoriumId == entity.LoanPryMoratoriumId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<LoanPryMoratorium> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<LoanPryMoratorium>()
                   select e;
        }

        protected override LoanPryMoratorium GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<LoanPryMoratorium>()
                         where e.LoanPryMoratoriumId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        //public IEnumerable<LoanPryMoratoriumInfo> GetLoanPryMoratoriums()
        //{
        //    using (IFRSContext entityContext = new IFRSContext())
        //    {
        //        var query = from a in entityContext.LoanPryMoratoriumDataSet;
        //                    //join b in entityContext.ScheduleTypeSet on a.Schedule_Type equals b.Code
        //                    select new LoanPryMoratoriumInfo()
        //                    {
        //                        LoanPryMoratorium = a
        //                        //ScheduleType = b
        //                    };

        //        return query.ToFullyLoaded();
        //    }
        //}
    }
}