using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IPlacementRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PlacementRepository : DataRepositoryBase<Placement>, IPlacementRepository
    {
        protected override Placement AddEntity(IFRSContext entityContext, Placement entity)
        {
            return entityContext.Set<Placement>().Add(entity);
        }

        protected override Placement UpdateEntity(IFRSContext entityContext, Placement entity)
        {
            return (from e in entityContext.Set<Placement>()
                    where e.Placement_Id == entity.Placement_Id
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<Placement> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<Placement>()
                   select e;
        }

        protected override Placement GetEntity(IFRSContext entityContext, int Placement_Id)
        {
            var query = (from e in entityContext.Set<Placement>()
                         where e.Placement_Id == Placement_Id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        //public IEnumerable<Placement> GetPlacementByRefNo(string RefNo)
        //{
        //    using(IFRSContext entityContext = new IFRSContext())
        //    {
        //    var query = (from e in entityContext.Set<Placement>()
        //                 where e.RefNo.Contains(RefNo)
        //                 select e);

        //    return query.ToArray();
        //    }
        //}
       
    }
}