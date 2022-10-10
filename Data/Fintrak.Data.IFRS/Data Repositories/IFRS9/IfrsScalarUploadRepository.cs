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
    [Export(typeof(IIfrsScalarUploadRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsScalarUploadRepository : DataRepositoryBase<IfrsScalarUpload>, IIfrsScalarUploadRepository
    {
        protected override IfrsScalarUpload AddEntity(IFRSContext entityContext, IfrsScalarUpload entity)
        {
            return entityContext.Set<IfrsScalarUpload>().Add(entity);
        }

        protected override IfrsScalarUpload UpdateEntity(IFRSContext entityContext, IfrsScalarUpload entity)
        {
            return (from e in entityContext.Set<IfrsScalarUpload>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IfrsScalarUpload> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<IfrsScalarUpload>()
                   select e;
        }

        protected override IfrsScalarUpload GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IfrsScalarUpload>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IfrsScalarUpload> GetAvailableIfrsScalarUpload(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<IfrsScalarUpload>()
                                 select new
                                 {
                                     e.PERIOD,
                                     e.NPL,
                                     e.GDP,
                                     e.Inflation,
                                     e.Exchange,
                                     e.ScalarType
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<IfrsScalarUpload>().Take(0).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsScalarUpload>().Take(defaultCount) 
                                 select e);

                    return query.ToArray();
                }
            }
        }


        public IEnumerable<IfrsScalarUpload> GetIfrsScalarUploadBySearch(string searchParam, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (searchParam.Contains("ExportData "))
                {
                    searchParam = searchParam.Replace("ExportData ", "");
                    var query = (from e in entityContext.Set<IfrsScalarUpload>()
                                 where searchParam.Contains(e.PERIOD)
                                 select new
                                 {
                                     e.PERIOD,
                                     e.NPL,
                                     e.GDP,
                                     e.Inflation,
                                     e.Exchange,
                                     e.ScalarType
                                 });

                    if (searchParam.Substring(0, 5) == "split")
                    {
                        searchParam = searchParam.Substring(5, searchParam.Length - 5);
                        var products = (from e in query select new { e.PERIOD }).Distinct();
                        var count = products.Count();
                        var ExportHandler = new ExcelService(path);
                        var PERIOD = count > 0 ? products.ToList().ElementAt(0).PERIOD : "";
                        string response = null;
                        for (int i = 0; i < count; ++i)
                        {
                            PERIOD = products.ToList().ElementAt(i).PERIOD;
                            response = ExportHandler.Export(query.Where(e => e.PERIOD == PERIOD).ToList(), path + PERIOD.Replace("/", ""));
                        }
                    }
                    else
                    {
                        var ExportHandler = new ExcelService(path);
                        string response = ExportHandler.Export(query.ToList(), path);
                    }

                    return new List<IfrsScalarUpload>().Take(0).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsScalarUpload>()
                                 where e.PERIOD == searchParam
                                 select e);
                    return query.ToArray();
                }
            }
        }


        public IEnumerable<IfrsScalarUpload> GetIfrsScalarUploadByScalarType(string ScalarTypeVal)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<IfrsScalarUpload>()
                             where e.ScalarType == ScalarTypeVal
                             select e);

                return query.ToArray();
            }
        }

        public IEnumerable<string> GetDistinctScalarType()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (entityContext.IfrsScalarUploadSet.Select(r => r.ScalarType)).Distinct();

                return query.ToFullyLoaded();
            }
        }
    }
}