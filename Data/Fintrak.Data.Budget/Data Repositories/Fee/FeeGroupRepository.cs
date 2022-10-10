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
    [Export(typeof(IFeeGroupRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FeeGroupRepository : DataRepositoryBase<FeeGroup>, IFeeGroupRepository
    {

        protected override FeeGroup AddEntity(BudgetContext entityContext, FeeGroup entity)
        {
            return entityContext.Set<FeeGroup>().Add(entity);
        }

        protected override FeeGroup UpdateEntity(BudgetContext entityContext, FeeGroup entity)
        {
            return (from e in entityContext.Set<FeeGroup>() 
                    where e.FeeGroupId == entity.FeeGroupId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<FeeGroup> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<FeeGroup>()
                   select e;
        }

        protected override FeeGroup GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<FeeGroup>()
                         where e.FeeGroupId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<FeeGroupInfo> GetFeeGroups(string year, string reviewCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.FeeGroupSet
                            join b in entityContext.FeeGroupSet on a.ParentCode equals b.Code into bparents
                            from bp in bparents.Where(bpt => (a.Year == bpt.Year && a.ReviewCode == bpt.ReviewCode)).DefaultIfEmpty()
                            where a.Year == year && a.ReviewCode == reviewCode
                            select new FeeGroupInfo()
                            {
                                FeeGroup = a,
                                Parent = bp
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
