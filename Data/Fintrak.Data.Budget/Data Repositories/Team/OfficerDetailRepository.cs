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
    [Export(typeof(IOfficerDetailRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OfficerDetailRepository : DataRepositoryBase<OfficerDetail>, IOfficerDetailRepository
    {

        protected override OfficerDetail AddEntity(BudgetContext entityContext, OfficerDetail entity)
        {
            return entityContext.Set<OfficerDetail>().Add(entity);
        }

        protected override OfficerDetail UpdateEntity(BudgetContext entityContext, OfficerDetail entity)
        {
            return (from e in entityContext.Set<OfficerDetail>()
                    where e.OfficerDetailId == entity.OfficerDetailId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<OfficerDetail> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<OfficerDetail>()
                   select e;
        }

        protected override OfficerDetail GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<OfficerDetail>()
                         where e.OfficerDetailId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<OfficerDetailInfo> GetOfficerDetailInDefinition(string year, string reviewCode, string definitionCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.OfficerDetailSet
                            join b in entityContext.TeamSet on a.MisCode equals b.Code into bparents
                            from bp in bparents.Where(bpt => (a.Year == bpt.Year && a.ReviewCode == bpt.ReviewCode)).DefaultIfEmpty()
                            join c in entityContext.TeamDefinitionSet on a.DefinitionCode equals c.Code into cparents
                            from cp in cparents.Where(cpt => (a.Year == cpt.Year && a.ReviewCode == cpt.ReviewCode)).DefaultIfEmpty()
                          
                            where a.Year == year && a.ReviewCode == reviewCode  && a.DefinitionCode == definitionCode

                            select new OfficerDetailInfo()
                            {
                                OfficerDetail = a,
                                Team = bp,
                                TeamDefinition = cp
                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<OfficerDetailInfo> GetOfficerDetailUnderDefinition(string year, string reviewCode, string definitionCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.OfficerDetailSet
                            join b in entityContext.TeamSet on a.MisCode equals b.Code into bparents
                            from bp in bparents.Where(bpt => (a.Year == bpt.Year && a.ReviewCode == bpt.ReviewCode)).DefaultIfEmpty()
                            join c in entityContext.TeamDefinitionSet on a.DefinitionCode equals c.Code into cparents
                            from cp in cparents.Where(cpt => (a.Year == cpt.Year && a.ReviewCode == cpt.ReviewCode)).DefaultIfEmpty()
                            join d in entityContext.TeamSet on bp.ParentCode equals d.Code into dparents
                            from dp in dparents.Where(dpt => (bp.Year == dpt.Year && bp.ReviewCode == dpt.ReviewCode)).DefaultIfEmpty()
                            join e in entityContext.TeamDefinitionSet on dp.DefinitionCode equals e.Code into eparents
                            from ep in eparents.Where(ept => (dp.Year == ept.Year && dp.ReviewCode == ept.ReviewCode)).DefaultIfEmpty()
                           

                            where a.Year == year && a.ReviewCode == reviewCode && dp.DefinitionCode == definitionCode

                            select new OfficerDetailInfo()
                            {
                                OfficerDetail = a,
                                Team = bp,
                                TeamDefinition = cp
                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<OfficerDetailInfo> GetOfficerDetailUnderDefinition(string year, string reviewCode, string definitionCode, string misCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.OfficerDetailSet
                            join b in entityContext.TeamSet on a.MisCode equals b.Code into bparents
                            from bp in bparents.Where(bpt => (a.Year == bpt.Year && a.ReviewCode == bpt.ReviewCode)).DefaultIfEmpty()
                            join c in entityContext.TeamDefinitionSet on a.DefinitionCode equals c.Code into cparents
                            from cp in cparents.Where(cpt => (a.Year == cpt.Year && a.ReviewCode == cpt.ReviewCode)).DefaultIfEmpty()
                            join d in entityContext.TeamSet on bp.ParentCode equals d.Code into dparents
                            from dp in dparents.Where(dpt => (bp.Year == dpt.Year && bp.ReviewCode == dpt.ReviewCode)).DefaultIfEmpty()
                            join e in entityContext.TeamDefinitionSet on dp.DefinitionCode equals e.Code into eparents
                            from ep in eparents.Where(ept => (dp.Year == ept.Year && dp.ReviewCode == ept.ReviewCode)).DefaultIfEmpty()

                            where a.Year == year && a.ReviewCode == reviewCode && dp.DefinitionCode == definitionCode && dp.Code == misCode 

                            select new OfficerDetailInfo()
                            {
                                OfficerDetail = a,
                                Team = bp,
                                TeamDefinition = cp
                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<OfficerDetailInfo> GetOfficerDetails(string year, string reviewCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.OfficerDetailSet
                            join b in entityContext.TeamSet on a.MisCode equals b.Code into bparents
                            from bp in bparents.Where(bpt => (a.Year == bpt.Year && a.ReviewCode == bpt.ReviewCode)).DefaultIfEmpty()
                            join c in entityContext.TeamDefinitionSet on a.DefinitionCode equals c.Code into cparents
                            from cp in cparents.Where(cpt => (a.Year == cpt.Year && a.ReviewCode == cpt.ReviewCode)).DefaultIfEmpty()
                           
                            where a.Year == year && a.ReviewCode == reviewCode

                            select new OfficerDetailInfo()
                            {
                                OfficerDetail = a,
                                Team = bp,
                                TeamDefinition = cp
                            };

                return query.ToFullyLoaded();
            }
        }

      
    }
}
