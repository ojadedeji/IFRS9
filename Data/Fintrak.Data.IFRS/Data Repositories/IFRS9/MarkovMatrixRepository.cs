using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IMarkovMatrixRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MarkovMatrixRepository : DataRepositoryBase<MarkovMatrix>, IMarkovMatrixRepository
    {
        protected override MarkovMatrix AddEntity(IFRSContext entityContext, MarkovMatrix entity)
        {
            return entityContext.Set<MarkovMatrix>().Add(entity);
        }

        protected override MarkovMatrix UpdateEntity(IFRSContext entityContext, MarkovMatrix entity)
        {
            return (from e in entityContext.Set<MarkovMatrix>()
                    where e.MarkovMatrixId == entity.MarkovMatrixId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<MarkovMatrix> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<MarkovMatrix>()
                   select e;
        }

        protected override MarkovMatrix GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<MarkovMatrix>()
                         where e.MarkovMatrixId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<MarkovMatrixInfo> GetMarkovMatrixs()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.MarkovMatrixSet
                            join b in entityContext.SectorSet on a.Sector equals b.Code
                            select new MarkovMatrixInfo()
                            {
                                MarkovMatrix = a,
                                Sector = b

                            };

                return query.ToFullyLoaded();
            }
        }
       
    }
}