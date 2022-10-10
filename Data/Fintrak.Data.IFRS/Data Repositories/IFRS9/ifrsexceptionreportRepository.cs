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
    [Export(typeof(IifrsexceptionreportRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ifrsexceptionreportRepository : DataRepositoryBase<ifrsexceptionreport>, IifrsexceptionreportRepository
    {
        protected override ifrsexceptionreport AddEntity(IFRSContext entityContext, ifrsexceptionreport entity)
        {
            return entityContext.Set<ifrsexceptionreport>().Add(entity);
        }

        protected override ifrsexceptionreport UpdateEntity(IFRSContext entityContext, ifrsexceptionreport entity)
        {
            return (from e in entityContext.Set<ifrsexceptionreport>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ifrsexceptionreport> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<ifrsexceptionreport>()
                   select e;
        }

        protected override ifrsexceptionreport GetEntity(IFRSContext entityContext, int Id)
        {
            var query = (from e in entityContext.Set<ifrsexceptionreport>()
                         where e.Id == Id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ifrsexceptionreport> GetRecordByRefNo(string searchParam)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<ifrsexceptionreport>()
                             where e.RefNo == searchParam                             
                             select e);

                return query.ToArray();
            }
        }



        public IEnumerable<ifrsexceptionreport> GetException()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<ifrsexceptionreport>()
                             //where e.RefNo == searchParam
                             select e);

                return query.ToArray();
            }
        }


        public IEnumerable<ifrsexceptionreport> Getifrsexceptionreport(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<ifrsexceptionreport>()
                                 select new
                                 {
                                     Id = e.Id,
                                     Refno = e.RefNo,
                                     NorminalRate= e.NorminalRate,
                                     EIR = e.EIR,
                                     ExceptionType = e.ExceptionType,
                                     Classification = e.Classification,
                                     RunDate= e.RunDate
                                 });
                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<ifrsexceptionreport>().Take(defaultCount).ToArray();

                    //var query = (from e in entityContext.Set<ifrsexceptionreport>() select e);
                    //var ExportHandler = new ExcelService();
                    //var response = ExportHandler.Export(query.ToList(), path);

                    //return query.Take(defaultCount).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<ifrsexceptionreport>().Take(defaultCount) select e);

                    return query.ToArray();
                }
            }
        }

        public IEnumerable<ifrsexceptionreport> Getifrsexceptionreports(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<ifrsexceptionreport>()
                                 select new
                                 {
                                     Id = e.Id,
                                     Refno = e.RefNo,
                                     NorminalRate = e.NorminalRate,
                                     EIR = e.EIR,
                                     ExceptionType = e.ExceptionType,
                                     Classification = e.Classification,
                                     RunDate = e.RunDate

                                 });
                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<ifrsexceptionreport>().Take(defaultCount).ToArray();

                    //var query = (from e in entityContext.Set<ifrsexceptionreport>() select e);
                    //var ExportHandler = new ExcelService();
                    //var response = ExportHandler.Export(query.ToList(), path);

                    //return query.Take(defaultCount).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<ifrsexceptionreport>().Take(defaultCount) select e);

                    return query.ToArray();
                }
            }
        }
    }
}
