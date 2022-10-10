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
    [Export(typeof(IGLMappingMgtRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GLMappingMgtRepository : DataRepositoryBase<GLMappingMgt>, IGLMappingMgtRepository
    {

        protected override GLMappingMgt AddEntity(IFRSContext entityContext, GLMappingMgt entity)
        {
            return entityContext.Set<GLMappingMgt>().Add(entity);
        }

        protected override GLMappingMgt UpdateEntity(IFRSContext entityContext, GLMappingMgt entity)
        {
            return (from e in entityContext.Set<GLMappingMgt>() 
                    where e.GLMappingMgtId == entity.GLMappingMgtId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<GLMappingMgt> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<GLMappingMgt>()
                   select e;
        }

        protected override GLMappingMgt GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<GLMappingMgt>()
                         where e.GLMappingMgtId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<GLMappingMgtInfo> GetGLMappingMgts()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.GLMappingMgtSet
                            join b in entityContext.IFRSRegistrySet on a.CaptionCode equals b.Code
                            where b.Flag == 1
                            select new GLMappingMgtInfo()
                            {
                                GLMappingMgt = a,
                                IFRSRegistry = b
                            };

                return query.ToFullyLoaded();
            }
        }

        public List<GLMappingMgt> GetSubSubCaption(string caption)
        {
            //var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;
            var connectionString = IFRSContext.GetDataConnection();

            var glmappings = new List<GLMappingMgt>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_getsubsubcaption", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "subcaption",
                    Value = caption,
                });


                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var glmapping = new GLMappingMgt();

                    if (reader["SubCaption1"] != DBNull.Value)
                        glmapping.SubCaption1 = reader["SubCaption1"].ToString();

                    glmappings.Add(glmapping);
                }

                con.Close();
            }

            return glmappings;
        }

 
    }
}
