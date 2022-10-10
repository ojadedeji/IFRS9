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
    [Export(typeof(IProbabilityWeightedRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProbabilityWeightedRepository : DataRepositoryBase<ProbabilityWeighted>, IProbabilityWeightedRepository
    {
        protected override ProbabilityWeighted AddEntity(IFRSContext entityContext, ProbabilityWeighted entity)
        {
            return entityContext.Set<ProbabilityWeighted>().Add(entity);
        }

        protected override ProbabilityWeighted UpdateEntity(IFRSContext entityContext, ProbabilityWeighted entity)
        {
            return (from e in entityContext.Set<ProbabilityWeighted>()
                    where e.ProbabilityWeighted_Id == entity.ProbabilityWeighted_Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ProbabilityWeighted> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<ProbabilityWeighted>()
                   select e;
        }

        protected override ProbabilityWeighted GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ProbabilityWeighted>()
                         where e.ProbabilityWeighted_Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ProbabilityWeighted> GetProbabilityWeightedByInstrumentType(string InstrumentType)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<ProbabilityWeighted>()
                             where e.InstrumentType == InstrumentType
                             select e);

                return query.ToArray();
            }
        }

        public IEnumerable<ProbabilityWeighted> ExportProbabilityWeighted(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<ProbabilityWeighted>()
                                 select new
                                 {
                                     e.ProbabilityWeighted_Id,
                                     e.Optimistic,
                                     e.Best,
                                     e.Downturn,
                                     e.InstrumentType,
                                     e.Category,
                                     e.ProductType,
                                     e.SubType
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<ProbabilityWeighted>().Take(0).ToArray().OrderBy(c => c.Best).ThenBy(c => c.Downturn);

                }
                else
                {
                    var query = (from e in entityContext.Set<ProbabilityWeighted>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                                 select e);

                    return query.ToArray();
                }
            }
        }

    }
}