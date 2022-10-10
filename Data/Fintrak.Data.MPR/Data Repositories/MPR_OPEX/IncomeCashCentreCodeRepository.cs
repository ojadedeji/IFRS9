using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomeCashCentreCodeRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeCashCentreCodeRepository : DataRepositoryBase<IncomeCashCentreCode>, IIncomeCashCentreCodeRepository
    {

        protected override IncomeCashCentreCode AddEntity(MPRContext entityContext, IncomeCashCentreCode entity)
        {
            return entityContext.Set<IncomeCashCentreCode>().Add(entity);
        }

        protected override IncomeCashCentreCode UpdateEntity(MPRContext entityContext, IncomeCashCentreCode entity)
        {
            return (from e in entityContext.Set<IncomeCashCentreCode>()
                    where e.IncomeCashCentreCodeId == entity.IncomeCashCentreCodeId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomeCashCentreCode> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<IncomeCashCentreCode>()
                   select e;
        }

        protected override IncomeCashCentreCode GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeCashCentreCode>()
                         where e.IncomeCashCentreCodeId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
