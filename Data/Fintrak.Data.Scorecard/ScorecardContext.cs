using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Fintrak.Shared.AuditService;
using Fintrak.Shared.Scorecard.Entities;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Data;
using systemContract = Fintrak.Data.SystemCore.Contracts;
using systemCore = Fintrak.Data.SystemCore;
using Fintrak.Data.SystemCore.Contracts;


namespace Fintrak.Data.Scorecard
{
    public class ScorecardContext : DbContext
    {
        const string SOLUTION_NAME = "FIN_SCORECARD";

        AuditManager _auditManager;

        public ScorecardContext()
            : base(GetDataConnection())
        {
            System.Data.Entity.Database.SetInitializer<ScorecardContext>(null);

            _auditManager = new AuditManager(GetDataConnection());
        }

        public ScorecardContext(string connectionString)
            : base(connectionString)
        {
            System.Data.Entity.Database.SetInitializer<ScorecardContext>(null);
            _auditManager = new AuditManager(connectionString);
        }

        //Score Card
        //Configuration
        public DbSet<SCDConfiguration> SCDConfigurationSet { get; set; }
        public DbSet<SCDTeamMap> SCDTeamMapSet { get; set; }
        public DbSet<SCDCategory> SCDCategorySet { get; set; }
        public DbSet<SCDKPI> SCDKPISet { get; set; }
        public DbSet<SCDKPIClassification> SCDKPIClassificationSet { get; set; }
        public DbSet<SCDParticipant> SCDParticipantSet { get; set; }
        public DbSet<SCDThreshold> SCDThresholdSet { get; set; }
        public DbSet<SCDActual> SCDActualSet { get; set; }
        public DbSet<SCDTarget> SCDTargetSet { get; set; }
        public DbSet<SCDKPIActualMap> SCDKPIActualMapSet { get; set; }
        public DbSet<SCDKPITargetMap> SCDKPITargetMapSet { get; set; }
        public DbSet<SCDKPIEntry> SCDKPIEntrySet { get; set; }
        public DbSet<SCDTeamClassification> SCDTeamClassificationSet { get; set; }

        //Core
        public DbSet<Staff> StaffSet { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Ignore<PropertyChangedEventHandler>();
            modelBuilder.Ignore<ExtensionDataObject>();
            modelBuilder.Ignore<IIdentifiableEntity>();

            //Score Card
            //SCDConfiguration
            modelBuilder.Entity<SCDConfiguration>().HasKey<int>(e => e.ConfigurationId).Ignore(e => e.EntityId);
            modelBuilder.Entity<SCDConfiguration>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<SCDConfiguration>().ToTable("scd_configuration");

            //SCDTeamMap
            modelBuilder.Entity<SCDTeamMap>().HasKey<int>(e => e.TeamMapId).Ignore(e => e.EntityId);
            modelBuilder.Entity<SCDTeamMap>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<SCDTeamMap>().ToTable("scd_team_map");

            //SCDCategory
            modelBuilder.Entity<SCDCategory>().HasKey<int>(e => e.CategoryId).Ignore(e => e.EntityId);
            modelBuilder.Entity<SCDCategory>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<SCDCategory>().ToTable("scd_category");

            //SCDKPI
            modelBuilder.Entity<SCDKPI>().HasKey<int>(e => e.KPIId).Ignore(e => e.EntityId);
            modelBuilder.Entity<SCDKPI>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<SCDKPI>().ToTable("scd_kpi");

            //SCDClassification
            modelBuilder.Entity<SCDKPIClassification>().HasKey<int>(e => e.ClassificationId).Ignore(e => e.EntityId);
            modelBuilder.Entity<SCDKPIClassification>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<SCDKPIClassification>().ToTable("scd_kpi_classification");

            //SCDParticipant
            modelBuilder.Entity<SCDParticipant>().HasKey<int>(e => e.ParticipantId).Ignore(e => e.EntityId);
            modelBuilder.Entity<SCDParticipant>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<SCDParticipant>().ToTable("scd_participant");

            //SCDThreshold
            modelBuilder.Entity<SCDThreshold>().HasKey<int>(e => e.ThresholdId).Ignore(e => e.EntityId);
            modelBuilder.Entity<SCDThreshold>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<SCDThreshold>().ToTable("scd_threshold");

            //SCDActual
            modelBuilder.Entity<SCDActual>().HasKey<int>(e => e.ActualId).Ignore(e => e.EntityId);
            modelBuilder.Entity<SCDActual>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<SCDActual>().ToTable("scd_actual");


            //SCDTarget
            modelBuilder.Entity<SCDTarget>().HasKey<int>(e => e.TargetId).Ignore(e => e.EntityId);
            modelBuilder.Entity<SCDTarget>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<SCDTarget>().ToTable("scd_target");

            //SCDKPIActualMap
            modelBuilder.Entity<SCDKPIActualMap>().HasKey<int>(e => e.MapId).Ignore(e => e.EntityId);
            modelBuilder.Entity<SCDKPIActualMap>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<SCDKPIActualMap>().ToTable("scd_actual_map");

            //SCDKPITargetMap
            modelBuilder.Entity<SCDKPITargetMap>().HasKey<int>(e => e.MapId).Ignore(e => e.EntityId);
            modelBuilder.Entity<SCDKPITargetMap>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<SCDKPITargetMap>().ToTable("scd_target_map");

            //SCDKPIEntry
            modelBuilder.Entity<SCDKPIEntry>().HasKey<int>(e => e.EntryId).Ignore(e => e.EntityId);
            modelBuilder.Entity<SCDKPIEntry>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<SCDKPIEntry>().ToTable("scd_entry");

            //SCDTeamClassification
            modelBuilder.Entity<SCDTeamClassification>().HasKey<int>(e => e.TeamClassificationId).Ignore(e => e.EntityId);
            modelBuilder.Entity<SCDTeamClassification>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<SCDTeamClassification>().ToTable("scd_team_classification");

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
                        if (entry.Entity != null)
                        {
                            if (entry.State == EntityState.Added)
                            {
                                //entry is Added 

                                var model = (EntityBase)entry.Entity;
                                model.CreatedBy = DataConnector.LoginName;
                                model.CreatedOn = DateTime.Now;
                                model.UpdatedBy = DataConnector.LoginName;
                                model.UpdatedOn = DateTime.Now;
                            }
                            else if (entry.State == EntityState.Deleted)
                            {
                                //entry in deleted

                            }
                            else
                            {
                                //entry is modified
                                var model = (EntityBase)entry.Entity;
                                model.UpdatedBy = DataConnector.LoginName;
                                model.UpdatedOn = DateTime.Now;
                            }

                            _auditManager.AddAudit(entry, DataConnector.LoginName);
                        }
                    }
                }

                _auditManager.Save();

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

                //connectionString="Data Source=10.0.0.18\FintrakSQL2014;Initial Catalog=FintrakDB;User =sa;Password=sqluser10$;Integrated Security=False"
                connectionString = string.Format("Data Source= {0};Initial Catalog={1};User ={2};Password={3};Integrated Security={4}", companydb.Database.ServerName, companydb.Database.DatabaseName, companydb.Database.UserName, companydb.Database.Password, companydb.Database.IntegratedSecurity);
            }

            return connectionString;
        }

    }
}
