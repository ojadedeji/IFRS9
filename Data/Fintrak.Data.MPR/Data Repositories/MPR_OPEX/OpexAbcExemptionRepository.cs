using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IOpexAbcExemptionRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OpexAbcExemptionRepository : DataRepositoryBase<OpexAbcExemption>, IOpexAbcExemptionRepository
    {

        protected override OpexAbcExemption AddEntity(MPRContext entityContext, OpexAbcExemption entity)
        {
            return entityContext.Set<OpexAbcExemption>().Add(entity);
        }

        protected override OpexAbcExemption UpdateEntity(MPRContext entityContext, OpexAbcExemption entity)
        {
            return (from e in entityContext.Set<OpexAbcExemption>()
                    where e.OpexAbcExemptionId == entity.OpexAbcExemptionId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<OpexAbcExemption> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<OpexAbcExemption>()
                   select e;
        }

        protected override OpexAbcExemption GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<OpexAbcExemption>()
                         where e.OpexAbcExemptionId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<OpexAbcExemptionInfo> GetOpexAbcExemptions()
        {
            using (MPRContext entityContext = new MPRContext())
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
