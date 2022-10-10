using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Linq.Dynamic;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Data.Core.Contracts;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Common.Services;
using Fintrak.Shared.Common.Services.QueryService;

namespace Fintrak.Data.Core
{
    [Export(typeof(IProductRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProductRepository : DataRepositoryBase<Product>, IProductRepository
    {
        protected override Product AddEntity(CoreContext entityContext, Product entity)
        {
            return entityContext.Set<Product>().Add(entity);
        }

        protected override Product UpdateEntity(CoreContext entityContext, Product entity)
        {
            return (from e in entityContext.Set<Product>()
                    where e.ProductId == entity.ProductId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Product> GetEntities(CoreContext entityContext)
        {
            return from e in entityContext.Set<Product>()
                   select e;
        }

        protected override Product GetEntity(CoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<Product>()
                         where e.ProductId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<Product> GetPaginatedEntities(QueryOptions queryOptions)
        {
            using (CoreContext entityContext = new CoreContext())
            {
                var query = from a in entityContext.ProductSet
                            select a;

                query = (queryOptions.FilterFieldType == "string")
                    ? query.Where(p => p.Code.Contains(queryOptions.FilterOption)
                                        || p.Name.Contains(queryOptions.FilterOption)
                                        || p.AssetGL.Contains(queryOptions.FilterOption)
                                        || p.LiabilityGL.Contains(queryOptions.FilterOption)
                                        || p.IncomeGL.Contains(queryOptions.FilterOption)
                                        || p.ExpenseGL.Contains(queryOptions.FilterOption)
                                        )
                    : query.Where(queryOptions.FilterField + " = " + queryOptions.FilterOption);

                var queryArray = query.OrderBy(queryOptions.Sort)
                            .Skip(
                               QueryOptionsCalculator.CalculateStart(queryOptions)
                           ).Take(queryOptions.PageSize)
                           .ToList();

                return queryArray;
            }
        }
    }
}
