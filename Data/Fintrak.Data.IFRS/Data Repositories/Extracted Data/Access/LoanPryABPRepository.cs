using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ILoanPryABPRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LoanPryABPRepository : DataRepositoryBase<LoanPryABP>, ILoanPryABPRepository
    {
        protected override LoanPryABP AddEntity(IFRSContext entityContext, LoanPryABP entity)
        {
            return entityContext.Set<LoanPryABP>().Add(entity);
        }

        protected override LoanPryABP UpdateEntity(IFRSContext entityContext, LoanPryABP entity)
        {
            return (from e in entityContext.Set<LoanPryABP>()
                    where e.PryId == entity.PryId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<LoanPryABP> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<LoanPryABP>()
                   select e;
        }

        protected override LoanPryABP GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<LoanPryABP>()
                         where e.PryId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<LoanPryABPInfo> GetLoanPryABPs()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.LoanPryABPDataSet
                            join b in entityContext.ScheduleTypeSet on a.Schedule_Type equals b.Code
                            select new LoanPryABPInfo()
                            {
                                LoanPryABP = a,
                                ScheduleType = b
                            };

                return query.ToFullyLoaded();
            }
        }
    }
}