using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Data.SqlClient;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Scorecard.Entities;
using Fintrak.Data.Scorecard.Contracts;

namespace Fintrak.Data.Scorecard
{
    [Export(typeof(ISCDTargetRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SCDTargetRepository : DataRepositoryBase<SCDTarget>, ISCDTargetRepository
    {

        protected override SCDTarget AddEntity(ScorecardContext entityContext, SCDTarget entity)
        {
            return entityContext.Set<SCDTarget>().Add(entity);
        }

        protected override SCDTarget UpdateEntity(ScorecardContext entityContext, SCDTarget entity)
        {
            return (from e in entityContext.Set<SCDTarget>()
                    where e.TargetId == entity.TargetId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<SCDTarget> GetEntities(ScorecardContext entityContext)
        {
            return from e in entityContext.Set<SCDTarget>()
                   select e;
        }

        protected override SCDTarget GetEntity(ScorecardContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<SCDTarget>()
                         where e.TargetId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<SCDTarget> GetCaptions()
        {
            //var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;
            var connectionString = ScorecardContext.GetDataConnection();

            var scdactuals = new List<SCDTarget>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_mpr_getDistinctSCDTargetCaption", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var scdactual = new SCDTarget();

                    if (reader["Caption"] != DBNull.Value)
                        scdactual.Caption = reader["Caption"].ToString();

                    scdactuals.Add(scdactual);
                }

                con.Close();
            }

            return scdactuals;
        }



    }
}
