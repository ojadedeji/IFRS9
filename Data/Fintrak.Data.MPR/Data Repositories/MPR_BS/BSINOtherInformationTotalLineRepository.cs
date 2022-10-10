using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IBSINOtherInformationTotalLineRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BSINOtherInformationTotalLineRepository : DataRepositoryBase<BSINOtherInformationTotalLine>, IBSINOtherInformationTotalLineRepository
    {

        protected override BSINOtherInformationTotalLine AddEntity(MPRContext entityContext, BSINOtherInformationTotalLine entity)
        {
            return entityContext.Set<BSINOtherInformationTotalLine>().Add(entity);
        }

        protected override BSINOtherInformationTotalLine UpdateEntity(MPRContext entityContext, BSINOtherInformationTotalLine entity)
        {
            return (from e in entityContext.Set<BSINOtherInformationTotalLine>()
                    where e.BSINOtherInformationTotalLineId == entity.BSINOtherInformationTotalLineId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<BSINOtherInformationTotalLine> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<BSINOtherInformationTotalLine>()
                   select e;
        }

        protected override BSINOtherInformationTotalLine GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<BSINOtherInformationTotalLine>()
                         where e.BSINOtherInformationTotalLineId == id
                         select e);

            var results = query.FirstOrDefault() ;

            return results;
        }
      
    }
}
