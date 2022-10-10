using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IImpairmentInvestmentRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ImpairmentInvestmentRepository : DataRepositoryBase<ImpairmentInvestment>, IImpairmentInvestmentRepository
    {
        protected override ImpairmentInvestment AddEntity(IFRSContext entityContext, ImpairmentInvestment entity)
        {
            return entityContext.Set<ImpairmentInvestment>().Add(entity);
        }

        protected override ImpairmentInvestment UpdateEntity(IFRSContext entityContext, ImpairmentInvestment entity)
        {
            return (from e in entityContext.Set<ImpairmentInvestment>()
                    where e.Investment_Id == entity.Investment_Id
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<ImpairmentInvestment> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<ImpairmentInvestment>().Take(200)
                   select e;
        }

        protected override ImpairmentInvestment GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ImpairmentInvestment>()
                         where e.Investment_Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}