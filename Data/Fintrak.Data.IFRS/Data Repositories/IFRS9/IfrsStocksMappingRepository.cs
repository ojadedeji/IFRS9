using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IIfrsStocksMappingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsStocksMappingRepository : DataRepositoryBase<IfrsStocksMapping>, IIfrsStocksMappingRepository
    {
        protected override IfrsStocksMapping AddEntity(IFRSContext entityContext, IfrsStocksMapping entity)
        {
            return entityContext.Set<IfrsStocksMapping>().Add(entity);
        }

        protected override IfrsStocksMapping UpdateEntity(IFRSContext entityContext, IfrsStocksMapping entity)
        {
            return (from e in entityContext.Set<IfrsStocksMapping>()
                    where e.IfrsStocksMappingId == entity.IfrsStocksMappingId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IfrsStocksMapping> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<IfrsStocksMapping>()
                   select e;
        }

        protected override IfrsStocksMapping GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IfrsStocksMapping>()
                         where e.IfrsStocksMappingId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IfrsStocksMappingInfo> GetIfrsStocksMappings()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.IfrsStocksMappingSet
                            join b in entityContext.IfrsEquityUnqoutedSet on a.Unqouted_stock_code equals b.Stock_code
                            join c in entityContext.IfrsStocksPrimaryDataSet on a.Qouted_stock_code equals c.Stock_code
                            select new IfrsStocksMappingInfo()
                            {
                                IfrsStocksMapping = a,
                                IfrsEquityUnqouted = b,
                                IfrsStocksPrimaryData = c
                            };

                return query.ToFullyLoaded();
            }
        }
       
    }
}