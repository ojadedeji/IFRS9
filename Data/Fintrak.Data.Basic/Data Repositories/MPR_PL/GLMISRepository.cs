using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{

    [Export(typeof(IGLMISRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GLMISRepository : DataRepositoryBase<GLMIS>, IGLMISRepository
    {

        protected override GLMIS AddEntity(BasicContext entityContext, GLMIS entity)
        {
            return entityContext.Set<GLMIS>().Add(entity);
        }

        protected override GLMIS UpdateEntity(BasicContext entityContext, GLMIS entity)
        {
            return (from e in entityContext.Set<GLMIS>() 
                    where e.GlMisId == entity.GlMisId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<GLMIS> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<GLMIS>()
                   select e;
        }

        protected override GLMIS GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<GLMIS>()
                         where e.GlMisId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<GLMISInfo> GetGLMIS()
        {
            using (BasicContext entityContext = new BasicContext())
            {
                var query = from a in entityContext.GLMISSet
                            join c in entityContext.TeamDefinitionSet on a.TeamDefinitionCode equals c.Code
                            join d in entityContext.TeamSet on a.TeamCode equals d.Code
                            join e in entityContext.TeamDefinitionSet on a.AccountOfficerDefinitionCode equals e.Code
                            join f in entityContext.TeamSet on a.AccountOfficerCode equals f.Code
                            select new GLMISInfo()
                            {
                                GLMIS = a,
                                TeamDefinition = c,
                                Team = d,
                                AccountOfficerDefinition = e,
                                AccountOfficer = f
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
