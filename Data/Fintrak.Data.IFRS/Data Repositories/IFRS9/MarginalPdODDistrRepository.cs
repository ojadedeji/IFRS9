using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IMarginalPdODDistrRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MarginalPdODDistrRepository : DataRepositoryBase<MarginalPdODDistr>, IMarginalPdODDistrRepository
    {
        protected override MarginalPdODDistr AddEntity(IFRSContext entityContext, MarginalPdODDistr entity)
        {
            return entityContext.Set<MarginalPdODDistr>().Add(entity);
        }

        protected override MarginalPdODDistr UpdateEntity(IFRSContext entityContext, MarginalPdODDistr entity)
        {
            return (from e in entityContext.Set<MarginalPdODDistr>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<MarginalPdODDistr> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<MarginalPdODDistr>().Take(200)   //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                   select e;
        }

        protected override MarginalPdODDistr GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<MarginalPdODDistr>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }



        /*
                public IEnumerable<MarginalPdODDistrInfo> GetMarginalPdODDistrs()
                {
                    using (IFRSContext entityContext = new IFRSContext())
                    {
                        var query = from a in entityContext.MarginalPdODDistrDataSet
                                    join b in entityContext.ScheduleTypeSet on a.Schedule_Type equals b.Code
                                    select new MarginalPdODDistrInfo()
                                    {
                                        MarginalPdODDistr = a,
                                        ScheduleType = b
                                    };

                        return query.ToFullyLoaded();
                    }
                }

        */

        public IEnumerable<MarginalPdODDistr> GetMarginalPdODDistrBySearch(string searchParam)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<MarginalPdODDistr>()
                             where e.Refno == searchParam
                             //orderby e.RefNo, e.datepmt
                             select e);

                return query.ToArray();
            }
        }

        public IEnumerable<MarginalPdODDistr> GetMarginalPdODDistrs(int defaultCount)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<MarginalPdODDistr>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                             select e);

                return query.ToArray();
            }
        }



    }
}