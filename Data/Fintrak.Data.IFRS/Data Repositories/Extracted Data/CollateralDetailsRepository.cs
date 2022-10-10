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
    [Export(typeof(ICollateralDetailsRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CollateralDetailsRepository : DataRepositoryBase<CollateralDetails>, ICollateralDetailsRepository
    {
        protected override CollateralDetails AddEntity(IFRSContext entityContext, CollateralDetails entity)
        {
            return entityContext.Set<CollateralDetails>().Add(entity);
        }

        protected override CollateralDetails UpdateEntity(IFRSContext entityContext, CollateralDetails entity)
        {
            return (from e in entityContext.Set<CollateralDetails>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CollateralDetails> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<CollateralDetails>().Take(200)
                   select e;
        }

        protected override CollateralDetails GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CollateralDetails>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<CollateralDetails> GetCollateralDetailsBySearch(string searchParam)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<CollateralDetails>()
                             where e.CustomerName == searchParam || e.AccountNo == searchParam
                             select e);

                return query.ToArray();
            }
        }

        public IEnumerable<CollateralDetails> GetCollateralDetails(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {

                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<CollateralDetails>() select new
                    {
                        e.AccountNo,
                        e.CustomerName,
                        e.YOO,
                        e.MaturityDate,
                        e.HC1,
                        e.HC2,
                        e.ColType1,
                        Col_Location1 = e.Col_Qualifier1,
                        e.Perfection_Status1,
                        e.ColAmt1,
                        e.ColType2,
                        Col_Location2 = e.Col_Qualifier2,
                        e.Perfection_Status2,
                        e.ColAmt2,
                        e.ColType3,
                        Col_Location3 = e.Col_Qualifier3,
                        e.Perfection_Status3,
                        e.ColAmt3,
                        e.ColType4,
                        Col_Location4 = e.Col_Qualifier4,
                        e.Perfection_Status4,
                        e.ColAmt4,
                        e.DateLoaded
                    });
                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<CollateralDetails>();
                }
                else
                {
                    var query = (from e in entityContext.Set<CollateralDetails>().Take(defaultCount)
                                 select e);

                    return query.ToArray();
                }
            }
        }
    }
}
