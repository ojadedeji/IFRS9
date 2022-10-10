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
    [Export(typeof(IOBExposureCCFRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OBExposureCCFRepository : DataRepositoryBase<OBExposureCCF>, IOBExposureCCFRepository
    {
        protected override OBExposureCCF AddEntity(IFRSContext entityContext, OBExposureCCF entity)
        {
            return entityContext.Set<OBExposureCCF>().Add(entity);
        }

        protected override OBExposureCCF UpdateEntity(IFRSContext entityContext, OBExposureCCF entity)
        {
            return (from e in entityContext.Set<OBExposureCCF>()
                    where e.obe_Id == entity.obe_Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<OBExposureCCF> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<OBExposureCCF>().Take(200)
                   select e;
        }

        protected override OBExposureCCF GetEntity(IFRSContext entityContext, int obe_Id)
        {
            var query = (from e in entityContext.Set<OBExposureCCF>()
                         where e.obe_Id == obe_Id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<OBExposureCCF> GetOBExposureCCFBySearch(int flag,string searchParam)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<OBExposureCCF>()
                             where e.RefNo == searchParam && e.Flag == flag
                             select e);

                return query.ToArray();
            }
        }

        public IEnumerable<OBExposureCCF> GetOBExposureCCF(int flag,int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    if (flag == 3)
                    {
                        var query = (from e in entityContext.Set<OBExposureCCF>()
                                     where e.Flag == flag
                                     select new
                                     {
                                         AccountNumber = e.RefNo,
                                         e.CustomerName,
                                         e.OBE_Type,
                                         e.Mapped_OBE_Type,
                                         e.ValueDate,
                                         e.MaturityDate,
                                         e.Currency,
                                         e.Rate,
                                         e.Amount,
                                         HC1 = e.ProductType,
                                         HC2 = e.SubType,
                                         e.Default_Flag,
                                         DefaultDate = e.Date_Of_Devolement,
                                         e.DefaultBal,
                                         e.OneYearPriorBal
                                     })
                            .OrderBy(c => c.AccountNumber).ThenBy(c => c.OBE_Type).ThenBy(c => c.HC1).ThenBy(c => c.HC2);
                        var ExportHandler = new ExcelService();
                        var response = ExportHandler.Export(query.ToList(), path);
                    }
                    else
                    {
                        var query = (from e in entityContext.Set<OBExposureCCF>()
                                     where e.Flag == flag
                                     select new
                                     {
                                         AccountNumber = e.RefNo,
                                         e.CustomerName,
                                         e.OBE_Type,
                                         e.Mapped_OBE_Type,
                                         e.ValueDate,
                                         e.MaturityDate,
                                         e.Currency,
                                         e.Rate,
                                         e.Amount,
                                         HC1 = e.ProductType,
                                         HC2 = e.SubType,
                                         e.Default_Flag,
                                         DefaultDate = e.Date_Of_Devolement,
                                         e.Default_Amount_Crystallize
                                     })
                            .OrderBy(c => c.AccountNumber).ThenBy(c => c.OBE_Type).ThenBy(c => c.HC1).ThenBy(c => c.HC2);
                        var ExportHandler = new ExcelService();
                        var response = ExportHandler.Export(query.ToList(), path);
                    }
                    return new List<OBExposureCCF>().ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<OBExposureCCF>() where e.Flag == flag select e)
                        .OrderBy(c => c.Flag).ThenBy(c => c.RefNo).ThenBy(c => c.OBE_Type).ThenBy(c => c.ProductType);

                    return query.ToArray();
                }
            }
        }


    }
}