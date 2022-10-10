using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IOpexMISReplacementRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OpexMISReplacementRepository : DataRepositoryBase<OpexMISReplacement>, IOpexMISReplacementRepository
    {

        protected override OpexMISReplacement AddEntity(BasicContext entityContext, OpexMISReplacement entity)
        {
            return entityContext.Set<OpexMISReplacement>().Add(entity);
        }

        protected override OpexMISReplacement UpdateEntity(BasicContext entityContext, OpexMISReplacement entity)
        {
            return (from e in entityContext.Set<OpexMISReplacement>()
                    where e.OpexMISReplacementId == entity.OpexMISReplacementId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<OpexMISReplacement> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<OpexMISReplacement>()
                   select e;
        }

        protected override OpexMISReplacement GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<OpexMISReplacement>()
                         where e.OpexMISReplacementId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<OpexMISReplacementInfo> GetOpexMISReplacements()
        {
            using (BasicContext entityContext = new BasicContext())
            {
                var query = from a in entityContext.OpexMISReplacementSet
                            join b in entityContext.CostCentreSet on a.MISCode equals b.Code
                            select new OpexMISReplacementInfo()
                            {
                                OpexMISReplacement = a,
                                CostCentre = b
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
