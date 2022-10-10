using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Core.Entities;
using Fintrak.Data.Core.Contracts;
using System.Data.SqlClient;

namespace Fintrak.Data.Core
{
    [Export(typeof(ISolutionRunDateRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SolutionRunDateRepository : DataRepositoryBase<SolutionRunDate>, ISolutionRunDateRepository
    {
        protected override SolutionRunDate AddEntity(CoreContext entityContext, SolutionRunDate entity)
        {
            return entityContext.Set<SolutionRunDate>().Add(entity);
        }

        protected override SolutionRunDate UpdateEntity(CoreContext entityContext, SolutionRunDate entity)
        {
            return (from e in entityContext.Set<SolutionRunDate>()
                    where e.SolutionRunDateId == entity.SolutionRunDateId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<SolutionRunDate> GetEntities(CoreContext entityContext)
        {
            return from e in entityContext.Set<SolutionRunDate>()
                   select e;
        }

        protected override SolutionRunDate GetEntity(CoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<SolutionRunDate>()
                         where e.SolutionRunDateId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<SolutionRunDateInfo> GetSolutionRunDates()
        {
            using (CoreContext entityContext = new CoreContext())
            {
                var query = from a in entityContext.SolutionRunDateSet
                           
                            select new SolutionRunDateInfo()
                            {
                                SolutionRunDate = a,

                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<SolutionRunDate> GetRunDate()
        {
            //var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;
            var connectionString = CoreContext.GetDataConnection();

            var solutionrundates = new List<SolutionRunDate>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_getarchiverundate", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;


                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var solutionrundate = new SolutionRunDate();

                    if (reader["AchRunDate"] != DBNull.Value)
                        solutionrundate.RunDate = DateTime.Parse(reader["AchRunDate"].ToString());

                    solutionrundates.Add(solutionrundate);
                }

                con.Close();
            }

            return solutionrundates;
        }
    }
}