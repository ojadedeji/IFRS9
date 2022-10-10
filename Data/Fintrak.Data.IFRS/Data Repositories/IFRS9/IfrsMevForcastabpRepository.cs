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
    [Export(typeof(IIfrsMevForcastabpRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsMevForcastabpRepository : DataRepositoryBase<IfrsMevForcastabp>, IIfrsMevForcastabpRepository
    {
        protected override IfrsMevForcastabp AddEntity(IFRSContext entityContext, IfrsMevForcastabp entity)
        {
            return entityContext.Set<IfrsMevForcastabp>().Add(entity);
        }

        protected override IfrsMevForcastabp UpdateEntity(IFRSContext entityContext, IfrsMevForcastabp entity)
        {
            return (from e in entityContext.Set<IfrsMevForcastabp>()
                    where e.Forecast_id == entity.Forecast_id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IfrsMevForcastabp> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<IfrsMevForcastabp>()
                   select e;
        }

        protected override IfrsMevForcastabp GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IfrsMevForcastabp>()
                         where e.Forecast_id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IfrsMevForcastabp> GetAvailableIfrsMevForcastabp(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<IfrsMevForcastabp>()
                                 select new
                                 {
                                     e.Periodic_date,
                                     e.Period_in_quarter,
                                     e.Factor,
                                     e.Mevid

                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<IfrsMevForcastabp>().Take(0).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsMevForcastabp>().Take(defaultCount)
                                 select e);

                    return query.ToArray();
                }
            }
        }


        public IEnumerable<IfrsMevForcastabp> GetIfrsMevForcastabpBySearch(string searchParam, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (searchParam.Contains("ExportData "))
                {
                    searchParam = searchParam.Replace("ExportData ", "");
                    var query = (from e in entityContext.Set<IfrsMevForcastabp>()
                                 where searchParam.Contains(e.Mevid) || searchParam.Contains(e.Period_in_quarter)
                                 select new
                                 {
                                     e.Periodic_date,
                                     e.Period_in_quarter,
                                     e.Factor,
                                     e.Mevid
                                 });

                    if (searchParam.Substring(0, 5) == "split")
                    {
                        searchParam = searchParam.Substring(5, searchParam.Length - 5);
                        var products = (from e in query select new { e.Mevid }).Distinct();
                        var count = products.Count();
                        var ExportHandler = new ExcelService(path);
                        var product = count > 0 ? products.ToList().ElementAt(0).Mevid : "";
                        string response = null;
                        for (int i = 0; i < count; ++i)
                        {
                            product = products.ToList().ElementAt(i).Mevid;
                            response = ExportHandler.Export(query.Where(e => e.Mevid == product).ToList(), path + product.Replace("/", ""));
                        }
                    }
                    else
                    {
                        var ExportHandler = new ExcelService(path);
                        string response = ExportHandler.Export(query.ToList(), path);
                    }

                    return new List<IfrsMevForcastabp>().Take(0).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsMevForcastabp>()
                                 where e.Mevid == searchParam || e.Period_in_quarter == searchParam
                                 select e);
                    return query.ToArray();
                }
            }
        }


        public IEnumerable<IfrsMevForcastabp> GetIfrsMevForcastabpByProduct(string ProductVal)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<IfrsMevForcastabp>()
                             where e.Mevid == ProductVal || e.Period_in_quarter == ProductVal
                             select e);

                return query.ToArray();
            }
        }

        public IEnumerable<string> GetDistinctProduct()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (entityContext.IfrsMevForcastabpSet.Select(r => r.Mevid)).Distinct();

                return query.ToFullyLoaded();
            }
        }
    }
}
