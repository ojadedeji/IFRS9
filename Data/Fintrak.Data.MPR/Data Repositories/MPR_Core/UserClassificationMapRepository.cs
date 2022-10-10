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
    [Export(typeof(IUserClassificationMapRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UserClassificationMapRepository : DataRepositoryBase<UserClassificationMap>, IUserClassificationMapRepository
    {

        protected override UserClassificationMap AddEntity(MPRContext entityContext, UserClassificationMap entity)
        {
            return entityContext.Set<UserClassificationMap>().Add(entity);
        }

        protected override UserClassificationMap UpdateEntity(MPRContext entityContext, UserClassificationMap entity)
        {
            return (from e in entityContext.Set<UserClassificationMap>() 
                    where e.UserClassificationMapId == entity.UserClassificationMapId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<UserClassificationMap> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<UserClassificationMap>()
                   select e;
        }

        protected override UserClassificationMap GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<UserClassificationMap>()
                         where e.UserClassificationMapId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<UserClassificationMap> GetUserClassificationMaps(string loginID)
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.UserClassificationMapSet
                            where a.LoginID == loginID
                            select a;
                            
                return query.ToFullyLoaded();
            }
        }
      
    }
}
