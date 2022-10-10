using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IInputDetailRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class InputDetailRepository : DataRepositoryBase<InputDetail>, IInputDetailRepository
    {
        protected override InputDetail AddEntity(IFRSContext entityContext, InputDetail entity)
        {
            return entityContext.Set<InputDetail>().Add(entity);
        }

        protected override InputDetail UpdateEntity(IFRSContext entityContext, InputDetail entity)
        {
            return (from e in entityContext.Set<InputDetail>()
                    where e.InputDetailId == entity.InputDetailId
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<InputDetail> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<InputDetail>()
                   select e;
        }

        protected override InputDetail GetEntity(IFRSContext entityContext, int InputDetailId)
        {
            var query = (from e in entityContext.Set<InputDetail>()
                         where e.InputDetailId == InputDetailId
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<EclWeightedAvg> GetEclWeightedAvgs()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<EclWeightedAvg>()
                             select e);

                return query.ToArray();
            }
        }

        //public IEnumerable<InputDetail> GetInputDetailByRefNo(string RefNo)
        //{
        //    using(IFRSContext entityContext = new IFRSContext())
        //    {
        //    var query = (from e in entityContext.Set<InputDetail>()
        //                 where e.RefNo.Contains(RefNo)
        //                 select e);

        //    return query.ToArray();
        //    }
        //}

    }
}