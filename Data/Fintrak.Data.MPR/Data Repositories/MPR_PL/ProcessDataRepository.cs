using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IProcessDataRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProcessDataRepository : DataRepositoryBase<ProcessData>, IProcessDataRepository
    {

        protected override ProcessData AddEntity(MPRContext entityContext, ProcessData entity)
        {
            return entityContext.Set<ProcessData>().Add(entity);
        }

        protected override ProcessData UpdateEntity(MPRContext entityContext, ProcessData entity)
        {
            return (from e in entityContext.Set<ProcessData>()
                    where e.ProcessDataId == entity.ProcessDataId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ProcessData> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<ProcessData>()
                   select e;
        }

        protected override ProcessData GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ProcessData>()
                         where e.ProcessDataId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }


        //public IEnumerable<ProcessData> GetProcessDatas(string year)
        //{
        //    using (MPRContext entityContext = new MPRContext())
        //    {
        //        var query = from a in entityContext.ProcessDataSet
        //                    where a.Year == year
        //                    select a;

        //        return query.ToFullyLoaded();
        //    }
        //}

      
    }
}
