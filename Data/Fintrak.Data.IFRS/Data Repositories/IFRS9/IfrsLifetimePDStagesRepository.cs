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
    [Export(typeof(IIfrsLifetimePDStagesRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsLifetimePDStagesRepository : DataRepositoryBase<IfrsLifetimePDStages>, IIfrsLifetimePDStagesRepository
    {
        protected override IfrsLifetimePDStages AddEntity(IFRSContext entityContext, IfrsLifetimePDStages entity)
        {
            return entityContext.Set<IfrsLifetimePDStages>().Add(entity);
        }

        protected override IfrsLifetimePDStages UpdateEntity(IFRSContext entityContext, IfrsLifetimePDStages entity)
        {
            return (from e in entityContext.Set<IfrsLifetimePDStages>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IfrsLifetimePDStages> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<IfrsLifetimePDStages>()
                   select e;
        }

        protected override IfrsLifetimePDStages GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IfrsLifetimePDStages>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        //public IEnumerable<IfrsLifetimePDStages> GetIfrsLifetimePDStagesBySearch(string searchParam, string path)
        //{
        //    using (IFRSContext entityContext = new IFRSContext())
        //    {
        //        if (searchParam.Contains("ExportData "))
        //        {
        //            searchParam = searchParam.Replace("ExportData ", "");
        //            var query = (from e in entityContext.Set<IfrsLifetimePDStages>()
        //                         where searchParam.Contains(e.Stage)
        //                         select new
        //                         {
        //                             e.Stage,
        //                             e.Month,
        //                             e.Date_pmt,
        //                             e.CumulativeSP,
        //                             e.MarginalSP,
        //                             e.MarginalDR,
        //                             e.MarginalDRBest,
        //                             e.MarginalDROptimistic,
        //                             e.MarginalDRDownturn,
        //                             e.CummPDBest,
        //                             e.CummPDOptimistic,
        //                             e.CummPDDownturn,
        //                             e.LifetimePDBest,
        //                             e.LifetimePDOptimistic,
        //                             e.LifetimePDDownturn
        //                         });

        //            if (searchParam.Substring(0, 5) == "split")
        //            {
        //                searchParam = searchParam.Substring(5, searchParam.Length - 5);
        //                var products = (from e in query select new { e.labels }).Distinct();
        //                var count = products.Count();
        //                var ExportHandler = new ExcelService(path);
        //                var labels = count > 0 ? products.ToList().ElementAt(0).labels : "";
        //                string response = null;
        //                for (int i = 0; i < count; ++i)
        //                {
        //                    labels = products.ToList().ElementAt(i).labels;
        //                    response = ExportHandler.Export(query.Where(e => e.labels == labels).ToList(), path + labels.Replace("/", ""));
        //                }
        //            }
        //            else
        //            {
        //                var ExportHandler = new ExcelService(path);
        //                string response = ExportHandler.Export(query.ToList(), path);
        //            }

        //            return new List<IfrsLifetimePDStages>().Take(0).ToArray();
        //        }
        //        else
        //        {
        //            var query = (from e in entityContext.Set<IfrsLifetimePDStages>()
        //                         where e.Stage == searchParam
        //                         select e);
        //            return query.ToArray();
        //        }
        //    }
        //}

        public IEnumerable<IfrsLifetimePDStages> ExportIfrsLifetimePDStages(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<IfrsLifetimePDStages>()
                                 select new
                                 {
                                     e.Stage,
                                     e.Month,
                                     e.Date_pmt,
                                     e.CumulativeSP,
                                     e.MarginalSP,
                                     e.MarginalDR,
                                     e.MarginalDRBest,
                                     e.MarginalDROptimistic,
                                     e.MarginalDRDownturn,
                                     e.CummPDBest,
                                     e.CummPDOptimistic,
                                     e.CummPDDownturn,
                                     e.LifetimePDBest,
                                     e.LifetimePDOptimistic,
                                     e.LifetimePDDownturn
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<IfrsLifetimePDStages>().Take(0).ToArray().OrderBy(c => c.Stage).ThenBy(c => c.Date_pmt);

                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsLifetimePDStages>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                                 select e);

                    return query.ToArray();
                }
            }
        }

    }
}