using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.CDQM.Entities;
using Fintrak.Data.CDQM.Contracts;

namespace Fintrak.Data.CDQM
{
    [Export(typeof(ICDQMTitleRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CDQMTitleRepository : DataRepositoryBase<CDQMTitle>, ICDQMTitleRepository
    {

        protected override CDQMTitle AddEntity(CDQMContext entityContext, CDQMTitle entity)
        {
            return entityContext.Set<CDQMTitle>().Add(entity);
        }

        protected override CDQMTitle UpdateEntity(CDQMContext entityContext, CDQMTitle entity)
        {
            return (from e in entityContext.Set<CDQMTitle>() 
                    where e.TitleId == entity.TitleId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CDQMTitle> GetEntities(CDQMContext entityContext)
        {
            return from e in entityContext.Set<CDQMTitle>()
                   select e;
        }

        protected override CDQMTitle GetEntity(CDQMContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CDQMTitle>()
                         where e.TitleId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
