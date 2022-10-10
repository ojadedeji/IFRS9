using Fintrak.Data.IFRS.Contracts;
using Fintrak.Shared.Common.Services;
using Fintrak.Shared.IFRS.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Fintrak.Data.IFRS
{
    [Export(typeof(IFacRatingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FacRatingRepository : DataRepositoryBase<FacRating>, IFacRatingRepository
    {
        protected override FacRating AddEntity(IFRSContext entityContext, FacRating entity)
        {
            return entityContext.Set<FacRating>().Add(entity);
        }

        protected override FacRating UpdateEntity(IFRSContext entityContext, FacRating entity)
        {
            return (from e in entityContext.Set<FacRating>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<FacRating> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<FacRating>().Take(200)
                   select e;
        }

        protected override FacRating GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<FacRating>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<FacRating> GetFacRatingBySearch(string searchParam)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<FacRating>()
                             where e.CustomerName == searchParam || e.AccountNo == searchParam
                             select e);

                return query.ToArray();
            }
        }

        public IEnumerable<FacRating> GetFacRating(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {

                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<FacRating>()
                                 select new
                                 {
                                     e.AccountNo,
                                     e.CustomerNo,
                                     e.CustomerName,
                                     e.Rating
                                 });
                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<FacRating>();
                }
                else
                {
                    var query = (from e in entityContext.Set<FacRating>().Take(defaultCount)
                                 select e);

                    return query.ToArray();
                }
            }
        }
    }
}
