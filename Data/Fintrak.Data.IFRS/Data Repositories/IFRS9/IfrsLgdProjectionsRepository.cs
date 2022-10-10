using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;
using Fintrak.Shared.Common.Services;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IIfrsLgdProjectionsRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsLgdProjectionsRepository : DataRepositoryBase<IfrsLgdProjections>, IIfrsLgdProjectionsRepository
    {
        protected override IfrsLgdProjections AddEntity(IFRSContext entityContext, IfrsLgdProjections entity)
        {
            return entityContext.Set<IfrsLgdProjections>().Add(entity);
        }

        protected override IfrsLgdProjections UpdateEntity(IFRSContext entityContext, IfrsLgdProjections entity)
        {
            return (from e in entityContext.Set<IfrsLgdProjections>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IfrsLgdProjections> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<IfrsLgdProjections>()
                   select e;
        }

        protected override IfrsLgdProjections GetEntity(IFRSContext entityContext, int Id)
        {
            var query = (from e in entityContext.Set<IfrsLgdProjections>()
                         where e.ID == Id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IfrsLgdProjections> GetRecordByRefNo(string searchParam)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<IfrsLgdProjections>()
                             where e.Refno == searchParam || e.AccountNo == searchParam
                           
                             select e);

                return query.ToArray();
            }
        }

        public IEnumerable<IfrsLgdProjections> GetIfrsLgdProjections (int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<IfrsLgdProjections>()
                                 select new
                                 {
                                     ID = e.AccountNo,
                                     RefNo = e.Refno
                                   
                                 });
                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<IfrsLgdProjections>().Take(defaultCount).ToArray();

                    //var query = (from e in entityContext.Set<IfrsLgdProjections>() select e);
                    //var ExportHandler = new ExcelService();
                    //var response = ExportHandler.Export(query.ToList(), path);

                    //return query.Take(defaultCount).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsLgdProjections>().Take(defaultCount) select e);

                    return query.ToArray();
                }
            }
        }
    }
}
