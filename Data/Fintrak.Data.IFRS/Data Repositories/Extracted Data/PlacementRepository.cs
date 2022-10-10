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
    [Export(typeof(IPlacementRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PlacementRepository : DataRepositoryBase<Placement>, IPlacementRepository
    {
        protected override Placement AddEntity(IFRSContext entityContext, Placement entity)
        {
            return entityContext.Set<Placement>().Add(entity);
        }

        protected override Placement UpdateEntity(IFRSContext entityContext, Placement entity)
        {
            return (from e in entityContext.Set<Placement>()
                    where e.Placement_Id == entity.Placement_Id
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<Placement> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<Placement>()
                   select e;
        }

        protected override Placement GetEntity(IFRSContext entityContext, int Placement_Id)
        {
            var query = (from e in entityContext.Set<Placement>()
                         where e.Placement_Id == Placement_Id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        //public IEnumerable<Placement> GetPlacementByRefNo(string RefNo)
        //{
        //    using(IFRSContext entityContext = new IFRSContext())
        //    {
        //    var query = (from e in entityContext.Set<Placement>()
        //                 where e.RefNo.Contains(RefNo)
        //                 select e);

        //    return query.ToArray();
        //    }
        //}
        //HistoricalClassificationRepository
      

        //PlacementRepository
        public IEnumerable<Placement> GetAvailablePlacement(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<Placement>()
                                 select new
                                 {
                                     e.RefNo,
                                     e.CustomerName,
                                     e.Rating,
                                     e.BookingDate,
                                     e.ValueDate,
                                     e.MaturityDate,
                                     e.Amount,
                                     e.Rate,
                                     e.Currency,
                                     e.ExchangeRate,
                                     e.LCY_Amount,
                                     e.CollateralType,
                                     e.CollateralValue,
                                     e.CollateralHaircut,
                                     e.RunDate,
                                     e.Stage,
                                     e.asset_classification,
                                     e.repayment_term,
                                     e.previous_rating,
                                     e.asset_type,
                                     e.asset_desc,
                                     e.rating_agency,
                                     e.days_pass_due,
                                     e.prudential_classification,
                                     e.forebearance_flag
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<Placement>().Take(0).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<Placement>().Take(defaultCount)
                                 select e);

                    return query.ToArray();
                }
            }
        }

    }
}