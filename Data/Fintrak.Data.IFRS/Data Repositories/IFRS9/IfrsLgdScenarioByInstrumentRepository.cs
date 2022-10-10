using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IIfrsLgdScenarioByInstrumentRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsLgdScenarioByInstrumentRepository : DataRepositoryBase<IfrsLgdScenarioByInstrument>, IIfrsLgdScenarioByInstrumentRepository
    {
        protected override IfrsLgdScenarioByInstrument AddEntity(IFRSContext entityContext, IfrsLgdScenarioByInstrument entity)
        {
            return entityContext.Set<IfrsLgdScenarioByInstrument>().Add(entity);
        }

        protected override IfrsLgdScenarioByInstrument UpdateEntity(IFRSContext entityContext, IfrsLgdScenarioByInstrument entity)
        {
            return (from e in entityContext.Set<IfrsLgdScenarioByInstrument>()
                    where e.InstrumentId == entity.InstrumentId
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<IfrsLgdScenarioByInstrument> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<IfrsLgdScenarioByInstrument>()
                   select e;
        }

        protected override IfrsLgdScenarioByInstrument GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IfrsLgdScenarioByInstrument>()
                         where e.InstrumentId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }


        public IEnumerable<IfrsLgdScenarioByInstrument> GetEntityByInstrumentType(string id)
        {

            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.IfrsLgdScenarioByInstrumentSet
                            where a.InstrumentType == id
                            select a;

                return query.ToFullyLoaded();
            }
        }
       
    }
}