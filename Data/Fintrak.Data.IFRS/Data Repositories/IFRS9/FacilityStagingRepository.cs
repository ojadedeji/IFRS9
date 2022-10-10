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
    [Export(typeof(IFacilityStagingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FacilityStagingRepository : DataRepositoryBase<FacilityStaging>, IFacilityStagingRepository
    {
        protected override FacilityStaging AddEntity(IFRSContext entityContext, FacilityStaging entity)
        {
            return entityContext.Set<FacilityStaging>().Add(entity);
        }

        protected override FacilityStaging UpdateEntity(IFRSContext entityContext, FacilityStaging entity)
        {
            return (from e in entityContext.Set<FacilityStaging>()
                    where e.facId == entity.facId
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<FacilityStaging> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<FacilityStaging>().Take(200).OrderBy(c => c.FacilityType).ThenByDescending(c => c.ReportDate)
                   select e;
        }

        protected override FacilityStaging GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<FacilityStaging>()
                         where e.facId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<FacilityStaging> GetEntityByParam(string searchParam)
        {

            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.FacilityStagingSet
                            where a.Refno == searchParam || a.CustomerNo == searchParam
                            orderby a.ReportDate descending
                            select a;

                return query.ToFullyLoaded();
            }
        }


        public IEnumerable<FacilityStaging> GetAllFacilityStagings(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<FacilityStaging>()
                                 select new
                                 {
                                     AccountNo = e.Refno,
                                     CustID = e.AccountNo,
                                     HC1 = e.CustomerNo,
                                     HC2 = e.CustomerName,
                                     e.FacilityType,
                                     e.Stage,
                                     e.ReportDate
                                 });
                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<FacilityStaging>();

                    //var query = (from e in entityContext.Set<FacilityStaging>() select e);
                    //var ExportHandler = new ExcelService();
                    //var response = ExportHandler.Export(query.ToList(), path);

                    //return query.Take(defaultCount).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<FacilityStaging>().Take(defaultCount) select e);

                    return query.ToArray();
                }
            }
        }

    }
}