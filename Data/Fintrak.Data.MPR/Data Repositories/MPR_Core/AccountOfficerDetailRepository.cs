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
    [Export(typeof(IAccountOfficerDetailRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AccountOfficerDetailRepository : DataRepositoryBase<AccountOfficerDetail>, IAccountOfficerDetailRepository
    {

        protected override AccountOfficerDetail AddEntity(MPRContext entityContext, AccountOfficerDetail entity)
        {
            return entityContext.Set<AccountOfficerDetail>().Add(entity);
        }

        protected override AccountOfficerDetail UpdateEntity(MPRContext entityContext, AccountOfficerDetail entity)
        {
            return (from e in entityContext.Set<AccountOfficerDetail>() 
                    where e.AccountOfficerDetailId == entity.AccountOfficerDetailId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<AccountOfficerDetail> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<AccountOfficerDetail>()
                   select e;
        }

        protected override AccountOfficerDetail GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<AccountOfficerDetail>()
                         where e.AccountOfficerDetailId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
