using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Data.Core.Contracts;
using Fintrak.Shared.Core.Entities;

namespace Fintrak.Data.Core
{
    [Export(typeof(IPayGradeRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PayGradeRepository : DataRepositoryBase<PayGrade>, IPayGradeRepository
    {
        protected override PayGrade AddEntity(CoreContext entityContext, PayGrade entity)
        {
            return entityContext.Set<PayGrade>().Add(entity);
        }

        protected override PayGrade UpdateEntity(CoreContext entityContext, PayGrade entity)
        {
            return (from e in entityContext.Set<PayGrade>()
                    where e.PayGradeId == entity.PayGradeId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<PayGrade> GetEntities(CoreContext entityContext)
        {
            return from e in entityContext.Set<PayGrade>()
                   select e;
        }

        protected override PayGrade GetEntity(CoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<PayGrade>()
                         where e.PayGradeId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}
