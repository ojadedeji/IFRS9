using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(ICheckListRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CheckListRepository : DataRepositoryBase<CheckList>, ICheckListRepository
    {

        protected override CheckList AddEntity(MPRContext entityContext, CheckList entity)
        {
            return entityContext.Set<CheckList>().Add(entity);
        }

        protected override CheckList UpdateEntity(MPRContext entityContext, CheckList entity)
        {
            return (from e in entityContext.Set<CheckList>()
                    where e.CheckListId == entity.CheckListId
                    select e).FirstOrDefault();
        }

        protected override CheckList GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CheckList>()
                         where e.CheckListId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    
        protected override IEnumerable<CheckList> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<CheckList>()
                   select e;
        }

       

      
    }
}
