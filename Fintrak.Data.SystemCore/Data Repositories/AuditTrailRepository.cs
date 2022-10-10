using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Fintrak.Data.SystemCore.Contracts;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.SystemCore.Entities;
using Fintrak.Shared.SystemCore.Framework;

namespace Fintrak.Data.SystemCore
{
    [Export(typeof(IAuditTrailRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AuditTrailRepository : DataRepositoryBase<AuditTrail>, IAuditTrailRepository
    {

        protected override AuditTrail AddEntity(SystemCoreContext entityContext, AuditTrail entity)
        {
            return entityContext.Set<AuditTrail>().Add(entity);
        }

        protected override AuditTrail UpdateEntity(SystemCoreContext entityContext, AuditTrail entity)
        {
            return (from e in entityContext.Set<AuditTrail>()
                    where e.AuditTrailId == entity.AuditTrailId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<AuditTrail> GetEntities(SystemCoreContext entityContext)
        {
            //return from e in entityContext.Set<AuditTrail>()
            //       select e;
            var query = (from e in entityContext.Set<AuditTrail>()
                         select e).Take(0);
            var results = query;
            return results;
        }

        protected override AuditTrail GetEntity(SystemCoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<AuditTrail>()
                         where e.AuditTrailId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<AuditTrail> GetByDate(DateTime fromDate, DateTime toDate)
        {
            using (SystemCoreContext entityContext = new SystemCoreContext())
            {
                var query = from a in entityContext.AuditTrailSet
                            where a.RevisionStamp >= fromDate && a.RevisionStamp <= toDate
                            select a;

                return query.ToFullyLoaded();
            }
        }

        //public IEnumerable<AuditTrail> GetByAction(AuditAction action, DateTime fromDate, DateTime toDate)
        //{
        //    using (SystemCoreContext entityContext = new SystemCoreContext())
        //    {
        //        var query = from a in entityContext.AuditTrailSet
        //                    where a.RevisionStamp >= fromDate && a.RevisionStamp <= toDate && a.Actions == action
        //                    select a;

        //        return query.ToFullyLoaded();
        //    }
        //}


        public IEnumerable<AuditTrail> GetAuditTrailByTab(AuditAction action)
        {
            using (SystemCoreContext entityContext = new SystemCoreContext())
            {
                var query = from a in entityContext.AuditTrailSet
                            where a.Actions == action
                            select a;

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<AuditTrail> GetByTable(string tablename, DateTime fromDate, DateTime toDate)
        {
            using (SystemCoreContext entityContext = new SystemCoreContext())
            {
                var query = from a in entityContext.AuditTrailSet
                            where a.RevisionStamp >= fromDate && a.RevisionStamp <= toDate && a.TableName == tablename
                            select a;

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<AuditTrail> GetByLoginID(string loginID, DateTime fromDate, DateTime toDate)
        {
            using (SystemCoreContext entityContext = new SystemCoreContext())
            {
                var query = from a in entityContext.AuditTrailSet
                            where a.RevisionStamp >= fromDate && a.RevisionStamp <= toDate && a.UserName == loginID
                            select a;

                return query.ToFullyLoaded();
            }
        }

        public List<AuditTrail> GetByAction(string action, DateTime fromDate, DateTime toDate)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

            var audTrails = new List<AuditTrail>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_cor_audittrail", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Action",
                    Value = action,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "StartDate",
                    Value = fromDate,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "EndDate",
                    Value = toDate,
                });

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var audTrail = new AuditTrail();

                    if (reader["AuditTrailId"] != DBNull.Value)
                        audTrail.AuditTrailId = int.Parse(reader["AuditTrailId"].ToString());

                    if (reader["RevisionStamp"] != DBNull.Value)
                        audTrail.RevisionStamp = DateTime.Parse(reader["RevisionStamp"].ToString());

                    if (reader["TableName"] != DBNull.Value)
                        audTrail.TableName = reader["TableName"].ToString();

                    if (reader["UserName"] != DBNull.Value)
                        audTrail.UserName = reader["UserName"].ToString();

                    if (reader["Actions"] != DBNull.Value)
                        audTrail.Actions = (AuditAction)reader["Actions"];

                    if (reader["ActionDescription"] != DBNull.Value)
                        audTrail.ActionDescription = reader["ActionDescription"].ToString();
                    audTrails.Add(audTrail);
                }

                con.Close();


            }

            return audTrails;
        }

    }
}
