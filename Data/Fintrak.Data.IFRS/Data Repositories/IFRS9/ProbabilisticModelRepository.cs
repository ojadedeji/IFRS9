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
    [Export(typeof(IProbabilisticModelRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProbabilisticModelRepository : DataRepositoryBase<ProbabilisticModel>, IProbabilisticModelRepository
    {
        protected override ProbabilisticModel AddEntity(IFRSContext entityContext, ProbabilisticModel entity)
        {
            return entityContext.Set<ProbabilisticModel>().Add(entity);
        }

        protected override ProbabilisticModel UpdateEntity(IFRSContext entityContext, ProbabilisticModel entity)
        {
            return (from e in entityContext.Set<ProbabilisticModel>()
                    where e.ProbId == entity.ProbId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ProbabilisticModel> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<ProbabilisticModel>()
                   select e;
        }

        protected override ProbabilisticModel GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ProbabilisticModel>()
                         where e.ProbId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ProbabilisticModel> GetProbabilisticModels(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                   
                    var query = (from e in entityContext.Set<ProbabilisticModel>()
                                 select new
                                 {

                                     Datee = e.Datee.ToString(),
                                     GDPRate = e.gdp_growth_rate                                    
                                 });
                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);
                    return new List<ProbabilisticModel>().Take(defaultCount).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<ProbabilisticModel>().Take(defaultCount) select e);

                    return query.ToArray();
                }
            }
        }

    }
}