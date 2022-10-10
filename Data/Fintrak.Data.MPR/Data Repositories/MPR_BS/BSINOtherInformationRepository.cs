using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IBSINOtherInformationRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BSINOtherInformationRepository : DataRepositoryBase<BSINOtherInformation>, IBSINOtherInformationRepository
    {

        protected override BSINOtherInformation AddEntity(MPRContext entityContext, BSINOtherInformation entity)
        {
            return entityContext.Set<BSINOtherInformation>().Add(entity);
        }

        protected override BSINOtherInformation UpdateEntity(MPRContext entityContext, BSINOtherInformation entity)
        {
            return (from e in entityContext.Set<BSINOtherInformation>()
                    where e.BSINOtherInformationId == entity.BSINOtherInformationId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<BSINOtherInformation> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<BSINOtherInformation>()
                   select e;
        }

        protected override BSINOtherInformation GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<BSINOtherInformation>()
                         where e.BSINOtherInformationId == id
                         select e);

            var results = query.FirstOrDefault() ;

            return results;
        }
      
    }
}
