using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;
using System.Data.SqlClient;
using Fintrak.Shared.Common.Services;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IComputedForcastedPDLGDRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ComputedForcastedPDLGDRepository : DataRepositoryBase<ComputedForcastedPDLGD>, IComputedForcastedPDLGDRepository
    {
        protected override ComputedForcastedPDLGD AddEntity(IFRSContext entityContext, ComputedForcastedPDLGD entity)
        {
            return entityContext.Set<ComputedForcastedPDLGD>().Add(entity);
        }

        protected override ComputedForcastedPDLGD UpdateEntity(IFRSContext entityContext, ComputedForcastedPDLGD entity)
        {
            return (from e in entityContext.Set<ComputedForcastedPDLGD>()
                    where e.ComputedPDId == entity.ComputedPDId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ComputedForcastedPDLGD> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<ComputedForcastedPDLGD>()
                   select e;
        }

        protected override ComputedForcastedPDLGD GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ComputedForcastedPDLGD>()
                         where e.ComputedPDId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ComputedForcastedPDLGD> GetAvailableComputedForcastedPDLGD(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<ComputedForcastedPDLGD>()
                                 select new
                                 {
                                     e.Year,
                                     e.Sector_Code,
                                     e.Type,
                                     e.PD_LGD,
                                     e.AdversePD,
                                     e.OptimisticPD,
                                     e.PD,
                                     e.Rundate
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<ComputedForcastedPDLGD>().Take(0).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<ComputedForcastedPDLGD>().Take(defaultCount)
                                 select e);

                    return query.ToArray();
                }
            }
        }

        public List<ComputedForcastedPDLGD> GetComputedForcastedPDLGD()
        {
            //var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;
            var connectionString = IFRSContext.GetDataConnection();

            var computes = new List<ComputedForcastedPDLGD>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_ifrs_cptforcasted_pd_lgd", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;


                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var compute = new ComputedForcastedPDLGD();

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