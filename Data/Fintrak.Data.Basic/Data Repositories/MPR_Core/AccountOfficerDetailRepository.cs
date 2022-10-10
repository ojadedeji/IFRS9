using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;
using Fintrak.Shared.Basic.Framework;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IAccountOfficerDetailRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AccountOfficerDetailRepository : DataRepositoryBase<AccountOfficerDetail>, IAccountOfficerDetailRepository
    {

        protected override AccountOfficerDetail AddEntity(BasicContext entityContext, AccountOfficerDetail entity)
        {
            return entityContext.Set<AccountOfficerDetail>().Add(entity);
        }

        protected override AccountOfficerDetail UpdateEntity(BasicContext entityContext, AccountOfficerDetail entity)
        {
            return (from e in entityContext.Set<AccountOfficerDetail>() 
                    where e.AccountOfficerDetailId == entity.AccountOfficerDetailId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<AccountOfficerDetail> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<AccountOfficerDetail>()
                   select e;
        }

        protected override AccountOfficerDetail GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<AccountOfficerDetail>()
                         where e.AccountOfficerDetailId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
