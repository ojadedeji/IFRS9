using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;
using Fintrak.Shared.IFRS.Framework;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IGLAdjustmentRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GLAdjustmentRepository : DataRepositoryBase<GLAdjustment>, IGLAdjustmentRepository
    {

        protected override GLAdjustment AddEntity(IFRSContext entityContext, GLAdjustment entity)
        {
            return entityContext.Set<GLAdjustment>().Add(entity);
        }

        protected override GLAdjustment UpdateEntity(IFRSContext entityContext, GLAdjustment entity)
        {
            return (from e in entityContext.Set<GLAdjustment>()
                    where e.GLAdjustmentId == entity.GLAdjustmentId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<GLAdjustment> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<GLAdjustment>()
                   select e;
        }

        protected override GLAdjustment GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<GLAdjustment>()
                         where e.GLAdjustmentId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<GLAdjustmentInfo> GetGLAdjustments(AdjustmentType adjustmentType, ReportType reportType, DateTime runDate)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.GLAdjustmentSet
                            join c in entityContext.CurrencySet on a.Currency equals c.Symbol
                            join d in entityContext.BranchSet on a.CompanyCode equals d.Code into ad
                       
                            from adi in ad.DefaultIfEmpty()
                             
                            where a.AdjustmentType == adjustmentType && a.RunDate == runDate && a.ReportType == reportType
                            select new GLAdjustmentInfo()
                            {
                                GLAdjustment = a,
                             //   GLMapping = b,
                                Branch = adi,
                                Currency = c
                            };

                return query.ToFullyLoaded();
            }
        }

    }
}
