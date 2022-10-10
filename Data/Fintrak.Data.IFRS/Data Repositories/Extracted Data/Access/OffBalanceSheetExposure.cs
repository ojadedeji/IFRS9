using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IOffBalanceSheetExposureABPRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OffBalanceSheetExposureABPRepository : DataRepositoryBase<OffBalanceSheetExposureABP>, IOffBalanceSheetExposureABPRepository
    {
        protected override OffBalanceSheetExposureABP AddEntity(IFRSContext entityContext, OffBalanceSheetExposureABP entity)
        {
            return entityContext.Set<OffBalanceSheetExposureABP>().Add(entity);
        }

        protected override OffBalanceSheetExposureABP UpdateEntity(IFRSContext entityContext, OffBalanceSheetExposureABP entity)
        {
            return (from e in entityContext.Set<OffBalanceSheetExposureABP>()
                    where e.ObeId == entity.ObeId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<OffBalanceSheetExposureABP> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<OffBalanceSheetExposureABP>().Take(500)
                   select e;
        }

        protected override OffBalanceSheetExposureABP GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<OffBalanceSheetExposureABP>()
                         where e.ObeId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}