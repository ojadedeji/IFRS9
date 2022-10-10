using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Data.IFRS;
using System.Data.SqlClient;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Core.Entities;
using Fintrak.Data.Core.Contracts;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Data.Core
{
    [Export(typeof(IExtractionJobRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ExtractionJobRepository : DataRepositoryBase<ExtractionJob>, IExtractionJobRepository
    {
        protected override ExtractionJob AddEntity(CoreContext entityContext, ExtractionJob entity)
        {
            return entityContext.Set<ExtractionJob>().Add(entity);
        }

        protected override ExtractionJob UpdateEntity(CoreContext entityContext, ExtractionJob entity)
        {
            return (from e in entityContext.Set<ExtractionJob>()
                    where e.ExtractionJobId == entity.ExtractionJobId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ExtractionJob> GetEntities(CoreContext entityContext)
        {
            return from e in entityContext.Set<ExtractionJob>()
                   select e;
        }

        protected override ExtractionJob GetEntity(CoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ExtractionJob>()
                         where e.ExtractionJobId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ExtractionJob> GetExtractionJobByRunDate(DateTime startDate, DateTime endDate)
        {
            using (CoreContext entityContext = new CoreContext())
            {
                var query = from a in entityContext.ExtractionJobSet
                            where a.StartDate >= startDate.Date &&  a.EndDate <= endDate.Date
                                select a;

                return query.ToFullyLoaded();
            }
        }

        public void ClearExtractionHistory(int solutionId)
        {
           
            var connectionString = IFRSContext.GetDataConnection(); 
            
            int status = 0;

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_clear_extraction_history", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "SolutionId",
                    Value = solutionId,
                });

                con.Open();

                status = cmd.ExecuteNonQuery();

                con.Close();
            }

        
        }
    }
}
