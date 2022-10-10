using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;
using Fintrak.Shared.Common.Services;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IMarginalPddSTRLBRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MarginalPddSTRLBRepository : DataRepositoryBase<MarginalPddSTRLB>, IMarginalPddSTRLBRepository
    {
        protected override MarginalPddSTRLB AddEntity(IFRSContext entityContext, MarginalPddSTRLB entity)
        {
            return entityContext.Set<MarginalPddSTRLB>().Add(entity);
        }

        protected override MarginalPddSTRLB UpdateEntity(IFRSContext entityContext, MarginalPddSTRLB entity)
        {
            return (from e in entityContext.Set<MarginalPddSTRLB>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<MarginalPddSTRLB> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<MarginalPddSTRLB>().Take(200)   //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                   select e;
        }

        protected override MarginalPddSTRLB GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<MarginalPddSTRLB>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }



/*
        public IEnumerable<MarginalPddSTRLBInfo> GetMarginalPddSTRLBs()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.MarginalPddSTRLBDataSet
                            join b in entityContext.ScheduleTypeSet on a.Schedule_Type equals b.Code
                            select new MarginalPddSTRLBInfo()
                            {
                                MarginalPddSTRLB = a,
                                ScheduleType = b
                            };

                return query.ToFullyLoaded();
            }
        }

*/

        public IEnumerable<MarginalPddSTRLB> GetMarginalPddSTRLBBySearch(string searchParam)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<MarginalPddSTRLB>()
                             where e.Refno == searchParam
                             //orderby e.RefNo, e.datepmt
                             select e);

                return query.ToArray();
            }
        }

        public IEnumerable<MarginalPddSTRLB> GetMarginalPddSTRLBs(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<MarginalPddSTRLB>() select e);
                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return query.Take(defaultCount).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<MarginalPddSTRLB>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                                 select e);

                    return query.ToArray();
                }
            }
        }



    }
}