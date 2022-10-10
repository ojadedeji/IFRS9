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
    [Export(typeof(IOBExposureRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OBExposureRepository : DataRepositoryBase<OBExposure>, IOBExposureRepository
    {
        protected override OBExposure AddEntity(IFRSContext entityContext, OBExposure entity)
        {
            return entityContext.Set<OBExposure>().Add(entity);
        }

        protected override OBExposure UpdateEntity(IFRSContext entityContext, OBExposure entity)
        {
            return (from e in entityContext.Set<OBExposure>()
                    where e.obe_Id == entity.obe_Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<OBExposure> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<OBExposure>().Take(200)
                   select e;
        }

        protected override OBExposure GetEntity(IFRSContext entityContext, int obe_Id)
        {
            var query = (from e in entityContext.Set<OBExposure>()
                         where e.obe_Id == obe_Id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<OBExposure> GetOBExposureBySearch(int flag, string searchParam)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<OBExposure>()
                             where e.Refno.Contains(searchParam) && e.Flag == flag
                             select e);

                return query.ToArray();
            }
        }

        public IEnumerable<OBExposure> GetOBExposure(int flag, int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<OBExposure>()
                                 where e.Flag == flag
                                 select new
                                 {
                                     AccountNumber = e.Refno,
                                     e.CustomerName,
                                     e.OBE_Type,
                                     e.Mapped_OBE_Type,
                                     e.ValueDate,
                                     e.MaturityDate,
                                     e.Currency,
                                     e.Rate,
                                     e.Amount,
                                     HC1 = e.ProductType,
                                     HC2 = e.SubType
                                 })
                        .OrderBy(c => c.AccountNumber).ThenBy(c => c.OBE_Type).ThenBy(c => c.HC1).ThenBy(c => c.HC2);
                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<OBExposure>().ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<OBExposure>() where e.Flag == flag select e)
                        .OrderBy(c => c.Flag).ThenBy(c => c.Refno).ThenBy(c => c.OBE_Type).ThenBy(c => c.ProductType).Take(defaultCount);

                    return query.ToArray().Take(defaultCount);
                }
            }
        }


    }
}