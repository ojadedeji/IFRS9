using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IRegressionOutputRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RegressionOutputRepository : DataRepositoryBase<RegressionOutput>, IRegressionOutputRepository
    {
        protected override RegressionOutput AddEntity(IFRSContext entityContext, RegressionOutput entity)
        {
            return entityContext.Set<RegressionOutput>().Add(entity);
        }

        protected override RegressionOutput UpdateEntity(IFRSContext entityContext, RegressionOutput entity)
        {
            return (from e in entityContext.Set<RegressionOutput>()
                    where e.RegressionOutputId == entity.RegressionOutputId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<RegressionOutput> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<RegressionOutput>()
                   select e;
        }

        protected override RegressionOutput GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<RegressionOutput>()
                         where e.RegressionOutputId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}