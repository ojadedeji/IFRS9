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
    [Export(typeof(IPayStructureRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PayStructureRepository : DataRepositoryBase<PayStructure>, IPayStructureRepository
    {

        protected override PayStructure AddEntity(BudgetContext entityContext, PayStructure entity)
        {
            return entityContext.Set<PayStructure>().Add(entity);
        }

        protected override PayStructure UpdateEntity(BudgetContext entityContext, PayStructure entity)
        {
            return (from e in entityContext.Set<PayStructure>() 
                    where e.PayStructureId == entity.PayStructureId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<PayStructure> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<PayStructure>()
                   select e;
        }

        protected override PayStructure GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<PayStructure>()
                         where e.PayStructureId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<PayStructureInfo> GetPayStructures(string year, string reviewCode, string classificationCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.PayStructureSet
                            join b in entityContext.PayClassificationSet on a.ClassificationCode equals b.Code into bparents
                            from bp in bparents.Where(bpt => (a.Year == bpt.Year && a.ReviewCode == bpt.ReviewCode)).DefaultIfEmpty()
                            join c in entityContext.GradeSet on a.GradeCode equals c.Code into cparents
                            from cp in cparents.Where(cpt => (a.Year == cpt.Year && a.ReviewCode == cpt.ReviewCode)).DefaultIfEmpty()

                            where a.Year == year && a.ReviewCode == reviewCode && a.ClassificationCode == classificationCode 
                            select new PayStructureInfo()
                            {
                                PayStructure = a,
                                PayClassification = bp,
                                Grade = cp
                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<PayStructureInfo> GetPayStructures(string year, string reviewCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.PayStructureSet
                            join b in entityContext.PayClassificationSet on a.ClassificationCode equals b.Code into bparents
                            from bp in bparents.Where(bpt => (a.Year == bpt.Year && a.ReviewCode == bpt.ReviewCode)).DefaultIfEmpty()
                            join c in entityContext.GradeSet on a.GradeCode equals c.Code into cparents
                            from cp in cparents.Where(cpt => (a.Year == cpt.Year && a.ReviewCode == cpt.ReviewCode)).DefaultIfEmpty()
                          
                            where a.Year == year && a.ReviewCode == reviewCode 
                            select new PayStructureInfo()
                            {
                                PayStructure = a,
                                PayClassification = bp,
                                Grade = cp
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
