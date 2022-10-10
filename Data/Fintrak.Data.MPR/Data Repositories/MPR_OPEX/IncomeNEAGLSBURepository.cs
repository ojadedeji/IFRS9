using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomeNEAGLSBURepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeNEAGLSBURepository : DataRepositoryBase<IncomeNEAGLSBU>, IIncomeNEAGLSBURepository
    {

        protected override IncomeNEAGLSBU AddEntity(MPRContext entityContext, IncomeNEAGLSBU entity)
        {
            return entityContext.Set<IncomeNEAGLSBU>().Add(entity);
        }

        protected override IncomeNEAGLSBU UpdateEntity(MPRContext entityContext, IncomeNEAGLSBU entity)
        {
            return (from e in entityContext.Set<IncomeNEAGLSBU>()
                    where e.IncomeNEAGLSBUId == entity.IncomeNEAGLSBUId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomeNEAGLSBU> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<IncomeNEAGLSBU>()
                   select e;
        }

        protected override IncomeNEAGLSBU GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeNEAGLSBU>()
                         where e.IncomeNEAGLSBUId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
