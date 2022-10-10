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
    [Export(typeof(IAdjustedMatrixRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AdjustedMatrixRepository : DataRepositoryBase<AdjustedMatrix>, IAdjustedMatrixRepository
    {
        protected override AdjustedMatrix AddEntity(IFRSContext entityContext, AdjustedMatrix entity)
        {
            return entityContext.Set<AdjustedMatrix>().Add(entity);
        }

        protected override AdjustedMatrix UpdateEntity(IFRSContext entityContext, AdjustedMatrix entity)
        {
            return (from e in entityContext.Set<AdjustedMatrix>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<AdjustedMatrix> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<AdjustedMatrix>()
                   select e;
        }

        protected override AdjustedMatrix GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<AdjustedMatrix>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<AdjustedMatrix> GetAvailableAdjustedMatrix(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<AdjustedMatrix>()
                                 select new
                                 {
                                     e.Mat_level,
                                     e.Sector,
                                     e.Period,
                                     e.Stage1,
                                     e.Stage2,
                                     e.Stage3,
                                     e.Quater,
                                     e.Scenerio
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<AdjustedMatrix>().Take(0).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<AdjustedMatrix>().Take(defaultCount) 
                                 select e);

                    return query.ToArray();
                }
            }
        }


        public IEnumerable<AdjustedMatrix> GetAdjustedMatrixBySearch(string searchParam, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (searchParam.Contains("ExportData "))
                {
                    searchParam = searchParam.Replace("ExportData ", "");
                    var query = (from e in entityContext.Set<AdjustedMatrix>()
                                 where searchParam.Contains(e.Sector)
                                 select new
                                 {
                                     e.Mat_level,
                                     e.Sector,
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

                    return new List<AdjustedMatrix>().Take(0).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<AdjustedMatrix>()
                                 where e.Sector == searchParam
                                 select e);
                    return query.ToArray();
                }
            }
        }


        public IEnumerable<AdjustedMatrix> GetAdjustedMatrixByMat_level(string Mat_levelVal)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<AdjustedMatrix>()
                             where e.Mat_level == Mat_levelVal
                             select e);

                return query.ToArray();
            }
        }

        public IEnumerable<string> GetDistinctMat_level()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (entityContext.AdjustedMatrixSet.Select(r => r.Mat_level)).Distinct();

                return query.ToFullyLoaded();
            }
        }
    }
}