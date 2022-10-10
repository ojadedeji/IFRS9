using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ICashflowRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CashflowRepository : DataRepositoryBase<Cashflow>, ICashflowRepository
    {
        protected override Cashflow AddEntity(IFRSContext entityContext, Cashflow entity)
        {
            return entityContext.Set<Cashflow>().Add(entity);
        }

        protected override Cashflow UpdateEntity(IFRSContext entityContext, Cashflow entity)
        {
            return (from e in entityContext.Set<Cashflow>()
                    where e.CashflowId == entity.CashflowId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Cashflow> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<Cashflow>().Take(200).OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                   select e;
        }

        protected override Cashflow GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<Cashflow>()
                         where e.CashflowId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }



/*
        public IEnumerable<CashflowInfo> GetCashflows()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.CashflowDataSet
                            join b in entityContext.ScheduleTypeSet on a.Schedule_Type equals b.Code
                            select new CashflowInfo()
                            {
                                Cashflow = a,
                                ScheduleType = b
                            };

                return query.ToFullyLoaded();
            }
        }

*/


        public IEnumerable<Cashflow> GetCashflowBySearch(string searchParam)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<Cashflow>()
                             where e.RefNo == searchParam
                             orderby e.RefNo, e.datepmt
                             select e);

                return query.ToArray();
            }
        }

        public IEnumerable<Cashflow> GetCashflows(int defaultCount)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<Cashflow>().Take(defaultCount).OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                             select e);

                return query.ToArray();
            }
        }



    }
}