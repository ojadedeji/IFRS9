using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IMarginalPdObeDistrRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MarginalPdObeDistrRepository : DataRepositoryBase<MarginalPdObeDistr>, IMarginalPdObeDistrRepository
    {
        protected override MarginalPdObeDistr AddEntity(IFRSContext entityContext, MarginalPdObeDistr entity)
        {
            return entityContext.Set<MarginalPdObeDistr>().Add(entity);
        }

        protected override MarginalPdObeDistr UpdateEntity(IFRSContext entityContext, MarginalPdObeDistr entity)
        {
            return (from e in entityContext.Set<MarginalPdObeDistr>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<MarginalPdObeDistr> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<MarginalPdObeDistr>().Take(200)   //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                   select e;
        }

        protected override MarginalPdObeDistr GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<MarginalPdObeDistr>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }



/*
        public IEnumerable<MarginalPdObeDistrInfo> GetMarginalPdObeDistrs()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.MarginalPdObeDistrDataSet
                            join b in entityContext.ScheduleTypeSet on a.Schedule_Type equals b.Code
                            select new MarginalPdObeDistrInfo()
                            {
                                MarginalPdObeDistr = a,
                                ScheduleType = b
                            };

                return query.ToFullyLoaded();
            }
        }

*/

        public IEnumerable<MarginalPdObeDistr> GetMarginalPdObeDistrBySearch(string searchParam)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<MarginalPdObeDistr>()
                             where e.Refno == searchParam
                             //orderby e.RefNo, e.datepmt
                             select e);

                return query.ToArray();
            }
        }

        public IEnumerable<MarginalPdObeDistr> GetMarginalPdObeDistrs(int defaultCount)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<MarginalPdObeDistr>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                             select e);

                return query.ToArray();
            }
        }



    }
}