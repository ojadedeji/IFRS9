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
    [Export(typeof(ICummulativeMatrixRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CummulativeMatrixRepository : DataRepositoryBase<CummulativeMatrix>, ICummulativeMatrixRepository
    {
        protected override CummulativeMatrix AddEntity(IFRSContext entityContext, CummulativeMatrix entity)
        {
            return entityContext.Set<CummulativeMatrix>().Add(entity);
        }

        protected override CummulativeMatrix UpdateEntity(IFRSContext entityContext, CummulativeMatrix entity)
        {
            return (from e in entityContext.Set<CummulativeMatrix>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CummulativeMatrix> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<CummulativeMatrix>()
                   select e;
        }

        protected override CummulativeMatrix GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CummulativeMatrix>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<CummulativeMatrix> GetAvailableCummulativeMatrix(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<CummulativeMatrix>()
                                 select new
                                 {
                                     e.Sector,
                                     e.Mat_level,
                                     e.Period,
                                     e.Stage1,
                                     e.Stage2,
                                     e.Stage3,
                                     e.Quater,
                                     e.Scenerio
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<CummulativeMatrix>().Take(0).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<CummulativeMatrix>().Take(defaultCount)
                                 select e);

                    return query.ToArray();
                }
            }
        }


        public IEnumerable<CummulativeMatrix> GetCummulativeMatrixBySearch(string searchParam, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (searchParam.Contains("ExportData "))
                {
                    searchParam = searchParam.Replace("ExportData ", "");
                    var query = (from e in entityContext.Set<CummulativeMatrix>()
                                 where searchParam.Contains(e.Sector)
                                 select new
                                 {
                                     e.Sector,
                                     e.Mat_level,
                                     e.Period,
                                     e.Stage1,
                                     e.Stage2,
                                     e.Stage3,
                                     e.Quater,
                                     e.Scenerio
                                 });

                    if (searchParam.Substring(0, 5) == "split")
                    {
                        searchParam = searchParam.Substring(5, searchParam.Length - 5);
                        var products = (from e in query select new { e.Sector }).Distinct();
                        var count = products.Count();
                        var ExportHandler = new ExcelService(path);
                        var Sector = count > 0 ? products.ToList().ElementAt(0).Sector : "";
                        string response = null;
                        for (int i = 0; i < count; ++i)
                        {
                            Sector = products.ToList().ElementAt(i).Sector;
                            response = ExportHandler.Export(query.Where(e => e.Sector == Sector).ToList(), path + Sector.Replace("/", ""));
                        }
                    }
                    else
                    {
                        var ExportHandler = new ExcelService(path);
                        string response = ExportHandler.Export(query.ToList(), path);
                    }

                    return new List<CummulativeMatrix>().Take(0).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<CummulativeMatrix>()
                                 where e.Sector == searchParam
                                 select e);
                    return query.ToArray();
                }
            }
        }


        public IEnumerable<CummulativeMatrix> GetCummulativeMatrixByMat_level(string Mat_levelVal)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<CummulativeMatrix>()
                             where e.Mat_level == Mat_levelVal
                             select e);

                return query.ToArray();
            }
        }

        public IEnumerable<string> GetDistinctMatlevel()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (entityContext.CummulativeMatrixSet.Select(r => r.Mat_level)).Distinct();

                return query.ToFullyLoaded();
            }
        }
    }
}