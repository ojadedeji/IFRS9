using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IOffBalancesheetECLRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OffBalancesheetECLRepository : DataRepositoryBase<OffBalancesheetECL>, IOffBalancesheetECLRepository
    {
        protected override OffBalancesheetECL AddEntity(IFRSContext entityContext, OffBalancesheetECL entity)
        {
            return entityContext.Set<OffBalancesheetECL>().Add(entity);
        }

        protected override OffBalancesheetECL UpdateEntity(IFRSContext entityContext, OffBalancesheetECL entity)
        {
            return (from e in entityContext.Set<OffBalancesheetECL>()
                    where e.obe_Id == entity.obe_Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<OffBalancesheetECL> GetEntities(IFRSContext entityContext)
        {
            var query = from e in entityContext.Set<OffBalancesheetECL>()
                        select e;
            query = query.OrderBy(a => a.obe_Id).GroupBy(e => e.Account_No).Select(a => a.FirstOrDefault()).Take(500);
            return query;
        }

        protected override OffBalancesheetECL GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<OffBalancesheetECL>()
                         where e.obe_Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<OffBalancesheetECL> GetEntityBySearchParam(string SearchParam)
        {

            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<OffBalancesheetECL>()
                            where e.Account_No == SearchParam || e.CustomerName.Contains(SearchParam)
                            select e);

                return query.ToArray();
            }
        }

        public IEnumerable<OffBalancesheetECL> GetOffBalancesheetECLs(int defaultCount)
        {

            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from e in entityContext.Set<OffBalancesheetECL>()
                            select e;
                query = query.OrderBy(a => a.obe_Id).GroupBy(e => e.Account_No).Select(a => a.FirstOrDefault()).Take(defaultCount);
                return query;
            }
        }
       
    }
}