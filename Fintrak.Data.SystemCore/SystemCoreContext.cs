using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using System.Xml;
using Fintrak.Shared.Common;
using Fintrak.Shared.Common.Core;
using finEntity = Fintrak.Shared.SystemCore.Entities;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Data;
using Fintrak.Shared.SystemCore.Framework;
using System.Data.SqlClient;

namespace Fintrak.Data.SystemCore
{
    public class SystemCoreContext : DbContext
    {



        public SystemCoreContext()
            : base("FintrakCoreDBConnection")
        {
            //SqlConnection con = Database.Connection as SqlConnection;
            //connew = con.ConnectionString.ToString();
            System.Data.Entity.Database.SetInitializer<SystemCoreContext>(null);
        }


        List<Fintrak.Shared.SystemCore.Entities.AuditTrail> auditTrailList = new List<Fintrak.Shared.SystemCore.Entities.AuditTrail>();

        //SystemCore
        public DbSet<finEntity.Solution> SolutionSet { get; set; }
        public DbSet<finEntity.Module> ModuleSet { get; set; }
        public DbSet<finEntity.Role> RoleSet { get; set; }
        public DbSet<finEntity.UserSetup> UserSetupSet { get; set; }
        public DbSet<finEntity.UserRole> UserRoleSet { get; set; }
        public DbSet<finEntity.Menu> MenuSet { get; set; }
        public DbSet<finEntity.MenuRole> MenuRoleSet { get; set; }
        public DbSet<finEntity.AuditTrail> AuditTrailSet { get; set; }
        public DbSet<finEntity.Client> ClientSet { get; set; }
        public DbSet<finEntity.Company> CompanySet { get; set; }
        public DbSet<finEntity.General> GeneralSet { get; set; }
        public DbSet<finEntity.Database> DatabaseSet { get; set; }
        public DbSet<finEntity.CompanySecurity> CompanySecuritySet { get; set; }
        public DbSet<finEntity.UserSession> UserSessionSet { get; set; }
        public DbSet<finEntity.CompanyUser> CompanyUserSet { get; set; }
        public DbSet<finEntity.CompanyModule> CompanyModuleSet { get; set; }
        public DbSet<finEntity.ErrorTracker> ErrorTrackerSet { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Ignore<PropertyChangedEventHandler>();
            modelBuilder.Ignore<ExtensionDataObject>();
            modelBuilder.Ignore<IIdentifiableEntity>();

            //Solution
            modelBuilder.Entity<finEntity.Solution>().HasKey<int>(e => e.SolutionId).Ignore(e => e.EntityId);
            modelBuilder.Entity<finEntity.Solution>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<finEntity.Solution>().ToTable("cor_solution");

            //Module
            modelBuilder.Entity<finEntity.Module>().HasKey<int>(e => e.ModuleId).Ignore(e => e.EntityId);
            modelBuilder.Entity<finEntity.Module>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<finEntity.Module>().ToTable("cor_module");

            //Role Role
            modelBuilder.Entity<finEntity.Role>().HasKey<int>(e => e.RoleId).Ignore(e => e.EntityId);
            modelBuilder.Entity<finEntity.Role>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<finEntity.Role>().ToTable("cor_role");

            //UserSetup
            modelBuilder.Entity<finEntity.UserSetup>().HasKey<int>(e => e.UserSetupId).Ignore(e => e.EntityId);
            modelBuilder.Entity<finEntity.UserSetup>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<finEntity.UserSetup>().ToTable("cor_usersetup");

            //UserRole
            modelBuilder.Entity<finEntity.UserRole>().HasKey<int>(e => e.UserRoleId).Ignore(e => e.EntityId);
            modelBuilder.Entity<finEntity.UserRole>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<finEntity.UserRole>().ToTable("cor_userrole");

            //Menu
            modelBuilder.Entity<finEntity.Menu>().HasKey<int>(e => e.MenuId).Ignore(e => e.EntityId);
            modelBuilder.Entity<finEntity.Menu>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<finEntity.Menu>().ToTable("cor_menu");

            //MenuRole 
            modelBuilder.Entity<finEntity.MenuRole>().HasKey<int>(e => e.MenuRoleId).Ignore(e => e.EntityId);
            modelBuilder.Entity<finEntity.MenuRole>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<finEntity.MenuRole>().ToTable("cor_menurole");

            //Fintrak.Shared.SystemCore.Entities.AuditTrail
            modelBuilder.Entity<finEntity.AuditTrail>().HasKey<int>(e => e.AuditTrailId).Ignore(e => e.EntityId);
            modelBuilder.Entity<finEntity.AuditTrail>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<finEntity.AuditTrail>().ToTable("cor_audittrail");

            //Client
            modelBuilder.Entity<finEntity.Client>().HasKey<int>(e => e.ClientId).Ignore(e => e.EntityId);
            modelBuilder.Entity<finEntity.Client>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<finEntity.Client>().ToTable("cor_client");

            //Company
            modelBuilder.Entity<finEntity.Company>().HasKey<int>(e => e.CompanyId).Ignore(e => e.EntityId);
            modelBuilder.Entity<finEntity.Company>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<finEntity.Company>().ToTable("cor_company");

            //General
            modelBuilder.Entity<finEntity.General>().HasKey<int>(e => e.GeneralId).Ignore(e => e.EntityId);
            modelBuilder.Entity<finEntity.General>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<finEntity.General>().ToTable("cor_general");

            //Database
            modelBuilder.Entity<finEntity.Database>().HasKey<int>(e => e.DatabaseId).Ignore(e => e.EntityId);
            modelBuilder.Entity<finEntity.Database>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<finEntity.Database>().ToTable("cor_database");

            //CompanySecurity
            modelBuilder.Entity<finEntity.CompanySecurity>().HasKey<int>(e => e.CompanySecurityId).Ignore(e => e.EntityId);
            modelBuilder.Entity<finEntity.CompanySecurity>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<finEntity.CompanySecurity>().ToTable("cor_company_security");

            //UserSession
            modelBuilder.Entity<finEntity.UserSession>().HasKey<int>(e => e.UserSessionId).Ignore(e => e.EntityId);
            modelBuilder.Entity<finEntity.UserSession>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<finEntity.UserSession>().ToTable("cor_user_session");

            //CompanyUser
            modelBuilder.Entity<finEntity.CompanyUser>().HasKey<int>(e => e.CompanyUserId).Ignore(e => e.EntityId);
            modelBuilder.Entity<finEntity.CompanyUser>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<finEntity.CompanyUser>().ToTable("cor_company_user");

            //CompanyModule
            modelBuilder.Entity<finEntity.CompanyModule>().HasKey<int>(e => e.CompanyModuleId).Ignore(e => e.EntityId);
            modelBuilder.Entity<finEntity.CompanyModule>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<finEntity.CompanyModule>().ToTable("cor_company_module");

            //ErrorTracker
            modelBuilder.Entity<finEntity.ErrorTracker>().HasKey<int>(e => e.ErrorTrackerId).Ignore(e => e.EntityId);
            modelBuilder.Entity<finEntity.ErrorTracker>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<finEntity.ErrorTracker>().ToTable("cor_error_tracker");

        }

        public override int SaveChanges()
        {
            try
            {
                if (ChangeTracker.HasChanges())
                {
                    var entries = this.ChangeTracker.Entries();

                    foreach (DbEntityEntry entry in entries)
                    {
                        if (entry.Entity != null && !(entry.Entity is Fintrak.Shared.SystemCore.Entities.AuditTrail))
                        {
                            //is a normal entry, not a relationship
                            Fintrak.Shared.SystemCore.Entities.AuditTrail audit = this.AuditTrailFactory(entry, DataConnector.LoginName);
                            auditTrailList.Add(audit);
                        }
                    }
                }

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

        private Fintrak.Shared.SystemCore.Entities.AuditTrail AuditTrailFactory(DbEntityEntry entry, string userName)
        {
            Fintrak.Shared.SystemCore.Entities.AuditTrail audit = new Fintrak.Shared.SystemCore.Entities.AuditTrail();

            audit.RevisionStamp = DateTime.Now;
            audit.TableName = entry.Entity.GetType().Name;
            audit.UserName = userName;
            audit.Deleted = false;
            audit.CreatedBy = userName;
            audit.CreatedOn = DateTime.Now;
            audit.UpdatedBy = userName;
            audit.UpdatedOn = DateTime.Now;

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

            return audit;
        }

        private string GetEntryValueInString(DbEntityEntry entry, bool isOrginal)
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

