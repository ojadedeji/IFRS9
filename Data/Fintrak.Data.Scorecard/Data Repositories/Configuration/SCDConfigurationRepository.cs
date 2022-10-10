using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Scorecard.Entities;
using Fintrak.Data.Scorecard.Contracts;

namespace Fintrak.Data.Scorecard
{
    [Export(typeof(ISCDConfigurationRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SCDConfigurationRepository : DataRepositoryBase<SCDConfiguration>, ISCDConfigurationRepository
    {

        protected override SCDConfiguration AddEntity(ScorecardContext entityContext, SCDConfiguration entity)
        {
            return entityContext.Set<SCDConfiguration>().Add(entity);
        }

        protected override SCDConfiguration UpdateEntity(ScorecardContext entityContext, SCDConfiguration entity)
        {
            return (from e in entityContext.Set<SCDConfiguration>()
                    where e.ConfigurationId == entity.ConfigurationId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<SCDConfiguration> GetEntities(ScorecardContext entityContext)
        {
            return from e in entityContext.Set<SCDConfiguration>()
                   select e;
        }

        protected override SCDConfiguration GetEntity(ScorecardContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<SCDConfiguration>()
                         where e.ConfigurationId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<SCDConfigurationInfo> GetSCDConfigurations()
        {
            using (ScorecardContext entityContext = new ScorecardContext())
            {
                var query = from a in entityContext.SCDConfigurationSet
                           
                            select new SCDConfigurationInfo()
                            {
                                SCDConfiguration = a
                            };

                return query.ToFullyLoaded();
            }
        }

    }
}
