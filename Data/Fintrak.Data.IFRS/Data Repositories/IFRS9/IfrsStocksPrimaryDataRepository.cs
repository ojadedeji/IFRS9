using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IIfrsStocksPrimaryDataRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsStocksPrimaryDataRepository : DataRepositoryBase<IfrsStocksPrimaryData>, IIfrsStocksPrimaryDataRepository
    {
        protected override IfrsStocksPrimaryData AddEntity(IFRSContext entityContext, IfrsStocksPrimaryData entity)
        {
            return entityContext.Set<IfrsStocksPrimaryData>().Add(entity);
        }

        protected override IfrsStocksPrimaryData UpdateEntity(IFRSContext entityContext, IfrsStocksPrimaryData entity)
        {
            return (from e in entityContext.Set<IfrsStocksPrimaryData>()
                    where e.IfrsStocksPrimaryDataId == entity.IfrsStocksPrimaryDataId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IfrsStocksPrimaryData> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<IfrsStocksPrimaryData>()
                   select e;
        }

        protected override IfrsStocksPrimaryData GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IfrsStocksPrimaryData>()
                         where e.IfrsStocksPrimaryDataId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}