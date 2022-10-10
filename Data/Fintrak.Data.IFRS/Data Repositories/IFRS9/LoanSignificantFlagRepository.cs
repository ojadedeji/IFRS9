using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ILoanSignificantFlagRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LoanSignificantFlagRepository : DataRepositoryBase<LoanSignificantFlag>, ILoanSignificantFlagRepository {
        protected override LoanSignificantFlag AddEntity(IFRSContext entityContext, LoanSignificantFlag entity)
        {
            return entityContext.Set<LoanSignificantFlag>().Add(entity);
        }

        protected override LoanSignificantFlag UpdateEntity(IFRSContext entityContext, LoanSignificantFlag entity)
        {
            return (from e in entityContext.Set<LoanSignificantFlag>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<LoanSignificantFlag> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<LoanSignificantFlag>().Take(200)     //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                   select e;
        }

        protected override LoanSignificantFlag GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<LoanSignificantFlag>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }



        /*
                public IEnumerable<LoanSignificantFlagInfo> GetLoanSignificantFlags()
                {
                    using (IFRSContext entityContext = new IFRSContext())
                    {
                        var query = from a in entityContext.LoanSignificantFlagDataSet
                                    join b in entityContext.ScheduleTypeSet on a.Schedule_Type equals b.Code
                                    select new LoanSignificantFlagInfo()
                                    {
                                        LoanSignificantFlag = a,
                                        ScheduleType = b
                                    };

                        return query.ToFullyLoaded();
                    }
                }

        */


        public IEnumerable<LoanSignificantFlag> GetLoanSignificantFlagBySearch(string searchParam)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<LoanSignificantFlag>()
                             where e.ProductType == searchParam
                            // orderby e.ProductType, e.datepmt
                             select e);

                return query.ToArray();
            }
        }

        public IEnumerable<LoanSignificantFlag> GetLoanSignificantFlags(int defaultCount)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<LoanSignificantFlag>().Take(defaultCount) 
                             select e);
                return query.ToArray();
            }
        }



    }
}