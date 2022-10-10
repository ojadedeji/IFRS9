using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Data.SqlClient;
using Fintrak.Data.IFRS;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Core.Entities;
using Fintrak.Data.Core.Contracts;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Data.Core
{
    [Export(typeof(IProcessJobRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProcessJobRepository : DataRepositoryBase<ProcessJob>, IProcessJobRepository
    {
        protected override ProcessJob AddEntity(CoreContext entityContext, ProcessJob entity)
        {
            return entityContext.Set<ProcessJob>().Add(entity);
        }

        protected override ProcessJob UpdateEntity(CoreContext entityContext, ProcessJob entity)
        {
            return (from e in entityContext.Set<ProcessJob>()
                    where e.ProcessJobId == entity.ProcessJobId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ProcessJob> GetEntities(CoreContext entityContext)
        {
            return from e in entityContext.Set<ProcessJob>().Take(50)
                   select e;
        }

        protected override ProcessJob GetEntity(CoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ProcessJob>()
                         where e.ProcessJobId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ProcessJob> GetProcessJobByRunDate(DateTime startDate, DateTime endDate)
        {
            using (CoreContext entityContext = new CoreContext())
            {
                var query = from a in entityContext.ProcessJobSet
                            where a.StartDate >= startDate.Date &&  a.EndDate <= endDate.Date
                                select a;

                return query.ToFullyLoaded();
            }
        }

        public void ClearProcessHistory(int solutionId)
        {

            var connectionString = IFRSContext.GetDataConnection();

            int status = 0;

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_clear_process_history", con);
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
