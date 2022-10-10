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
using Fintrak.Shared.Basic.Entities;
using Fintrak.Shared.Scorecard.Entities;
using Fintrak.Shared.Common;
using Fintrak.Shared.Common.Core;
using finEntity = Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Core.Framework;
using Fintrak.Shared.Common.Data;

namespace Fintrak.Data.Scorecard
{
    public class ScorecardContext : DbContext
    {

        public ScorecardContext()
            : base("FintrakDBConnection")
        {
            System.Data.Entity.Database.SetInitializer<ScorecardContext>(null);
        }

        List<AuditTrail> auditTrailList = new List<AuditTrail>();

        //MPR
        public DbSet<TeamClassificationType> TeamClassificationTypeSet { get; set; }
       

        //Score Card
        //Configuration
        public DbSet<SCDConfiguration> SCDConfigurationSet { get; set; }

        //Core
        public DbSet<AuditTrail> AuditTrailSet { get; set; }
        public DbSet<Company> CompanySet { get; set; }
        public DbSet<Staff> StaffSet { get; set; }
       

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Ignore<PropertyChangedEventHandler>();
            modelBuilder.Ignore<ExtensionDataObject>();
            modelBuilder.Ignore<IIdentifiableEntity>();

            //MPR
            //TeamDefinition
            modelBuilder.Entity<TeamDefinition>().HasKey<int>(e => e.TeamDefinitionId).Ignore(e => e.EntityId);
            modelBuilder.Entity<TeamDefinition>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<TeamDefinition>().ToTable("mpr_team_definition");

          
            //Score Card
            //SCDConfiguration
            modelBuilder.Entity<SCDConfiguration>().HasKey<int>(e => e.ConfigurationId).Ignore(e => e.EntityId);
            modelBuilder.Entity<SCDConfiguration>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<SCDConfiguration>().ToTable("scd_configuration");

            //Core
            //Audittrail
            modelBuilder.Entity<AuditTrail>().HasKey<int>(e => e.AuditTrailId).Ignore(e => e.EntityId);
            modelBuilder.Entity<AuditTrail>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<AuditTrail>().ToTable("cor_audittrail");

            //CompanySet
            modelBuilder.Entity<Company>().HasKey<int>(e => e.CompanyId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Company>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Company>().ToTable("cor_company");

            //Staff
            modelBuilder.Entity<Staff>().HasKey<int>(e => e.StaffId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Staff>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Staff>().ToTable("cor_staff");

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
                        if (entry.Entity != null && !(entry.Entity is AuditTrail))
                        {
                            //is a normal entry, not a relationship
                            AuditTrail audit = this.AuditTrailFactory(entry, DataConnector.LoginName);
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


        //
        private AuditTrail AuditTrailFactory(DbEntityEntry entry, string userName)
        {
            AuditTrail audit = new AuditTrail();

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
