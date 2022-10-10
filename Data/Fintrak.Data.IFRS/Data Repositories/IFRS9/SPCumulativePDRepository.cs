using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ISPCumulativePDRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SPCumulativePDRepository : DataRepositoryBase<SPCumulativePD>, ISPCumulativePDRepository
    {
        protected override SPCumulativePD AddEntity(IFRSContext entityContext, SPCumulativePD entity)
        {
            return entityContext.Set<SPCumulativePD>().Add(entity);
        }

        protected override SPCumulativePD UpdateEntity(IFRSContext entityContext, SPCumulativePD entity)
        {
            return (from e in entityContext.Set<SPCumulativePD>()
                    where e.SPCumulative_Id == entity.SPCumulative_Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<SPCumulativePD> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<SPCumulativePD>()
                   select e;
        }

        protected override SPCumulativePD GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<SPCumulativePD>()
                         where e.SPCumulative_Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}