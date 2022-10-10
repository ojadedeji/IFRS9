using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IMemoAccountMapRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MemoAccountMapRepository : DataRepositoryBase<MemoAccountMap>, IMemoAccountMapRepository
    {

        protected override MemoAccountMap AddEntity(BasicContext entityContext, MemoAccountMap entity)
        {
            return entityContext.Set<MemoAccountMap>().Add(entity);
        }

        protected override MemoAccountMap UpdateEntity(BasicContext entityContext, MemoAccountMap entity)
        {
            return (from e in entityContext.Set<MemoAccountMap>() 
                    where e.MemoAccountMapId == entity.MemoAccountMapId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<MemoAccountMap> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<MemoAccountMap>()
                   select e;
        }

        protected override MemoAccountMap GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<MemoAccountMap>()
                         where e.MemoAccountMapId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<MemoAccountMapInfo> GetMemoAccountMaps()
        {
            using (BasicContext entityContext = new BasicContext())
            {
                var query = from a in entityContext.MemoAccountMapSet
                            //join b in entityContext.CustAccountSet on a.AccountNo equals b.AccountNo
                            join b in entityContext.CustAccountSet on a.AccountNo equals b.AccountNo into bparents
                            from bp in bparents.DefaultIfEmpty()
                            join c in entityContext.MemoUnitsSet on a.Code equals c.Code into cparents
                            from cp in cparents.DefaultIfEmpty()

                            select new MemoAccountMapInfo()
                            {
                                MemoAccountMap = a,
                                CustAccount = bp,
                                MemoUnits = cp,
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
