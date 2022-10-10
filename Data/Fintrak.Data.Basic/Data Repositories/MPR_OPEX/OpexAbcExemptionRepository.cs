using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IOpexAbcExemptionRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OpexAbcExemptionRepository : DataRepositoryBase<OpexAbcExemption>, IOpexAbcExemptionRepository
    {

        protected override OpexAbcExemption AddEntity(BasicContext entityContext, OpexAbcExemption entity)
        {
            return entityContext.Set<OpexAbcExemption>().Add(entity);
        }

        protected override OpexAbcExemption UpdateEntity(BasicContext entityContext, OpexAbcExemption entity)
        {
            return (from e in entityContext.Set<OpexAbcExemption>()
                    where e.OpexAbcExemptionId == entity.OpexAbcExemptionId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<OpexAbcExemption> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<OpexAbcExemption>()
                   select e;
        }

        protected override OpexAbcExemption GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<OpexAbcExemption>()
                         where e.OpexAbcExemptionId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<OpexAbcExemptionInfo> GetOpexAbcExemptions()
        {
            using (BasicContext entityContext = new BasicContext())
            {
                var query = from a in entityContext.OpexAbcExemptionSet
                            join b in entityContext.CostCentreSet on a.MisCode equals b.Code
                            select new OpexAbcExemptionInfo()
                            {
                                 OpexAbcExemption = a,
                                 CostCentre = b
                            };

                return query.ToFullyLoaded();
            }
        }

      
    }
}
