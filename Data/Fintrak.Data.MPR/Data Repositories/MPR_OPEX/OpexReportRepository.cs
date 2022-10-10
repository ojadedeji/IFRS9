using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IOpexReportRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OpexReportRepository : DataRepositoryBase<OpexReport>, IOpexReportRepository
    {

        protected override OpexReport AddEntity(MPRContext entityContext, OpexReport entity)
        {
            return entityContext.Set<OpexReport>().Add(entity);
        }

        protected override OpexReport UpdateEntity(MPRContext entityContext, OpexReport entity)
        {
            return (from e in entityContext.Set<OpexReport>()
                    where e.ReportId == entity.ReportId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<OpexReport> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<OpexReport>()
                   select e;
        }

        protected override OpexReport GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<OpexReport>()
                         where e.ReportId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<OpexReportInfo> GetOpexReports()
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.OpexReportSet
                            join b in entityContext.GLDefinitionSet on a.GLCode equals b.GL_Code into parents
                            from pb in parents.DefaultIfEmpty()
                            join c in entityContext.BranchSet on a.BranchCode equals c.Code into cparents
                            from pc in cparents.DefaultIfEmpty()
                            select new OpexReportInfo()
                            {
                                OpexReport = a,
                                GLDefinition = pb,
                                Branch = pc
                            };

                return query.ToFullyLoaded();
            }
        }

    }
}
