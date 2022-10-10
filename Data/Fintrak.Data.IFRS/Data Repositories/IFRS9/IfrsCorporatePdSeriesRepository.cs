using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Linq.Dynamic;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;
using Fintrak.Shared.Common.Services;
using Fintrak.Shared.Common.Services.QueryService;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IIfrsCorporatePdSeriesRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsCorporatePdSeriesRepository : DataRepositoryBase<IfrsCorporatePdSeries>, IIfrsCorporatePdSeriesRepository
    {
        protected override IfrsCorporatePdSeries AddEntity(IFRSContext entityContext, IfrsCorporatePdSeries entity)
        {
            return entityContext.Set<IfrsCorporatePdSeries>().Add(entity);
        }

        protected override IfrsCorporatePdSeries UpdateEntity(IFRSContext entityContext, IfrsCorporatePdSeries entity)
        {
            return (from e in entityContext.Set<IfrsCorporatePdSeries>()
                    where e.Sno == entity.Sno
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<IfrsCorporatePdSeries> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<IfrsCorporatePdSeries>().Take(100)
                   select e;
        }

        protected override IfrsCorporatePdSeries GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IfrsCorporatePdSeries>()
                         where e.Sno == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
        public IEnumerable<IfrsCorporatePdSeries> GetPaginatedEntities(QueryOptions queryOptions)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.IfrsCorporatePdSeriesSet
                            select a;

                query = (queryOptions.FilterFieldType == "string")
                    ? query.Where(queryOptions.FilterField + ".contains(@0)", queryOptions.FilterOption)
                    : query.Where(queryOptions.FilterField + " = " + queryOptions.FilterOption);

                var queryArray = query.OrderBy(queryOptions.Sort)
                            .Skip(
                               QueryOptionsCalculator.CalculateStart(queryOptions)
                           ).Take(queryOptions.PageSize)
                           .ToFullyLoaded();

                return queryArray;
            }
        }
        public string GetForExport(string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from e in entityContext.Set<IfrsCorporatePdSeries>()
                            select e;
                query = query.OrderBy(a => a.Sno);

                var ExportHandler = new ExcelService();
                return ExportHandler.Export(query.ToList(), path);
            }
        }

        public UInt64 GetTotalRecordsCount(string tableName, string columnName, string searchParamS, Double? searchParamN)
        {
            return 0;
        }
       
    }
}