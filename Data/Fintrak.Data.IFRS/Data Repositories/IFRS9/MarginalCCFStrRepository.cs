using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IMarginalCCFStrRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MarginalCCFStrRepository : DataRepositoryBase<MarginalCCFStr>, IMarginalCCFStrRepository
    {
        protected override MarginalCCFStr AddEntity(IFRSContext entityContext, MarginalCCFStr entity)
        {
            return entityContext.Set<MarginalCCFStr>().Add(entity);
        }

        protected override MarginalCCFStr UpdateEntity(IFRSContext entityContext, MarginalCCFStr entity)
        {
            return (from e in entityContext.Set<MarginalCCFStr>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<MarginalCCFStr> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<MarginalCCFStr>().Take(200).OrderBy(c => c.OBEType) //.ThenBy(c => c.datepmt)
                   select e;
        }

        protected override MarginalCCFStr GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<MarginalCCFStr>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }



/*
        public IEnumerable<MarginalCCFStrInfo> GetMarginalCCFStrs()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.MarginalCCFStrDataSet
                            join b in entityContext.ScheduleTypeSet on a.Schedule_Type equals b.Code
                            select new MarginalCCFStrInfo()
                            {
                                MarginalCCFStr = a,
                                ScheduleType = b
                            };

                return query.ToFullyLoaded();
            }
        }

*/


        public IEnumerable<MarginalCCFStr> GetMarginalCCFStrBySearch(string searchParam)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<MarginalCCFStr>()
                             where e.OBEType == searchParam
                             orderby e.seq //, e.datepmt
                             select e);

                return query.ToArray();
            }
        }

        public IEnumerable<MarginalCCFStr> GetMarginalCCFStrs(int defaultCount)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<MarginalCCFStr>()
                             .Take(defaultCount)
                             .OrderBy(c => c.OBEType)
                            // .ThenBy(c => c.datepmt)
                             select e);

                return query.ToArray();
            }
        }



    }
}