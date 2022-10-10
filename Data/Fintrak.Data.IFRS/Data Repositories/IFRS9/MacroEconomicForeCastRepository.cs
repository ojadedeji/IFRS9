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
    [Export(typeof(IMacroEconomicForeCastRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MacroEconomicForeCastRepository : DataRepositoryBase<MacroEconomicForeCast>, IMacroEconomicForeCastRepository
    {
        protected override MacroEconomicForeCast AddEntity(IFRSContext entityContext, MacroEconomicForeCast entity)
        {
            return entityContext.Set<MacroEconomicForeCast>().Add(entity);
        }

        protected override MacroEconomicForeCast UpdateEntity(IFRSContext entityContext, MacroEconomicForeCast entity)
        {
            return (from e in entityContext.Set<MacroEconomicForeCast>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<MacroEconomicForeCast> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<MacroEconomicForeCast>()
                   select e;
        }

        protected override MacroEconomicForeCast GetEntity(IFRSContext entityContext, int Id)
        {
            var query = (from e in entityContext.Set<MacroEconomicForeCast>()
                         where e.ID == Id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        //    public IEnumerable<MacroEconomicForeCast> GetRecordByRefNo(string searchParam)
        //    {
        //        using (IFRSContext entityContext = new IFRSContext())
        //        {
        //            var query = (from e in entityContext.Set<MacroEconomicForeCast>()
        //                         where e.Refno == searchParam || e.AccountNo == searchParam
        //                         orderby e.date_pmt
        //                         select e);

        //            return query.ToArray();
        //        }
        //    }

        //    public IEnumerable<MacroEconomicForeCast> GetMacroEconomicForeCasts(int defaultCount, string path)
        //    {
        //        using (IFRSContext entityContext = new IFRSContext())
        //        {
        //            if (!string.IsNullOrEmpty(path))
        //            {
        //                var query = (from e in entityContext.Set<MacroEconomicForeCast>()
        //                             select new
        //                             {
        //                                 CustID = e.AccountNo,
        //                                 AccountNo = e.Refno,
        //                                 HC1 = e.Producttype,
        //                                 HC2 = e.SubType,
        //                                 e.CustomerName,
        //                                 e.Currency,
        //                                 e.ExchangeRate,
        //                                 Date = e.date_pmt,
        //                                 e.MaturityDate,
        //                                 EAD = e.AmortizedCost_FCY,
        //                                 EAD_Trans = e.AmortizedCost_LCY,
        //                                 e.EIR,
        //                                 e.Stage,
        //                                 e.TotalColRecAmt,
        //                                 e.RecoveryRate,
        //                                 e.UNSecuredRecovery,
        //                                 e.TotalRecoverableAmt,
        //                                 e.LGD
        //                             });
        //                var ExportHandler = new ExcelService();
        //                var response = ExportHandler.Export(query.ToList(), path);

        //                return new List<MacroEconomicForeCast>().Take(defaultCount).ToArray();

        //                //var query = (from e in entityContext.Set<MacroEconomicForeCast>() select e);
        //                //var ExportHandler = new ExcelService();
        //                //var response = ExportHandler.Export(query.ToList(), path);

        //                //return query.Take(defaultCount).ToArray();
        //            }
        //            else
        //            {
        //                var query = (from e in entityContext.Set<MacroEconomicForeCast>().Take(defaultCount) select e);

        //                return query.ToArray();
        //            }
        //        }
        //    }
        //}

    }
}
