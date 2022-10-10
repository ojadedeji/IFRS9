using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Data;
using systemContract = Fintrak.Data.SystemCore.Contracts;
using systemCore = Fintrak.Data.SystemCore;
using Fintrak.Data.SystemCore.Contracts;
using Fintrak.Shared.AuditService;
using DecryptOperation;

namespace Fintrak.Data.Core
{
    public class CoreContext : DbContext
    {
        const string SOLUTION_NAME = "CORE";
        AuditManager _auditManager;

        public CoreContext()
            : base(GetDataConnection())
        {
            System.Data.Entity.Database.SetInitializer<CoreContext>(null);

            _auditManager = new AuditManager(GetDataConnection());
        }

        public CoreContext(string connectionString)
            : base(connectionString)
        {
            System.Data.Entity.Database.SetInitializer<CoreContext>(null);
            _auditManager = new AuditManager(connectionString);
        }

        //Core
        public DbSet<FiscalYear> FiscalYearSet { get; set; }
        public DbSet<FiscalPeriod> FiscalPeriodSet { get; set; }
      
        public DbSet<ProductType> ProductTypeSet { get; set; }
        public DbSet<Product> ProductSet { get; set; }
        public DbSet<Currency> CurrencySet { get; set; }
        public DbSet<RateType> RateTypeSet { get; set; }
        public DbSet<CurrencyRate> CurrencyRateSet { get; set; }
        public DbSet<ChartOfAccount> ChartOfAccountSet { get; set; }
        public DbSet<ProductTypeMapping> ProductTypeMappingSet { get; set; }
        public DbSet<Branch> BranchSet { get; set; }
        public DbSet<FinancialType> FinancialTypeSet { get; set; }
        public DbSet<Staff> StaffSet { get; set; }
        public DbSet<PayGrade> PayGradeSet { get; set; }
       
        //Extraction and Processing
        public DbSet<PackageSetup> PackageSetupSet { get; set; }
        public DbSet<Extraction> ExtractionSet { get; set; }
        public DbSet<ExtractionRole> ExtractionRoleSet { get; set; }
        public DbSet<ExtractionJob> ExtractionJobSet { get; set; }
        public DbSet<ExtractionTrigger> ExtractionTriggerSet { get; set; }
        public DbSet<Processes> ProcessSet { get; set; }
        public DbSet<ProcessRole> ProcessRoleSet { get; set; }
        public DbSet<ProcessJob> ProcessJobSet { get; set; }
        public DbSet<ProcessTrigger> ProcessTriggerSet { get; set; }
        public DbSet<SolutionRunDate> SolutionRunDateSet { get; set; }
        public DbSet<ClosedPeriod> ClosedPeriodSet { get; set; }
        public DbSet<ClosedPeriodTemplate> ClosedPeriodTemplateSet { get; set; }
        public DbSet<Upload> UploadSet { get; set; }
        public DbSet<UploadRole> UploadRoleSet { get; set; }

        public DbSet<ReportStatus> ReportStatusSet { get; set; }
        public DbSet<DefaultUser> DefaultUserSet { get; set; }
        public DbSet<CheckDataAvailability> CheckDataAvailabilitySet { get; set; }

     public DbSet<CheckifrsDataAvailability> CheckifrsDataAvailabilitySet { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Ignore<PropertyChangedEventHandler>();
            modelBuilder.Ignore<ExtensionDataObject>();
            modelBuilder.Ignore<IIdentifiableEntity>();

            //FiscalYear
            modelBuilder.Entity<FiscalYear>().HasKey<int>(e => e.FiscalYearId).Ignore(e => e.EntityId);
            modelBuilder.Entity<FiscalYear>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<FiscalYear>().ToTable("cor_fiscalyear");

            //FiscalPeriod
            modelBuilder.Entity<FiscalPeriod>().HasKey<int>(e => e.FiscalPeriodId).Ignore(e => e.EntityId);
            modelBuilder.Entity<FiscalPeriod>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<FiscalPeriod>().ToTable("cor_fiscalperiod");

            //ProductType
            modelBuilder.Entity<ProductType>().HasKey<int>(e => e.ProductTypeId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ProductType>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ProductType>().ToTable("cor_producttype");

            //Product
            modelBuilder.Entity<Product>().HasKey<int>(e => e.ProductId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Product>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Product>().ToTable("cor_product");

            //Currency
            modelBuilder.Entity<Currency>().HasKey<int>(e => e.CurrencyId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Currency>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Currency>().ToTable("cor_currency");

            //RateType
            modelBuilder.Entity<RateType>().HasKey<int>(e => e.RateTypeId).Ignore(e => e.EntityId);
            modelBuilder.Entity<RateType>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<RateType>().ToTable("cor_ratetype");

            //CurrencyRate
            modelBuilder.Entity<CurrencyRate>().HasKey<int>(e => e.CurrencyRateId).Ignore(e => e.EntityId);
            modelBuilder.Entity<CurrencyRate>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CurrencyRate>().ToTable("cor_currencyrate");

            //ChartOfAccount
            modelBuilder.Entity<ChartOfAccount>().HasKey<int>(e => e.ChartOfAccountId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ChartOfAccount>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ChartOfAccount>().ToTable("cor_chartofacct");

            //ProductTypeMapping
            modelBuilder.Entity<ProductTypeMapping>().HasKey<int>(e => e.ProductTypeMappingId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ProductTypeMapping>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ProductTypeMapping>().ToTable("cor_producttypemapping");

            //Branch
            modelBuilder.Entity<Branch>().HasKey<int>(e => e.BranchId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Branch>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Branch>().ToTable("cor_branch");

            //FinancialType
            modelBuilder.Entity<FinancialType>().HasKey<int>(e => e.FinancialTypeId).Ignore(e => e.EntityId);
            modelBuilder.Entity<FinancialType>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<FinancialType>().ToTable("cor_financial_type");


            //GLDefinition
            modelBuilder.Entity<GLDefinition>().HasKey<int>(e => e.GLDefinitionId).Ignore(e => e.EntityId);
            modelBuilder.Entity<GLDefinition>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<GLDefinition>().ToTable("cor_gl_definition");

            //Staff
            modelBuilder.Entity<Staff>().HasKey<int>(e => e.StaffId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Staff>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Staff>().ToTable("cor_staff");

            //PayGrade
            modelBuilder.Entity<PayGrade>().HasKey<int>(e => e.PayGradeId).Ignore(e => e.EntityId);
            modelBuilder.Entity<PayGrade>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<PayGrade>().ToTable("cor_paygrade");

            //PackageSetup
            modelBuilder.Entity<PackageSetup>().HasKey<int>(e => e.PackageSetupId).Ignore(e => e.EntityId);
            modelBuilder.Entity<PackageSetup>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<PackageSetup>().ToTable("cor_packagesetup");

            //Extraction
            modelBuilder.Entity<Extraction>().HasKey<int>(e => e.ExtractionId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Extraction>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Extraction>().ToTable("cor_extraction");

            //ExtractionRole
            modelBuilder.Entity<ExtractionRole>().HasKey<int>(e => e.ExtractionRoleId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ExtractionRole>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ExtractionRole>().ToTable("cor_extractionrole");

            //ExtractionJob
            modelBuilder.Entity<ExtractionJob>().HasKey<int>(e => e.ExtractionJobId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ExtractionJob>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ExtractionJob>().ToTable("cor_extractionjob");

            //ExtractionTrigger
            modelBuilder.Entity<ExtractionTrigger>().HasKey<int>(e => e.ExtractionTriggerId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ExtractionTrigger>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ExtractionTrigger>().ToTable("cor_extractiontrigger");

            //Process
            modelBuilder.Entity<Processes>().HasKey<int>(e => e.ProcessId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Processes>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Processes>().ToTable("cor_process");

            //ProcessRole
            modelBuilder.Entity<ProcessRole>().HasKey<int>(e => e.ProcessRoleId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ProcessRole>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ProcessRole>().ToTable("cor_processrole");

            //ProcessJob
            modelBuilder.Entity<ProcessJob>().HasKey<int>(e => e.ProcessJobId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ProcessJob>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ProcessJob>().ToTable("cor_processjob");

            //ProcessTrigger
            modelBuilder.Entity<ProcessTrigger>().HasKey<int>(e => e.ProcessTriggerId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ProcessTrigger>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ProcessTrigger>().ToTable("cor_processtrigger");

            //Upload
            modelBuilder.Entity<Upload>().HasKey<int>(e => e.UploadId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Upload>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Upload>().ToTable("cor_upload");

            //UploadRole
            modelBuilder.Entity<UploadRole>().HasKey<int>(e => e.UploadRoleId).Ignore(e => e.EntityId);
            modelBuilder.Entity<UploadRole>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<UploadRole>().ToTable("cor_uploadrole");

            //SolutionRunDate
            modelBuilder.Entity<SolutionRunDate>().HasKey<int>(e => e.SolutionRunDateId).Ignore(e => e.EntityId);
            modelBuilder.Entity<SolutionRunDate>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<SolutionRunDate>().ToTable("cor_solutionrundate");

            //ClosedPeriod
            modelBuilder.Entity<ClosedPeriod>().HasKey<int>(e => e.ClosedPeriodId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ClosedPeriod>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ClosedPeriod>().ToTable("cor_closedperiod");

            //ClosedPeriodTemplate
            modelBuilder.Entity<ClosedPeriodTemplate>().HasKey<int>(e => e.ClosedPeriodTemplateId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ClosedPeriodTemplate>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ClosedPeriodTemplate>().ToTable("cor_closeperiod_template");

            //Report Status
            modelBuilder.Entity<ReportStatus>().HasKey<int>(e => e.StatusId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ReportStatus>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ReportStatus>().ToTable("cor_report_status");


            //DefaultUser
            modelBuilder.Entity<DefaultUser>().HasKey<int>(e => e.DefaultUserId).Ignore(e => e.EntityId);
            modelBuilder.Entity<DefaultUser>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<DefaultUser>().ToTable("cor_defaultuser");

            //CheckDataAvailability
            modelBuilder.Entity<CheckDataAvailability>().HasKey<int>(e => e.CheckDataId).Ignore(e => e.EntityId);
            modelBuilder.Entity<CheckDataAvailability>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CheckDataAvailability>().ToTable("ifrs_data_availability");

            //CheckDataAvailability
            modelBuilder.Entity<CheckifrsDataAvailability>().HasKey<int>(e => e.CheckDataId).Ignore(e => e.EntityId);
            modelBuilder.Entity<CheckifrsDataAvailability>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CheckifrsDataAvailability>().ToTable("ifrs9_data_availability");
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

    }
}
