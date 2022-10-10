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
    [Export(typeof(IIfrsPdSeriesByRatingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsPdSeriesByRatingRepository : DataRepositoryBase<IfrsPdSeriesByRating>, IIfrsPdSeriesByRatingRepository
    {
        protected override IfrsPdSeriesByRating AddEntity(IFRSContext entityContext, IfrsPdSeriesByRating entity)
        {
            return entityContext.Set<IfrsPdSeriesByRating>().Add(entity);
        }

        protected override IfrsPdSeriesByRating UpdateEntity(IFRSContext entityContext, IfrsPdSeriesByRating entity)
        {
            return (from e in entityContext.Set<IfrsPdSeriesByRating>()
                    where e.Sno == entity.Sno
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<IfrsPdSeriesByRating> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<IfrsPdSeriesByRating>()
                   select e;
        }

        public IEnumerable<IfrsPdSeriesByRating> GetAvailableIfrsPdSeriesByRating(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<IfrsPdSeriesByRating>()
                                 select new
                                 {
                                     e.Rating,
                                     e.seq,
                                     e.Year,
                                     e.Period,
                                     e.PDYear,
                                     e.MarginalDefaultPD,
                                     e.MarginalPD_BEST,
                                     e.MarginalPD_Downturn,
                                     e.MarginalPD_Optimistic,
                                     e.SurvivalPD_Downturn,
                                     e.SurvivalPD_Optimistic,
                                     e.LifeTimePD_BEST,
                                     e.LifeTimePD_Downturn,
                                     e.LifeTimePD_Optimistic,
                                     e.RunDate,
                                     e.EndDate
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<IfrsPdSeriesByRating>().Take(0).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsPdSeriesByRating>().Take(defaultCount)
                                 select e);

                    return query.ToArray();
                }
            }
        }

        protected override IfrsPdSeriesByRating GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IfrsPdSeriesByRating>()
                         where e.Sno == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }


        public IEnumerable<IfrsPdSeriesByRating> GetEntityByCode(string id)
        {

            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.IfrsPdSeriesByRatingSet
                            where a.Rating == id
                            select a;

                return query.ToFullyLoaded();
            }
        }
       
    }
}