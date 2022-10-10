using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomeCentralVaultScheduleRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeCentralVaultScheduleRepository : DataRepositoryBase<IncomeCentralVaultSchedule>, IIncomeCentralVaultScheduleRepository
    {

        protected override IncomeCentralVaultSchedule AddEntity(MPRContext entityContext, IncomeCentralVaultSchedule entity)
        {
            return entityContext.Set<IncomeCentralVaultSchedule>().Add(entity);
        }

        protected override IncomeCentralVaultSchedule UpdateEntity(MPRContext entityContext, IncomeCentralVaultSchedule entity)
        {
            return (from e in entityContext.Set<IncomeCentralVaultSchedule>()
                    where e.IncomeCentralVaultScheduleId == entity.IncomeCentralVaultScheduleId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomeCentralVaultSchedule> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<IncomeCentralVaultSchedule>()
                   select e;
        }

        protected override IncomeCentralVaultSchedule GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeCentralVaultSchedule>()
                         where e.IncomeCentralVaultScheduleId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IncomeCentralVaultScheduleInfo> GetIncomeCentralVaultSchedule()
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.IncomeCentralVaultScheduleSet
                            join c in entityContext.BranchSet on a.BranchCode equals c.Code into cparents
                            from pc in cparents.DefaultIfEmpty()
                            select new IncomeCentralVaultScheduleInfo()
                            {
                                IncomeCentralVaultSchedule = a,
                                Branch = pc
                            };

                return query.ToFullyLoaded();
            }
        }

    }
}
