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
    [Export(typeof(IRegressionCofficientRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RegressionCofficientRepository : DataRepositoryBase<RegressionCofficient>, IRegressionCofficientRepository
    {
        protected override RegressionCofficient AddEntity(IFRSContext entityContext, RegressionCofficient entity)
        {
            return entityContext.Set<RegressionCofficient>().Add(entity);
        }

        protected override RegressionCofficient UpdateEntity(IFRSContext entityContext, RegressionCofficient entity)
        {
            return (from e in entityContext.Set<RegressionCofficient>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<RegressionCofficient> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<RegressionCofficient>()
                   select e;
        }

        protected override RegressionCofficient GetEntity(IFRSContext entityContext, int Id)
        {
            var query = (from e in entityContext.Set<RegressionCofficient>()
                         where e.ID == Id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<RegressionCofficient> GetRecordByRefNo(string searchParam)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<RegressionCofficient>()
                             //where e.ID == searchParam
                             //orderby e.date_pmt
                             select e);

                return query.ToArray();
            }
        }

        public IEnumerable<RegressionCofficient> GetRegressionCofficients(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<RegressionCofficient>()
                                 select new
                                 {
                                     CustID = e.ID,
                                     //DefaultParam = e.DefaultParam,
                                     //DaysPastDue = e.DaysPastDue

                                 });
                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<RegressionCofficient>().Take(defaultCount).ToArray();

                    //var query = (from e in entityContext.Set<RegressionCofficient>() select e);
                    //var ExportHandler = new ExcelService();
                    //var response = ExportHandler.Export(query.ToList(), path);

                    //return query.Take(defaultCount).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<RegressionCofficient>().Take(defaultCount) select e);

                    return query.ToArray();
                }
            }
        }
    }
}

