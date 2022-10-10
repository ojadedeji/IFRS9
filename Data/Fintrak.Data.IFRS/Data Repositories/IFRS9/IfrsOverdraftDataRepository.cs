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
    [Export(typeof(IIfrsOverdraftDataRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsOverdraftDataRepository : DataRepositoryBase<IfrsOverdraftData>, IIfrsOverdraftDataRepository
    {
        protected override IfrsOverdraftData AddEntity(IFRSContext entityContext, IfrsOverdraftData entity)
        {
            return entityContext.Set<IfrsOverdraftData>().Add(entity);
        }

        protected override IfrsOverdraftData UpdateEntity(IFRSContext entityContext, IfrsOverdraftData entity)
        {
            return (from e in entityContext.Set<IfrsOverdraftData>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IfrsOverdraftData> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<IfrsOverdraftData>()
                   select e;
        }

        protected override IfrsOverdraftData GetEntity(IFRSContext entityContext, int Id)
        {
            var query = (from e in entityContext.Set<IfrsOverdraftData>()
                         where e.ID == Id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IfrsOverdraftData> GetRecordByRefNo(string searchParam)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<IfrsOverdraftData>()
                             where e.RefNo == searchParam || e.AccountNo == searchParam
                           
                             select e);

                return query.ToArray();
            }
        }

        public IEnumerable<IfrsOverdraftData> GetIfrsOverdraftData (int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<IfrsOverdraftData>()
                                 select new
                                 {
                                     AccountNo = e.AccountNo,
                                     RefNo = e.RefNo,
                                     CustomerName = e.CustomerName,
                                     HC1= e.ProductType,
                                     HC2= e.SubType,
                                     Valuedate= e.ValueDate,
                                     MaturityDate= e.MaturityDate,
                                     Currency = e.Currency,
                                     ODLimit = e.ODLimit,
                                     DrawnAmout = e.DrawnAmount,
                                     Rate = e.Rate,
                                     Stage=  e.Stage
                                     
                                 });
                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<IfrsOverdraftData>().Take(defaultCount).ToArray();

                    //var query = (from e in entityContext.Set<IfrsOverdraftData>() select e);
                    //var ExportHandler = new ExcelService();
                    //var response = ExportHandler.Export(query.ToList(), path);

                    //return query.Take(defaultCount).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsOverdraftData>().Take(defaultCount) select e);

                    return query.ToArray();
                }
            }
        }
    }
}
