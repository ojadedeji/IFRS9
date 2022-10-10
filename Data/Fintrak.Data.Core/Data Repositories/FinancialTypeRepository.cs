using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Data.Core.Contracts;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Core.Entities;

namespace Fintrak.Data.Core
{
    [Export(typeof(IFinancialTypeRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FinancialTypeRepository : DataRepositoryBase<FinancialType>, IFinancialTypeRepository
    {
        protected override FinancialType AddEntity(CoreContext entityContext, FinancialType entity)
        {
            return entityContext.Set<FinancialType>().Add(entity);
        }

        protected override FinancialType UpdateEntity(CoreContext entityContext, FinancialType entity)
        {
            return (from e in entityContext.Set<FinancialType>()
                    where e.FinancialTypeId == entity.FinancialTypeId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<FinancialType> GetEntities(CoreContext entityContext)
        {
            return from e in entityContext.Set<FinancialType>()
                   select e;
        }

        protected override FinancialType GetEntity(CoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<FinancialType>()
                         where e.FinancialTypeId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<FinancialTypeInfo> GetFinancialTypes()
        {
            using (CoreContext entityContext = new CoreContext())
            {
                var query = from a in entityContext.FinancialTypeSet
                            join b in entityContext.FinancialTypeSet on a.ParentId equals b.FinancialTypeId into parents
                            from pt in parents.DefaultIfEmpty()
                            select new FinancialTypeInfo()
                            {
                                FinancialType = a,
                                Parent = pt
                            };

                return query.ToFullyLoaded();
            }
        }
    }
}
