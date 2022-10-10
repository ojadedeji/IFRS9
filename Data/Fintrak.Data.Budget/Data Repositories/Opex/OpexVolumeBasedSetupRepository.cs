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
    [Export(typeof(IOpexVolumeBasedSetupRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OpexVolumeBasedSetupRepository : DataRepositoryBase<OpexVolumeBasedSetup>, IOpexVolumeBasedSetupRepository
    {

        protected override OpexVolumeBasedSetup AddEntity(BudgetContext entityContext, OpexVolumeBasedSetup entity)
        {
            return entityContext.Set<OpexVolumeBasedSetup>().Add(entity);
        }

        protected override OpexVolumeBasedSetup UpdateEntity(BudgetContext entityContext, OpexVolumeBasedSetup entity)
        {
            return (from e in entityContext.Set<OpexVolumeBasedSetup>() 
                    where e.OpexVolumeBasedSetupId == entity.OpexVolumeBasedSetupId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<OpexVolumeBasedSetup> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<OpexVolumeBasedSetup>()
                   select e;
        }

        protected override OpexVolumeBasedSetup GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<OpexVolumeBasedSetup>()
                         where e.OpexVolumeBasedSetupId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<OpexVolumeBasedSetupInfo> GetOpexVolumeBasedSetups(string year, string reviewCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.OpexVolumeBasedSetupSet
                            join b in entityContext.OpexItemSet on a.OpexCode equals b.Code into bparents
                            from bp in bparents.Where(bpt => (a.Year == bpt.Year && a.ReviewCode == bpt.ReviewCode)).DefaultIfEmpty()
                            join c in entityContext.OpexCategorySet on bp.CategoryCode equals c.Code into cparents
                            from cp in cparents.Where(cpt => (bp.Year == cpt.Year && bp.ReviewCode == cpt.ReviewCode)).DefaultIfEmpty()

                            where a.Year == year && a.ReviewCode == reviewCode
                            select new OpexVolumeBasedSetupInfo()
                            {
                                OpexVolumeBasedSetup = a,
                                OpexItem = bp,
                                OpexCategory = cp
                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<OpexVolumeBasedSetupInfo> GetOpexVolumeBasedSetups(string year, string reviewCode,string categoryCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.OpexVolumeBasedSetupSet
                            join b in entityContext.OpexItemSet on a.OpexCode equals b.Code into bparents
                            from bp in bparents.Where(bpt => (a.Year == bpt.Year && a.ReviewCode == bpt.ReviewCode)).DefaultIfEmpty()
                            join c in entityContext.OpexCategorySet on bp.CategoryCode equals c.Code into cparents
                            from cp in cparents.Where(cpt => (bp.Year == cpt.Year && bp.ReviewCode == cpt.ReviewCode)).DefaultIfEmpty()

                            where a.Year == year && a.ReviewCode == reviewCode && bp.Code == categoryCode

                            select new OpexVolumeBasedSetupInfo()
                            {
                                OpexVolumeBasedSetup = a,
                                OpexItem = bp,
                                OpexCategory = cp
                            };

                return query.ToFullyLoaded();
            }
        }

    }
}
