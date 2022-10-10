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
    [Export(typeof(IFacOBEStagingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FacOBEStagingRepository : DataRepositoryBase<FacOBEStaging>, IFacOBEStagingRepository
    {
        protected override FacOBEStaging AddEntity(IFRSContext entityContext, FacOBEStaging entity)
        {
            return entityContext.Set<FacOBEStaging>().Add(entity);
        }

        protected override FacOBEStaging UpdateEntity(IFRSContext entityContext, FacOBEStaging entity)
        {
            return (from e in entityContext.Set<FacOBEStaging>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<FacOBEStaging> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<FacOBEStaging>().Take(200)
                   select e;
        }

        protected override FacOBEStaging GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<FacOBEStaging>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<FacOBEStaging> GetFacOBEStagingBySearch(string searchParam)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                string hc2 = (searchParam.Split(' ').Count() > 1 ? searchParam.Split(' ')[1] : searchParam.Split(' ')[0]);
                string hc1 = searchParam.Split(' ')[0];
                var query = (from e in entityContext.Set<FacOBEStaging>()
                             where e.AccountNo == searchParam || e.HC1 == searchParam || e.HC2 == searchParam || (e.HC1 == hc1 && e.HC2 == hc2)
                             select e);
                var aaa = searchParam.Split(' ').Count();
                return query.ToArray();
            }
        }

        public IEnumerable<FacOBEStaging> GetFacOBEStaging(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {

                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<FacOBEStaging>()
                                 select new
                                 {
                                     e.AccountNo,
                                     e.HC1,
                                     e.HC2,
                                     e.Stage
                                 });
                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<FacOBEStaging>();
                }
                else
                {
                    var query = (from e in entityContext.Set<FacOBEStaging>().Take(defaultCount)
                                 select e);

                    return query.ToArray();
                }
            }
        }
    }
}
