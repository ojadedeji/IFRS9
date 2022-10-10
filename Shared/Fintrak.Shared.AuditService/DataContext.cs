using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Collections.Generic;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Data;
using systemContract = Fintrak.Data.SystemCore.Contracts;
using systemCore = Fintrak.Data.SystemCore;
using Fintrak.Data.SystemCore.Contracts;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using System.Reflection;
using Fintrak.Shared.Common;
using DecryptOperation;
using System.Web;
using System.Net;

namespace Fintrak.Shared.AuditService
{
    public class DataContext : DbContext
    {
        const string SOLUTION_NAME = "CORE";

        public DataContext()
            : base(GetDataConnection())
        {
            System.Data.Entity.Database.SetInitializer<DataContext>(null);
        }

        public DataContext(string connectionString)
            : base(connectionString)
        {
            System.Data.Entity.Database.SetInitializer<DataContext>(null);
        }

        //Core

        public List<AuditTrail> auditTrailList = new List<AuditTrail>();
      
        public DbSet<AuditTrail> AuditTrailSet { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Ignore<PropertyChangedEventHandler>();
            modelBuilder.Ignore<ExtensionDataObject>();
            modelBuilder.Ignore<IIdentifiableEntity>();

            //Fintrak.Shared.SystemCore.Entities.AuditTrail
            modelBuilder.Entity<AuditTrail>().HasKey<int>(e => e.AuditTrailId).Ignore(e => e.EntityId);
            modelBuilder.Entity<AuditTrail>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<AuditTrail>().ToTable("cor_audittrail");
        }

        public override int SaveChanges()
        {
            try
            {
                if (auditTrailList.Count > 0)
                {
                    foreach (var audit in auditTrailList)
                    {
                        //add all audits 
                        AuditTrailSet.Add(audit);
                    }

                    auditTrailList.Clear();
                }

                return base.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                var innerEx = e.InnerException;
                while (innerEx.InnerException != null)
                    innerEx = innerEx.InnerException;

                throw new Exception(innerEx.Message);
            }
            catch (DbEntityValidationException e)
            {
                var sb = new StringBuilder();

                foreach (var entry in e.EntityValidationErrors)
                {
                    foreach (var error in entry.ValidationErrors)
                    {
                        sb.AppendLine(string.Format("{0}-{1}-{2}", entry.Entry.Entity, error.PropertyName, error.ErrorMessage));
                    }
                }

                throw new Exception(sb.ToString());
            }
            catch (Exception e)
            {
                var innerEx = e.InnerException;
                while (innerEx.InnerException != null)
                    innerEx = innerEx.InnerException;

                throw new Exception(innerEx.Message);
            }

        }

        public static string GetDataConnection()
        {
            string connectionString = "";

            if (!string.IsNullOrEmpty(DataConnector.CompanyCode) && !string.IsNullOrEmpty(SOLUTION_NAME))
            {
                systemContract.IDatabaseRepository databaseRepository = new systemCore.DatabaseRepository();
                var companydbs = databaseRepository.GetDatabases().Where(c => c.Database.CompanyCode == DataConnector.CompanyCode && (c.Solution.Name == SOLUTION_NAME || c.Solution.Name == "CORE"));

                DatabaseInfo companydb = null;

                if (companydbs == null)
                    throw new Exception("Unable to load database.");
                else
                {
                    companydb = companydbs.Where(c => c.Solution.Name == SOLUTION_NAME).FirstOrDefault();

                    if (companydb == null)
                        companydb = companydbs.FirstOrDefault();
                }

                string companydbServerName;
                string companydbDatabaseName;
                string companydbUserName;
                string companydbPassword;

                if (companydb.Database.Title == "Encrypted")
                {
                    var key = Properties.Resources.Key;
                    companydbServerName = CryptoEngine.Decrypt(companydb.Database.ServerName, key);
                    companydbDatabaseName = CryptoEngine.Decrypt(companydb.Database.DatabaseName, key);
                    companydbUserName = CryptoEngine.Decrypt(companydb.Database.UserName, key);
                    companydbPassword = CryptoEngine.Decrypt(companydb.Database.Password, key);
                }
                else
                {
                    companydbServerName = companydb.Database.ServerName;
                    companydbDatabaseName = companydb.Database.DatabaseName;
                    companydbUserName = companydb.Database.UserName;
                    companydbPassword = companydb.Database.Password;
                }

                //connectionString="Data Source=10.0.0.18\FintrakSQL2014;Initial Catalog=FintrakDB;User =sa;Password=sqluser10$;Integrated Security=False"
                connectionString = string.Format("Data Source= {0};Initial Catalog={1};User ={2};Password={3};Integrated Security={4}", companydbServerName, companydbDatabaseName, companydbUserName, companydbPassword, companydb.Database.IntegratedSecurity);
            }

            return connectionString;
        }

        public AuditTrail AuditTrailFactory(DbEntityEntry entry, string userName)
        {
            AuditTrail audit = new AuditTrail();
            string Hostname;

            audit.RevisionStamp = DateTime.Now;
            audit.TableName = entry.Entity.GetType().Name;
            audit.UserName = userName;
            audit.Deleted = false;
            audit.CreatedBy = userName;
            audit.CreatedOn = DateTime.Now;
            audit.UpdatedBy = userName;
            audit.UpdatedOn = DateTime.Now;

            string VisitorsIPAddress = string.Empty;
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null && HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString().Length > 3)
            {
                VisitorsIPAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != null && HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString().Length > 3)
            {
                VisitorsIPAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
            else if (HttpContext.Current.Request.UserHostAddress.Length != 0 && HttpContext.Current.Request.UserHostAddress.Length > 3)
            {
                VisitorsIPAddress = HttpContext.Current.Request.UserHostAddress;
            }
            else
            {
                IPHostEntry host;
                host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress ip in host.AddressList)
                {
                    if (ip.AddressFamily.ToString() == "InterNetwork")
                    {
                        VisitorsIPAddress = ip.ToString();
                    }
                }
            }
            Hostname = Dns.GetHostEntry(VisitorsIPAddress).HostName.ToString();

            audit.PCName = Hostname;
            audit.IPAddress = VisitorsIPAddress;
            //audit.PCName

            if (entry.State == EntityState.Added)
            {
                //entry is Added 

                var model = (EntityBase)entry.Entity;
                model.CreatedBy = DataConnector.LoginName;
                model.CreatedOn = DateTime.Now;
                model.UpdatedBy = DataConnector.LoginName;
                model.UpdatedOn = DateTime.Now;

                audit.NewData = GetEntryValueInString(entry, false);
                audit.Actions = AuditAction.C;
            }
            else if (entry.State == EntityState.Deleted)
            {
                //entry in deleted
                audit.OldData = GetEntryValueInString(entry, true);
                audit.Actions = AuditAction.D;
            }
            else
            {
                //entry is modified
                var model = (EntityBase)entry.Entity;
                model.UpdatedBy = DataConnector.LoginName;
                model.UpdatedOn = DateTime.Now;

                audit.OldData = GetEntryValueInString(entry, true);
                audit.NewData = GetEntryValueInString(entry, false);
                audit.Actions = AuditAction.U;

                IEnumerable<string> modifiedProperties = entry.CurrentValues.PropertyNames;
                //assing collection of mismatched Columns name as serialized string 
                audit.ChangedColumns = XMLSerializationHelper.XmlSerialize(modifiedProperties.ToArray());
            }

            auditTrailList.Add(audit);

            return audit;
        }

        public string GetEntryValueInString(DbEntityEntry entry, bool isOrginal)
        {
            if (entry.Entity is EntityBase)
            {
                object target = CloneEntity(entry.Entity);

                IEnumerable<string> properties = null;
                if (isOrginal)
                    properties = entry.OriginalValues.PropertyNames;
                else
                    properties = entry.CurrentValues.PropertyNames;

                foreach (string propName in properties)
                {
                    object setterValue = null;
                    if (isOrginal)
                    {
                        //Get orginal value 
                        setterValue = entry.OriginalValues[propName];
                    }
                    else
                    {
                        //Get orginal value 
                        setterValue = entry.CurrentValues[propName];
                    }
                    //Find property to update 
                    PropertyInfo propInfo = target.GetType().GetProperty(propName);
                    //update property with orgibal value 
                    if (setterValue == DBNull.Value)
                    {//
                        setterValue = null;
                    }
                    propInfo.SetValue(target, setterValue, null);

                }//end foreach

                XmlSerializer formatter = new XmlSerializer(target.GetType());
                XDocument document = new XDocument();

                using (XmlWriter xmlWriter = document.CreateWriter())
                {
                    formatter.Serialize(xmlWriter, target);
                }
                return document.Root.ToString();
            }
            return null;
        }

        public object CloneEntity(object obj)
        {
            DataContractSerializer dcSer = new DataContractSerializer(obj.GetType());
            MemoryStream memoryStream = new MemoryStream();

            dcSer.WriteObject(memoryStream, obj);
            memoryStream.Position = 0;

            object newObject = dcSer.ReadObject(memoryStream);
            return newObject;
        }

    }
}
