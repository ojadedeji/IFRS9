using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IGeneralTransferPriceRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GeneralTransferPriceRepository : DataRepositoryBase<GeneralTransferPrice>, IGeneralTransferPriceRepository
    {

        protected override GeneralTransferPrice AddEntity(MPRContext entityContext, GeneralTransferPrice entity)
        {
            return entityContext.Set<GeneralTransferPrice>().Add(entity);
        }

        protected override GeneralTransferPrice UpdateEntity(MPRContext entityContext, GeneralTransferPrice entity)
        {
            return (from e in entityContext.Set<GeneralTransferPrice>()
                    where e.GeneralTransferPriceId == entity.GeneralTransferPriceId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<GeneralTransferPrice> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<GeneralTransferPrice>()
                   select e;
        }

        protected override GeneralTransferPrice GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<GeneralTransferPrice>()
                         where e.GeneralTransferPriceId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
 
    }
}
