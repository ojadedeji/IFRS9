using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.CDQM.Entities;
using Fintrak.Data.CDQM.Contracts;

namespace Fintrak.Data.CDQM
{
    [Export(typeof(ICDQMETLConfigurationRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CDQMETLConfigurationRepository : DataRepositoryBase<CDQMETLConfiguration>, ICDQMETLConfigurationRepository
    {

        protected override CDQMETLConfiguration AddEntity(CDQMContext entityContext, CDQMETLConfiguration entity)
        {
            return entityContext.Set<CDQMETLConfiguration>().Add(entity);
        }

        protected override CDQMETLConfiguration UpdateEntity(CDQMContext entityContext, CDQMETLConfiguration entity)
        {
            return (from e in entityContext.Set<CDQMETLConfiguration>() 
                    where e.ETLConfigurationId == entity.ETLConfigurationId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CDQMETLConfiguration> GetEntities(CDQMContext entityContext)
        {
            return from e in entityContext.Set<CDQMETLConfiguration>()
                   select e;
        }

        protected override CDQMETLConfiguration GetEntity(CDQMContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CDQMETLConfiguration>()
                         where e.ETLConfigurationId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
