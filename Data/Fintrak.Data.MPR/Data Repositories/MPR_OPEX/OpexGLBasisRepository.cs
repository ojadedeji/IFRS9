using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IOpexGLBasisRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OpexGLBasisRepository : DataRepositoryBase<OpexGLBasis>, IOpexGLBasisRepository
    {

        protected override OpexGLBasis AddEntity(MPRContext entityContext, OpexGLBasis entity)
        {
            return entityContext.Set<OpexGLBasis>().Add(entity);
        }

        protected override OpexGLBasis UpdateEntity(MPRContext entityContext, OpexGLBasis entity)
        {
            return (from e in entityContext.Set<OpexGLBasis>()
                    where e.OpexGLBasisId == entity.OpexGLBasisId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<OpexGLBasis> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<OpexGLBasis>()
                   select e;
        }

        protected override OpexGLBasis GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<OpexGLBasis>()
                         where e.OpexGLBasisId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
