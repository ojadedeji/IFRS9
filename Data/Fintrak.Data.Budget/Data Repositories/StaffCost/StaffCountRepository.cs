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
    [Export(typeof(IStaffCountRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class StaffCountRepository : DataRepositoryBase<StaffCount>, IStaffCountRepository
    {

        protected override StaffCount AddEntity(BudgetContext entityContext, StaffCount entity)
        {
            return entityContext.Set<StaffCount>().Add(entity);
        }

        protected override StaffCount UpdateEntity(BudgetContext entityContext, StaffCount entity)
        {
            return (from e in entityContext.Set<StaffCount>() 
                    where e.StaffCountId == entity.StaffCountId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<StaffCount> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<StaffCount>()
                   select e;
        }

        protected override StaffCount GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<StaffCount>()
                         where e.StaffCountId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<StaffCountInfo> GetStaffCounts(string year, string reviewCode, string definitionCode, string misCode, CenterTypeEnum center)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.StaffCountSet
                            join b in entityContext.PayClassificationSet on a.ClassificationCode equals b.Code into bparents
                            from bp in bparents.Where(bpt => (a.Year == bpt.Year && a.ReviewCode == bpt.ReviewCode)).DefaultIfEmpty()
                            join c in entityContext.GradeSet on a.GradeCode equals c.Code into cparents
                            from cp in cparents.Where(cpt => (a.Year == cpt.Year && a.ReviewCode == cpt.ReviewCode)).DefaultIfEmpty()
                            join d in entityContext.TeamSet on a.MisCode equals d.Code into dparents
                            from dp in dparents.Where(dpt => (a.Year == dpt.Year && a.ReviewCode == dpt.ReviewCode)).DefaultIfEmpty()
                            join e in entityContext.TeamDefinitionSet on dp.DefinitionCode equals e.Code into eparents
                            from ep in eparents.Where(ept => (dp.Year == ept.Year && dp.ReviewCode == ept.ReviewCode)).DefaultIfEmpty()

                            where a.Year == year && a.ReviewCode == reviewCode && a.DefintionCode == definitionCode && a.MisCode == misCode && a.CenterType == center 

                            select new StaffCountInfo()
                            {
                                StaffCount = a,
                                PayClassification = bp,
                                Grade = cp,
                                Team = dp,
                                TeamDefinition = ep
                            };

                return query.ToFullyLoaded();
            }
        }

      
    }
}
