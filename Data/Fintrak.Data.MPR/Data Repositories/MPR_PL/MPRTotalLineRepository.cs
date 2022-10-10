using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IMPRTotalLineRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MPRTotalLineRepository : DataRepositoryBase<MPRTotalLine>, IMPRTotalLineRepository
    {

        protected override MPRTotalLine AddEntity(MPRContext entityContext, MPRTotalLine entity)
        {
            return entityContext.Set<MPRTotalLine>().Add(entity);
        }

        protected override MPRTotalLine UpdateEntity(MPRContext entityContext, MPRTotalLine entity)
        {
            return (from e in entityContext.Set<MPRTotalLine>()
                    where e.TotallineId == entity.TotallineId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<MPRTotalLine> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<MPRTotalLine>()
                   select e;
        }

        protected override MPRTotalLine GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<MPRTotalLine>()
                         where e.TotallineId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<MPRTotalLineInfo> GetMPRTotalLines()
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.MPRTotalLineSet
                            join b in entityContext.MPRTotalLineSet on a.ParentId equals b.TotallineId into parents
                            from pt in parents.DefaultIfEmpty()
                            select new MPRTotalLineInfo()
                            {
                                MPRTotalLine = a,
                                Parent = pt
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
