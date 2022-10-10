using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IOffBalanceSheetExposureRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OffBalanceSheetExposureRepository : DataRepositoryBase<OffBalanceSheetExposure>, IOffBalanceSheetExposureRepository
    {
        protected override OffBalanceSheetExposure AddEntity(IFRSContext entityContext, OffBalanceSheetExposure entity)
        {
            return entityContext.Set<OffBalanceSheetExposure>().Add(entity);
        }

        protected override OffBalanceSheetExposure UpdateEntity(IFRSContext entityContext, OffBalanceSheetExposure entity)
        {
            return (from e in entityContext.Set<OffBalanceSheetExposure>()
                    where e.ObeId == entity.ObeId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<OffBalanceSheetExposure> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<OffBalanceSheetExposure>()
                   select e;
        }

        protected override OffBalanceSheetExposure GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<OffBalanceSheetExposure>()
                         where e.ObeId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}