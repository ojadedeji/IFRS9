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
    [Export(typeof(IFacClassConsolidatedRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FacClassConsolidatedRepository : DataRepositoryBase<FacClassConsolidated>, IFacClassConsolidatedRepository
    {
        protected override FacClassConsolidated AddEntity(IFRSContext entityContext, FacClassConsolidated entity)
        {
            return entityContext.Set<FacClassConsolidated>().Add(entity);
        }

        protected override FacClassConsolidated UpdateEntity(IFRSContext entityContext, FacClassConsolidated entity)
        {
            return (from e in entityContext.Set<FacClassConsolidated>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<FacClassConsolidated> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<FacClassConsolidated>()
                   select e;
        }

        protected override FacClassConsolidated GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<FacClassConsolidated>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<FacClassConsolidated> GetFacClassConsolidatedBySearch(string searchParam)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<FacClassConsolidated>()
                             where e.CustomerName == searchParam || e.AccountNo == searchParam
                             select e);

                return query.ToArray();
            }
        }

        public IEnumerable<FacClassConsolidated> GetFacClassConsolidated(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {

                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<FacClassConsolidated>()
                                 select new
                                 {
                                     e.AccountNo,
                                     e.CustomerNo,
                                     e.CustomerName,
                                     e.HC1,
                                     e.HC2,
                                     e.Sector
                                 });
                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<FacClassConsolidated>();
                }
                else
                {
                    var query = (from e in entityContext.Set<FacClassConsolidated>().Take(defaultCount)
                                 select e);

                    return query.ToArray();
                }
            }
        }
    }
}
