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
    [Export(typeof(IFacStagingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FacStagingRepository : DataRepositoryBase<FacStaging>, IFacStagingRepository
    {
        protected override FacStaging AddEntity(IFRSContext entityContext, FacStaging entity)
        {
            return entityContext.Set<FacStaging>().Add(entity);
        }

        protected override FacStaging UpdateEntity(IFRSContext entityContext, FacStaging entity)
        {
            return (from e in entityContext.Set<FacStaging>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<FacStaging> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<FacStaging>().Take(200)
                   select e;
        }

        protected override FacStaging GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<FacStaging>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<FacStaging> GetFacStagingBySearch(string searchParam)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                string hc2 = (searchParam.Split(' ').Count() > 1 ? searchParam.Split(' ')[1] : searchParam.Split(' ')[0]);
                string hc1 = searchParam.Split(' ')[0];
                var query = (from e in entityContext.Set<FacStaging>()
                             where e.AccountNo == searchParam || e.HC1 == searchParam || e.HC2 == searchParam || (e.HC1 == hc1 && e.HC2 == hc2)
                             select e);
                var aaa = searchParam.Split(' ').Count();
                return query.ToArray();
            }
        }

        public IEnumerable<FacStaging> GetFacStaging(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {

                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<FacStaging>()
                                 select new
                                 {
                                     e.AccountNo,
                                     e.HC1,
                                     e.HC2,
                                     e.FacilityType,
                                     e.Stage
                                 });
                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<FacStaging>();
                }
                else
                {
                    var query = (from e in entityContext.Set<FacStaging>().Take(defaultCount)
                                 select e);

                    return query.ToArray();
                }
            }
        }
    }
}
