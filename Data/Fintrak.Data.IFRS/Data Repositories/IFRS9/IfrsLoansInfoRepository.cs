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
    [Export(typeof(IIfrsLoansInfoRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsLoansInfoRepository : DataRepositoryBase<IfrsLoansInfo>, IIfrsLoansInfoRepository
    {
        protected override IfrsLoansInfo AddEntity(IFRSContext entityContext, IfrsLoansInfo entity)
        {
            return entityContext.Set<IfrsLoansInfo>().Add(entity);
        }

        protected override IfrsLoansInfo UpdateEntity(IFRSContext entityContext, IfrsLoansInfo entity)
        {
            return (from e in entityContext.Set<IfrsLoansInfo>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IfrsLoansInfo> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<IfrsLoansInfo>()
                   select e;
        }

        protected override IfrsLoansInfo GetEntity(IFRSContext entityContext, int Id)
        {
            var query = (from e in entityContext.Set<IfrsLoansInfo>()
                         where e.Id == Id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IfrsLoansInfo> GetRecordByRefNo(string searchParam)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<IfrsLoansInfo>()
                             where e.Refno == searchParam                             
                             select e);

                return query.ToArray();
            }
        }

        public IEnumerable<IfrsLoansInfo> GetIfrsLoansInfos(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<IfrsLoansInfo>()
                                 select new
                                 {
                                     Id = e.Id,
                                     Refno = e.Refno,
                                     ProductType = e.ProductType,
                                     OutstandingBal = e.OutstandingBal,
                                   
                                 });
                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<IfrsLoansInfo>().Take(defaultCount).ToArray();

                    //var query = (from e in entityContext.Set<IfrsLoansInfo>() select e);
                    //var ExportHandler = new ExcelService();
                    //var response = ExportHandler.Export(query.ToList(), path);

                    //return query.Take(defaultCount).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsLoansInfo>().Take(defaultCount) select e);

                    return query.ToArray();
                }
            }
        }
    }
}
