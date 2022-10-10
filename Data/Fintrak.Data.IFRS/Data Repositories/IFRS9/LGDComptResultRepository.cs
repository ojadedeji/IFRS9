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
    [Export(typeof(ILGDComptResultRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LGDComptResultRepository : DataRepositoryBase<LGDComptResult>, ILGDComptResultRepository
    {
        protected override LGDComptResult AddEntity(IFRSContext entityContext, LGDComptResult entity)
        {
            return entityContext.Set<LGDComptResult>().Add(entity);
        }

        protected override LGDComptResult UpdateEntity(IFRSContext entityContext, LGDComptResult entity)
        {
            return (from e in entityContext.Set<LGDComptResult>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<LGDComptResult> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<LGDComptResult>().Take(200)   //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                   select e;
        }

        protected override LGDComptResult GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<LGDComptResult>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }



/*
        public IEnumerable<LGDComptResultInfo> GetLGDComptResults()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.LGDComptResultDataSet
                            join b in entityContext.ScheduleTypeSet on a.Schedule_Type equals b.Code
                            select new LGDComptResultInfo()
                            {
                                LGDComptResult = a,
                                ScheduleType = b
                            };

                return query.ToFullyLoaded();
            }
        }

*/

        public IEnumerable<LGDComptResult> GetLGDComptResultBySearch(string searchParam)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<LGDComptResult>()
                             where e.Refno == searchParam
                             //orderby e.RefNo, e.datepmt
                             select e);

                return query.ToArray();
            }
        }

        public IEnumerable<LGDComptResult> GetLGDComptResults(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<LGDComptResult>()
                                 select new
                                 {
                                     CustID = e.AccountNo,
                                     AccountNo = e.Refno,
                                     HC1 = e.Producttype,
                                     HC2 = e.SubType,
                                     e.CustomerName,
                                     e.Currency,
                                     e.ExchangeRate,
                                     Date = e.date_pmt,
                                     e.MaturityDate,
                                     EAD = e.AmortizedCost_FCY,
                                     EAD_Trans = e.AmortizedCost_LCY,
                                     e.EIR,
                                     e.Stage,
                                     e.TotalColRecAmt,
                                     e.RecoveryRate,
                                     e.UNSecuredRecovery,
                                     e.TotalRecoverableAmt,
                                     e.LGD
                                 });
                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<LGDComptResult>().Take(defaultCount).ToArray();

                    //var query = (from e in entityContext.Set<LGDComptResult>() select e);
                    //var ExportHandler = new ExcelService();
                    //var response = ExportHandler.Export(query.ToList(), path);

                    //return query.Take(defaultCount).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<LGDComptResult>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                                 select e);

                    return query.ToArray();
                }
            }
        }



    }
}