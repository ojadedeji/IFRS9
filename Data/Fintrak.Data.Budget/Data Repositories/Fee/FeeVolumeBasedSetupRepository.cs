using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Budget.Entities;
using Fintrak.Data.Budget.Contracts;
using Fintrak.Shared.Budget.Framework.Enums;

namespace Fintrak.Data.Budget
{
    [Export(typeof(IFeeVolumeBasedSetupRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FeeVolumeBasedSetupRepository : DataRepositoryBase<FeeVolumeBasedSetup>, IFeeVolumeBasedSetupRepository
    {

        protected override FeeVolumeBasedSetup AddEntity(BudgetContext entityContext, FeeVolumeBasedSetup entity)
        {
            return entityContext.Set<FeeVolumeBasedSetup>().Add(entity);
        }

        protected override FeeVolumeBasedSetup UpdateEntity(BudgetContext entityContext, FeeVolumeBasedSetup entity)
        {
            return (from e in entityContext.Set<FeeVolumeBasedSetup>() 
                    where e.FeeVolumeBasedSetupId == entity.FeeVolumeBasedSetupId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<FeeVolumeBasedSetup> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<FeeVolumeBasedSetup>()
                   select e;
        }

        protected override FeeVolumeBasedSetup GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<FeeVolumeBasedSetup>()
                         where e.FeeVolumeBasedSetupId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<FeeVolumeBasedSetupInfo> GetFeeVolumeBasedSetups(string year, string reviewCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.FeeVolumeBasedSetupSet
                            join b in entityContext.FeeItemSet on a.FeeCode equals b.Code into bparents
                            from bp in bparents.Where(bpt => (a.Year == bpt.Year && a.ReviewCode == bpt.ReviewCode)).DefaultIfEmpty()
                            join c in entityContext.FeeCategorySet on bp.CategoryCode equals c.Code into cparents
                            from cp in cparents.Where(cpt => (bp.Year == cpt.Year && bp.ReviewCode == cpt.ReviewCode)).DefaultIfEmpty()

                            where a.Year == year && a.ReviewCode == reviewCode
                            select new FeeVolumeBasedSetupInfo()
                            {
                                FeeVolumeBasedSetup = a,
                                FeeItem = bp,
                                FeeCategory = cp
                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<FeeVolumeBasedSetupInfo> GetFeeVolumeBasedSetups(string year, string reviewCode,string categoryCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.FeeVolumeBasedSetupSet
                            join b in entityContext.FeeItemSet on a.FeeCode equals b.Code into bparents
                            from bp in bparents.Where(bpt => (a.Year == bpt.Year && a.ReviewCode == bpt.ReviewCode)).DefaultIfEmpty()
                            join c in entityContext.FeeCategorySet on bp.CategoryCode equals c.Code into cparents
                            from cp in cparents.Where(cpt => (bp.Year == cpt.Year && bp.ReviewCode == cpt.ReviewCode)).DefaultIfEmpty()

                            where a.Year == year && a.ReviewCode == reviewCode && bp.Code == categoryCode

                            select new FeeVolumeBasedSetupInfo()
                            {
                                FeeVolumeBasedSetup = a,
                                FeeItem = bp,
                                FeeCategory = cp
                            };

                return query.ToFullyLoaded();
            }
        }

    }
}
