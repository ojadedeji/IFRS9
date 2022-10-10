using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;
using Fintrak.Shared.Basic.Framework;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IGLAdjustmentRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GLAdjustmentRepository : DataRepositoryBase<GLAdjustment>, IGLAdjustmentRepository
    {

        protected override GLAdjustment AddEntity(BasicContext entityContext, GLAdjustment entity)
        {
            return entityContext.Set<GLAdjustment>().Add(entity);
        }

        protected override GLAdjustment UpdateEntity(BasicContext entityContext, GLAdjustment entity)
        {
            return (from e in entityContext.Set<GLAdjustment>() 
                    where e.GLAdjustmentId == entity.GLAdjustmentId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<GLAdjustment> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<GLAdjustment>()
                   select e;
        }

        protected override GLAdjustment GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<GLAdjustment>()
                         where e.GLAdjustmentId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<GLAdjustmentInfo> GetGLAdjustments(AdjustmentType adjustmentType, DateTime runDate)
        {
            using (BasicContext entityContext = new BasicContext())
            {
                var query = from a in entityContext.GLAdjustmentSet
                            join b in entityContext.GLMappingSet on a.GLCode equals b.GLCode
                            join c in entityContext.CurrencySet on a.Currency equals c.Symbol
                            join d in entityContext.BranchSet on a.CompanyCode equals d.Code
                            where a.AdjustmentType == adjustmentType //&& a.RunDate == runDate.
                            select new GLAdjustmentInfo()
                            {
                                GLAdjustment = a,
                                GLMapping = b,
                                Branch = d,
                                Currency = c
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
