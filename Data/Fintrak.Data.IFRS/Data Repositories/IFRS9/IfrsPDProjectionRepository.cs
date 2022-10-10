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
    [Export(typeof(IIfrsPDProjectionRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsPDProjectionRepository : DataRepositoryBase<IfrsPDProjection>, IIfrsPDProjectionRepository
    {
        protected override IfrsPDProjection AddEntity(IFRSContext entityContext, IfrsPDProjection entity)
        {
            return entityContext.Set<IfrsPDProjection>().Add(entity);
        }

        protected override IfrsPDProjection UpdateEntity(IFRSContext entityContext, IfrsPDProjection entity)
        {
            return (from e in entityContext.Set<IfrsPDProjection>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IfrsPDProjection> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<IfrsPDProjection>()
                   select e;
        }

        protected override IfrsPDProjection GetEntity(IFRSContext entityContext, int Id)
        {
            var query = (from e in entityContext.Set<IfrsPDProjection>()
                         where e.ID == Id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IfrsPDProjection> GetRecordByRefNo(string searchParam)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<IfrsPDProjection>()
                             where e.Refno == searchParam || e.AccountNo == searchParam
                             orderby e.date_pmt
                             select e);

                return query.ToArray();
            }
        }

        public IEnumerable<IfrsPDProjection> GetIfrsPDProjections(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<IfrsPDProjection>()
                                 select new
                                 {
                                     ID = e.ID,
                                     AccountNo = e.AccountNo,
                                     RefNo = e.Refno,
                                     ProductName = e.ProductName
                                  
                                 });
                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<IfrsPDProjection>().Take(defaultCount).ToArray();

                    //var query = (from e in entityContext.Set<IfrsPDProjection>() select e);
                    //var ExportHandler = new ExcelService();
                    //var response = ExportHandler.Export(query.ToList(), path);

                    //return query.Take(defaultCount).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsPDProjection>().Take(defaultCount) select e);

                    return query.ToArray();
                }
            }
        }
    }
}
