using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ILgdInputFactorRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LgdInputFactorRepository : DataRepositoryBase<LgdInputFactor>, ILgdInputFactorRepository
    {
        protected override LgdInputFactor AddEntity(IFRSContext entityContext, LgdInputFactor entity)
        {
            return entityContext.Set<LgdInputFactor>().Add(entity);
        }

        protected override LgdInputFactor UpdateEntity(IFRSContext entityContext, LgdInputFactor entity)
        {
            return (from e in entityContext.Set<LgdInputFactor>()
                    where e.LgdInputFactorId == entity.LgdInputFactorId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<LgdInputFactor> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<LgdInputFactor>()
                   select e;
        }

        protected override LgdInputFactor GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<LgdInputFactor>()
                         where e.LgdInputFactorId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}