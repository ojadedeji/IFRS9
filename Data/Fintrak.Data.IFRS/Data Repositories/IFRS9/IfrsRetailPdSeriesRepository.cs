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
    [Export(typeof(IIfrsRetailPdSeriesRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsRetailPdSeriesRepository : DataRepositoryBase<IfrsRetailPdSeries>, IIfrsRetailPdSeriesRepository
    {
        protected override IfrsRetailPdSeries AddEntity(IFRSContext entityContext, IfrsRetailPdSeries entity)
        {
            return entityContext.Set<IfrsRetailPdSeries>().Add(entity);
        }

        protected override IfrsRetailPdSeries UpdateEntity(IFRSContext entityContext, IfrsRetailPdSeries entity)
        {
            return (from e in entityContext.Set<IfrsRetailPdSeries>()
                    where e.PdSeriesId == entity.PdSeriesId
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<IfrsRetailPdSeries> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<IfrsRetailPdSeries>().Take(100)
                   select e;
        }

        protected override IfrsRetailPdSeries GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IfrsRetailPdSeries>()
                         where e.PdSeriesId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
        public IEnumerable<IfrsRetailPdSeries> GetPaginatedEntities(QueryOptions queryOptions)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.IfrsRetailPdSeriesSet
                            select a;

                query = (queryOptions.FilterFieldType == "string")
                    ? query.Where(queryOptions.FilterField + ".contains(@0)", queryOptions.FilterOption)
                    : query.Where(queryOptions.FilterField + " = " + queryOptions.FilterOption);

                var queryArray = query.OrderBy("ClassificationStage ASC")
                            .Skip(
                               QueryOptionsCalculator.CalculateStart(queryOptions)
                           ).Take(queryOptions.PageSize)
                           .ToFullyLoaded();

                return queryArray;
            }
        }

        public UInt64 GetTotalRecordsCount(string tableName, string columnName, string searchParamS, Double? searchParamN)
        {
            return 0;
        }
       
    }
}