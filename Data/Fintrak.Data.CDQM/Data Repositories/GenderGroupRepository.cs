using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.CDQM.Entities;
using Fintrak.Data.CDQM.Contracts;

namespace Fintrak.Data.CDQM
{
    [Export(typeof(ICDQMGenderGroupRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CDQMGenderGroupRepository : DataRepositoryBase<CDQMGenderGroup>, ICDQMGenderGroupRepository
    {

        protected override CDQMGenderGroup AddEntity(CDQMContext entityContext, CDQMGenderGroup entity)
        {
            return entityContext.Set<CDQMGenderGroup>().Add(entity);
        }

        protected override CDQMGenderGroup UpdateEntity(CDQMContext entityContext, CDQMGenderGroup entity)
        {
            return (from e in entityContext.Set<CDQMGenderGroup>() 
                    where e.GenderGroupId == entity.GenderGroupId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CDQMGenderGroup> GetEntities(CDQMContext entityContext)
        {
            return from e in entityContext.Set<CDQMGenderGroup>()
                   select e;
        }

        protected override CDQMGenderGroup GetEntity(CDQMContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CDQMGenderGroup>()
                         where e.GenderGroupId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
