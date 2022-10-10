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
    [Export(typeof(ISCDActualRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SCDActualRepository : DataRepositoryBase<SCDActual>, ISCDActualRepository
    {

        protected override SCDActual AddEntity(ScorecardContext entityContext, SCDActual entity)
        {
            return entityContext.Set<SCDActual>().Add(entity);
        }

        protected override SCDActual UpdateEntity(ScorecardContext entityContext, SCDActual entity)
        {
            return (from e in entityContext.Set<SCDActual>()
                    where e.ActualId == entity.ActualId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<SCDActual> GetEntities(ScorecardContext entityContext)
        {
            return from e in entityContext.Set<SCDActual>()
                   select e;
        }

        protected override SCDActual GetEntity(ScorecardContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<SCDActual>()
                         where e.ActualId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }


        public IEnumerable<SCDActual> GetCaption()
        {
            //var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;
            var connectionString = ScorecardContext.GetDataConnection();

            var scdactuals = new List<SCDActual>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_mpr_getDistinctSCDActualCaption", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var scdactual = new SCDActual();

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
