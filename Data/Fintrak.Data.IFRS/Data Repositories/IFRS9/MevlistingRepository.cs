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
    [Export(typeof(IMevlistingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MevlistingRepository : DataRepositoryBase<Mevlisting>, IMevlistingRepository
    {
        protected override Mevlisting AddEntity(IFRSContext entityContext, Mevlisting entity)
        {
            return entityContext.Set<Mevlisting>().Add(entity);
        }

        protected override Mevlisting UpdateEntity(IFRSContext entityContext, Mevlisting entity)
        {
            return (from e in entityContext.Set<Mevlisting>()
                    where e.listing_id == entity.listing_id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Mevlisting> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<Mevlisting>()
                   select e;
        }

        protected override Mevlisting GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<Mevlisting>()
                         where e.listing_id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<Mevlisting> GetAvailableMevlisting(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<Mevlisting>()
                                 select new
                                 {
                                     e.mev,
                                     e.mev_code,
                                     e.probability_weighted,
                                     e.Dependent_variable,
                                     e.future_opt_sign,
                                     e.future_down_sign

                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<Mevlisting>().Take(0).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<Mevlisting>().Take(defaultCount)
                                 select e);

                    return query.ToArray();
                }
            }
        }


        public IEnumerable<Mevlisting> GetMevlistingBySearch(string searchParam, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (searchParam.Contains("ExportData "))
                {
                    searchParam = searchParam.Replace("ExportData ", "");
                    var query = (from e in entityContext.Set<Mevlisting>()
                                 where searchParam.Contains(e.mev_code)
                                 select new
                                 {
                                     e.mev,
                                     e.mev_code,
                                     e.probability_weighted,
                                     e.Dependent_variable,
                                     e.future_opt_sign,
                                     e.future_down_sign
                                 });

                    if (searchParam.Substring(0, 5) == "split")
                    {
                        searchParam = searchParam.Substring(5, searchParam.Length - 5);
                        var products = (from e in query select new { e.mev_code }).Distinct();
                        var count = products.Count();
                        var ExportHandler = new ExcelService(path);
                        var producttype = count > 0 ? products.ToList().ElementAt(0).mev_code : "";
                        string response = null;
                        for (int i = 0; i < count; ++i)
                        {
                            producttype = products.ToList().ElementAt(i).mev_code;
                            response = ExportHandler.Export(query.Where(e => e.mev_code == producttype).ToList(), path + producttype.Replace("/", ""));
                        }
                    }
                    else
                    {
                        var ExportHandler = new ExcelService(path);
                        string response = ExportHandler.Export(query.ToList(), path);
                    }

                    return new List<Mevlisting>().Take(0).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<Mevlisting>()
                                 where e.mev_code == searchParam
                                 select e);
                    return query.ToArray();
                }
            }
        }


        public IEnumerable<Mevlisting> GetMevlistingByMev(string mevVal)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<Mevlisting>()
                             where e.mev == mevVal
                             select e);

                return query.ToArray();
            }
        }

        public IEnumerable<string> GetDistinctMevs()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (entityContext.MevlistingSet.Select(r => r.mev)).Distinct();

                return query.ToFullyLoaded();
            }
        }
    }
}