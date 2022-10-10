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
    [Export(typeof(IRegressionweightsRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RegressionweightsRepository : DataRepositoryBase<Regressionweights>, IRegressionweightsRepository
    {
        protected override Regressionweights AddEntity(IFRSContext entityContext, Regressionweights entity)
        {
            return entityContext.Set<Regressionweights>().Add(entity);
        }

        protected override Regressionweights UpdateEntity(IFRSContext entityContext, Regressionweights entity)
        {
            return (from e in entityContext.Set<Regressionweights>()
                    where e.weight_id == entity.weight_id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Regressionweights> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<Regressionweights>()
                   select e;
        }

        protected override Regressionweights GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<Regressionweights>()
                         where e.weight_id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<Regressionweights> GetAvailableRegressionweights(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<Regressionweights>()
                                 select new
                                 {
                                     e.labels,
                                     e.weights,
                                     e.pvalue,
                                     e.se,
                                     e.tstat,
                                     e.Lower_confidence_level,
                                     e.Upper_confidence_level

                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<Regressionweights>().Take(0).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<Regressionweights>().Take(defaultCount)
                                 select e);

                    return query.ToArray();
                }
            }
        }


        public IEnumerable<Regressionweights> GetRegressionweightsBySearch(string searchParam, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (searchParam.Contains("ExportData "))
                {
                    searchParam = searchParam.Replace("ExportData ", "");
                    var query = (from e in entityContext.Set<Regressionweights>()
                                 where searchParam.Contains(e.labels)
                                 select new
                                 {
                                     e.labels,
                                     e.weights,
                                     e.pvalue,
                                     e.se,
                                     e.tstat,
                                     e.Lower_confidence_level,
                                     e.Upper_confidence_level
                                 });

                    if (searchParam.Substring(0, 5) == "split")
                    {
                        searchParam = searchParam.Substring(5, searchParam.Length - 5);
                        var products = (from e in query select new { e.labels }).Distinct();
                        var count = products.Count();
                        var ExportHandler = new ExcelService(path);
                        var labels = count > 0 ? products.ToList().ElementAt(0).labels : "";
                        string response = null;
                        for (int i = 0; i < count; ++i)
                        {
                            labels = products.ToList().ElementAt(i).labels;
                            response = ExportHandler.Export(query.Where(e => e.labels == labels).ToList(), path + labels.Replace("/", ""));
                        }
                    }
                    else
                    {
                        var ExportHandler = new ExcelService(path);
                        string response = ExportHandler.Export(query.ToList(), path);
                    }

                    return new List<Regressionweights>().Take(0).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<Regressionweights>()
                                 where e.labels == searchParam
                                 select e);
                    return query.ToArray();
                }
            }
        }


        public IEnumerable<Regressionweights> GetRegressionweightsByLabels(string LabelsVal)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<Regressionweights>()
                             where e.labels == LabelsVal
                             select e);

                return query.ToArray();
            }
        }

        public IEnumerable<string> GetDistinctLabels()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (entityContext.RegressionweightsSet.Select(r => r.labels)).Distinct();

                return query.ToFullyLoaded();
            }
        }
    }
}