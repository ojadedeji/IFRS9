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
    [Export(typeof(IFeeSharedRatioRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FeeSharedRatioRepository : DataRepositoryBase<FeeSharedRatio>, IFeeSharedRatioRepository
    {

        protected override FeeSharedRatio AddEntity(BudgetContext entityContext, FeeSharedRatio entity)
        {
            return entityContext.Set<FeeSharedRatio>().Add(entity);
        }

        protected override FeeSharedRatio UpdateEntity(BudgetContext entityContext, FeeSharedRatio entity)
        {
            return (from e in entityContext.Set<FeeSharedRatio>() 
                    where e.FeeSharedRatioId == entity.FeeSharedRatioId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<FeeSharedRatio> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<FeeSharedRatio>()
                   select e;
        }

        protected override FeeSharedRatio GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<FeeSharedRatio>()
                         where e.FeeSharedRatioId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<FeeSharedRatioInfo> GetFeeSharedRatios(string year, string reviewCode, string definitionCode, string misCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.FeeSharedRatioSet
                            join b in entityContext.FeeItemSet on a.ItemCode equals b.Code into bparents
                            from bp in bparents.Where(bpt => (a.Year == bpt.Year && a.ReviewCode == bpt.ReviewCode)).DefaultIfEmpty()
                            join c in entityContext.FeeCategorySet on bp.CategoryCode equals c.Code into cparents
                            from cp in cparents.Where(cpt => (bp.Year == cpt.Year && bp.ReviewCode == cpt.ReviewCode)).DefaultIfEmpty()
                            join d in entityContext.TeamSet on a.MisCode equals d.Code into dparents
                            from dp in dparents.Where(dpt => (a.Year == dpt.Year && a.ReviewCode == dpt.ReviewCode && a.DefintionCode == dpt.DefinitionCode)).DefaultIfEmpty()
                            join e in entityContext.TeamDefinitionSet on dp.DefinitionCode equals e.Code into eparents
                            from ep in eparents.Where(ept => (dp.Year == ept.Year && dp.ReviewCode == ept.ReviewCode)).DefaultIfEmpty()

                            where a.Year == year && a.ReviewCode == reviewCode && a.DefintionCode == definitionCode && a.MisCode == misCode  

                            select new FeeSharedRatioInfo()
                            {
                                FeeSharedRatio = a,
                                FeeItem = bp,
                                FeeCategory = cp,
                                Team = dp,
                                TeamDefinition = ep
                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<FeeSharedRatioInfo> GetFeeSharedRatios(string year, string reviewCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.FeeSharedRatioSet
                            join b in entityContext.FeeItemSet on a.ItemCode equals b.Code into bparents
                            from bp in bparents.Where(bpt => (a.Year == bpt.Year && a.ReviewCode == bpt.ReviewCode)).DefaultIfEmpty()
                            join c in entityContext.FeeCategorySet on bp.CategoryCode equals c.Code into cparents
                            from cp in cparents.Where(cpt => (bp.Year == cpt.Year && bp.ReviewCode == cpt.ReviewCode)).DefaultIfEmpty()
                            join d in entityContext.TeamSet on a.MisCode equals d.Code into dparents
                            from dp in dparents.Where(dpt => (a.Year == dpt.Year && a.ReviewCode == dpt.ReviewCode && a.DefintionCode == dpt.DefinitionCode)).DefaultIfEmpty()
                            join e in entityContext.TeamDefinitionSet on dp.DefinitionCode equals e.Code into eparents
                            from ep in eparents.Where(ept => (dp.Year == ept.Year && dp.ReviewCode == ept.ReviewCode)).DefaultIfEmpty()

                            where a.Year == year && a.ReviewCode == reviewCode 

                            select new FeeSharedRatioInfo()
                            {
                                FeeSharedRatio = a,
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
