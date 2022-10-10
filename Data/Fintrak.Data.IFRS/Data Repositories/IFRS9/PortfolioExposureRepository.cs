using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IPortfolioExposureRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PortfolioExposureRepository : DataRepositoryBase<PortfolioExposure>, IPortfolioExposureRepository
    {
        protected override PortfolioExposure AddEntity(IFRSContext entityContext, PortfolioExposure entity)
        {
            return entityContext.Set<PortfolioExposure>().Add(entity);
        }

        protected override PortfolioExposure UpdateEntity(IFRSContext entityContext, PortfolioExposure entity)
        {
            return (from e in entityContext.Set<PortfolioExposure>()
                    where e.PortfolioId == entity.PortfolioId
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<PortfolioExposure> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<PortfolioExposure>()
                   select e;
        }

        protected override PortfolioExposure GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<PortfolioExposure>()
                         where e.PortfolioId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}