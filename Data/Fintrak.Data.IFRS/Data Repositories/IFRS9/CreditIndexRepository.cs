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
    [Export(typeof(ICreditIndexRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CreditIndexRepository : DataRepositoryBase<CreditIndex>, ICreditIndexRepository
    {
        protected override CreditIndex AddEntity(IFRSContext entityContext, CreditIndex entity)
        {
            return entityContext.Set<CreditIndex>().Add(entity);
        }

        protected override CreditIndex UpdateEntity(IFRSContext entityContext, CreditIndex entity)
        {
            return (from e in entityContext.Set<CreditIndex>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CreditIndex> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<CreditIndex>()
                   select e;
        }

        protected override CreditIndex GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CreditIndex>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<CreditIndex> GetAvailableCreditIndex(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<CreditIndex>()
                                 select new
                                 {
                                     e.foreacst_date,
                                     e.creditindex,
                                     e.scenerio,
                                     e.forcast_date
                                  
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<CreditIndex>().Take(0).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<CreditIndex>().Take(defaultCount) 
                                 select e);

                    return query.ToArray();
                }
            }
        }


        public IEnumerable<CreditIndex> GetCreditIndexBySearch(string searchParam, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (searchParam.Contains("ExportData "))
                {
                    searchParam = searchParam.Replace("ExportData ", "");
                    var query = (from e in entityContext.Set<CreditIndex>()
                                 where searchParam.Contains(e.scenerio)
                                 select new
                                 {
                                     e.foreacst_date,
                                     e.creditindex,
                                     e.scenerio,
                                     e.forcast_date
                                 });

                    if (searchParam.Substring(0, 5) == "split")
                    {
                        searchParam = searchParam.Substring(5, searchParam.Length - 5);
                        var products = (from e in query select new { e.scenerio }).Distinct();
                        var count = products.Count();
                        var ExportHandler = new ExcelService(path);
                        var scenerio = count > 0 ? products.ToList().ElementAt(0).scenerio : "";
                        string response = null;
                        for (int i = 0; i < count; ++i)
                        {
                            scenerio = products.ToList().ElementAt(i).scenerio;
                            response = ExportHandler.Export(query.Where(e => e.scenerio == scenerio).ToList(), path + scenerio.Replace("/", ""));
                        }
                    }
                    else
                    {
                        var ExportHandler = new ExcelService(path);
                        string response = ExportHandler.Export(query.ToList(), path);
                    }

                    return new List<CreditIndex>().Take(0).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<CreditIndex>()
                                 where e.scenerio == searchParam
                                 select e);
                    return query.ToArray();
                }
            }
        }


        public IEnumerable<CreditIndex> GetCreditIndexByForcast(int ForcastVal)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<CreditIndex>()
                             where e.foreacst_date == ForcastVal
                             select e);

                return query.ToArray();
            }
        }

        public IEnumerable<DateTime> GetDistinctForcast()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (entityContext.CreditIndexSet.Select(r => r.forcast_date)).Distinct();

                return query.ToFullyLoaded();
            }
        }
    }
}