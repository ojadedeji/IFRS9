using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;
using Fintrak.Shared.MPR.Framework;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IUserMISRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UserMISRepository : DataRepositoryBase<UserMIS>, IUserMISRepository
    {

        protected override UserMIS AddEntity(MPRContext entityContext, UserMIS entity)
        {
            return entityContext.Set<UserMIS>().Add(entity);
        }

        protected override UserMIS UpdateEntity(MPRContext entityContext, UserMIS entity)
        {
            return (from e in entityContext.Set<UserMIS>() 
                    where e.UserMISId == entity.UserMISId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<UserMIS> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<UserMIS>()
                   select e;
        }

        protected override UserMIS GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<UserMIS>()
                         where e.UserMISId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
