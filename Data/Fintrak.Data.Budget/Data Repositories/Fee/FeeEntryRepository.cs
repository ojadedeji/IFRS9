using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Budget.Framework.Enums;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Budget.Entities;
using Fintrak.Data.Budget.Contracts;

namespace Fintrak.Data.Budget
{
    [Export(typeof(IFeeEntryRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FeeEntryRepository : DataRepositoryBase<FeeEntry>, IFeeEntryRepository
    {

        protected override FeeEntry AddEntity(BudgetContext entityContext, FeeEntry entity)
        {
            return entityContext.Set<FeeEntry>().Add(entity);
        }

        protected override FeeEntry UpdateEntity(BudgetContext entityContext, FeeEntry entity)
        {
            return (from e in entityContext.Set<FeeEntry>() 
                    where e.FeeEntryId == entity.FeeEntryId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<FeeEntry> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<FeeEntry>()
                   select e;
        }

        protected override FeeEntry GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<FeeEntry>()
                         where e.FeeEntryId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<FeeEntryInfo> GetFeeEntries(string year, string reviewCode, string definitionCode, string misCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.FeeEntrySet
                            join b in entityContext.FeeItemSet on a.ItemCode equals b.Code into bparents
                            from bp in bparents.Where(bpt => (a.Year == bpt.Year && a.ReviewCode == bpt.ReviewCode)).DefaultIfEmpty()
                            join c in entityContext.FeeCategorySet on bp.CategoryCode equals c.Code into cparents
                            from cp in cparents.Where(cpt => (bp.Year == cpt.Year && bp.ReviewCode == cpt.ReviewCode)).DefaultIfEmpty()
                            join d in entityContext.TeamSet on a.MisCode equals d.Code into dparents
                            from dp in dparents.Where(dpt => (a.Year == dpt.Year && a.ReviewCode == dpt.ReviewCode && a.DefintionCode == dpt.DefinitionCode)).DefaultIfEmpty()
                            join e in entityContext.TeamDefinitionSet on dp.DefinitionCode equals e.Code into eparents
                            from ep in eparents.Where(ept => (dp.Year == ept.Year && dp.ReviewCode == ept.ReviewCode)).DefaultIfEmpty()

                            where a.Year == year && a.ReviewCode == reviewCode && a.DefintionCode == definitionCode && a.MisCode == misCode  

                            select new FeeEntryInfo()
                            {
                                FeeEntry = a,
                                FeeItem = bp,
                                FeeCategory = cp,
                                Team = dp,
                                TeamDefinition = ep
                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<FeeEntryInfo> GetFeeEntries(string year, string reviewCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.FeeEntrySet
                            join b in entityContext.FeeItemSet on a.ItemCode equals b.Code into bparents
                            from bp in bparents.Where(bpt => (a.Year == bpt.Year && a.ReviewCode == bpt.ReviewCode)).DefaultIfEmpty()
                            join c in entityContext.FeeCategorySet on bp.CategoryCode equals c.Code into cparents
                            from cp in cparents.Where(cpt => (bp.Year == cpt.Year && bp.ReviewCode == cpt.ReviewCode)).DefaultIfEmpty()
                            join d in entityContext.TeamSet on a.MisCode equals d.Code into dparents
                            from dp in dparents.Where(dpt => (a.Year == dpt.Year && a.ReviewCode == dpt.ReviewCode && a.DefintionCode == dpt.DefinitionCode)).DefaultIfEmpty()
                            join e in entityContext.TeamDefinitionSet on dp.DefinitionCode equals e.Code into eparents
                            from ep in eparents.Where(ept => (dp.Year == ept.Year && dp.ReviewCode == ept.ReviewCode)).DefaultIfEmpty()

                            where a.Year == year && a.ReviewCode == reviewCode 

                            select new FeeEntryInfo()
                            {
                                FeeEntry = a,
                                FeeItem = bp,
                                FeeCategory = cp,
                                Team = dp,
                                TeamDefinition = ep
                            };

                return query.ToFullyLoaded();
            }
        }
    }
}
