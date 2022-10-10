using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;
using System.Data.SqlClient;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IComputedForcastedPDLGDForeignRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ComputedForcastedPDLGDForeignRepository : DataRepositoryBase<ComputedForcastedPDLGDForeign>, IComputedForcastedPDLGDForeignRepository
    {
        protected override ComputedForcastedPDLGDForeign AddEntity(IFRSContext entityContext, ComputedForcastedPDLGDForeign entity)
        {
            return entityContext.Set<ComputedForcastedPDLGDForeign>().Add(entity);
        }

        protected override ComputedForcastedPDLGDForeign UpdateEntity(IFRSContext entityContext, ComputedForcastedPDLGDForeign entity)
        {
            return (from e in entityContext.Set<ComputedForcastedPDLGDForeign>()
                    where e.ComputedPDId == entity.ComputedPDId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ComputedForcastedPDLGDForeign> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<ComputedForcastedPDLGDForeign>()
                   select e;
        }

        protected override ComputedForcastedPDLGDForeign GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ComputedForcastedPDLGDForeign>()
                         where e.ComputedPDId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }


        public List<ComputedForcastedPDLGDForeign> GetComputedForcastedPDLGDForeign()
        {
            //var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;
            var connectionString = IFRSContext.GetDataConnection();

            var computes = new List<ComputedForcastedPDLGDForeign>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_ifrs_cptforcasted_pd_lgd", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;


                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var compute = new ComputedForcastedPDLGDForeign();

                    if (reader["PD"] != DBNull.Value)
                        compute.PD = double.Parse(reader["PD"].ToString());

                    if (reader["PD_LGD"] != DBNull.Value)
                        compute.PD_LGD = double.Parse(reader["PD_LGD"].ToString());

                    if (reader["Description"] != DBNull.Value)
                        compute.Sector_Code = reader["Description"].ToString();

                    if (reader["Year"] != DBNull.Value)
                        compute.Year = int.Parse(reader["Year"].ToString());

                    computes.Add(compute);
                }

                con.Close();
            }

            return computes;
        }
       
    }
}