using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IMPRCommFeeRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MPRCommFeeRepository : DataRepositoryBase<MPRCommFee>, IMPRCommFeeRepository
    {

        protected override MPRCommFee AddEntity(MPRContext entityContext, MPRCommFee entity)
        {
            return entityContext.Set<MPRCommFee>().Add(entity);
        }

        protected override MPRCommFee UpdateEntity(MPRContext entityContext, MPRCommFee entity)
        {
            return (from e in entityContext.Set<MPRCommFee>()
                    where e.CommFee_Id == entity.CommFee_Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<MPRCommFee> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<MPRCommFee>()
                   select e;
        }

        protected override MPRCommFee GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<MPRCommFee>()
                         where e.CommFee_Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }


        public IEnumerable<MPRCommFee> GetMPRCommFeesBySearch(string SearchValue)
        {

            using (MPRContext entityContext = new MPRContext())
            {
                var query = (from e in entityContext.Set<MPRCommFee>()
                             where e.RelatedAccount.Contains(SearchValue) || 
                                    e.CustomerName.Contains(SearchValue) || 
                                    e.GL_Code.Contains(SearchValue)
                             select e);

                return query.ToList();

            }
        }

    }
}
