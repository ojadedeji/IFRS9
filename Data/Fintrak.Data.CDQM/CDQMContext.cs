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
using Fintrak.Shared.CDQM.Entities;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Data;
using systemContract = Fintrak.Data.SystemCore.Contracts;
using systemCore = Fintrak.Data.SystemCore;
using Fintrak.Data.SystemCore.Contracts;

namespace Fintrak.Data.CDQM
{
    public class CDQMContext : DbContext
    {
        const string SOLUTION_NAME = "FIN_CDQM";
        AuditManager _auditManager;

        public CDQMContext()
            : base(GetDataConnection())
        {
            System.Data.Entity.Database.SetInitializer<CDQMContext>(null);

            _auditManager = new AuditManager(GetDataConnection());
        }

        public CDQMContext(string connectionString)
            : base(connectionString)
        {
            System.Data.Entity.Database.SetInitializer<CDQMContext>(null);
            _auditManager = new AuditManager(connectionString);
        }

        //Configuration
        public DbSet<CDQMAddress> CDQMAddressSet { get; set; }
        public DbSet<CDQMCountry> CDQMCountrySet { get; set; }
        public DbSet<CDQMCustomerMIS> CDQMCustomerMISSet { get; set; }
        public DbSet<CDQMETLConfiguration> CDQMETLConfigurationSet { get; set; }
        public DbSet<CDQMGenderGroup> CDQMGenderGroupSet { get; set; }
        public DbSet<CDQMMerchant> CDQMMerchantSet { get; set; }
        public DbSet<CDQMProduct> CDQMProductSet { get; set; }
        public DbSet<CDQMTitle> CDQMTitleSet { get; set; }
        public DbSet<CDQMCustomerPersistent> CDQMCustomerPersistentSet { get; set; }
        public DbSet<CDQMCustomerDuplicate> CDQMCustomerDuplicateSet { get; set; }

       
        public DbSet<ChartOfAccount> ChartOfAccountSet { get; set; }
        public DbSet<Currency> CurrencySet { get; set; }
        public DbSet<FiscalYear> FiscalYearSet { get; set; }
        public DbSet<Branch> BranchSet { get; set; }
        public DbSet<Product> ProductSet { get; set; }
        public DbSet<FiscalPeriod> FiscalPeriodSet { get; set; }
        public DbSet<FinancialType> FinancialTypeSet { get; set; }
       

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Ignore<PropertyChangedEventHandler>();
            modelBuilder.Ignore<ExtensionDataObject>();
            modelBuilder.Ignore<IIdentifiableEntity>();

            //CDQM
            //Configuration
            modelBuilder.Entity<CDQMGenderGroup>().HasKey<int>(e => e.GenderGroupId).Ignore(e => e.EntityId);
            modelBuilder.Entity<CDQMGenderGroup>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CDQMGenderGroup>().ToTable("cdqm_gender_group");

            modelBuilder.Entity<CDQMAddress>().HasKey<int>(e => e.AddressId).Ignore(e => e.EntityId);
            modelBuilder.Entity<CDQMAddress>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CDQMAddress>().ToTable("cdqm_address");

            modelBuilder.Entity<CDQMCountry>().HasKey<int>(e => e.CountryId).Ignore(e => e.EntityId);
            modelBuilder.Entity<CDQMCountry>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CDQMCountry>().ToTable("cdqm_country");

            modelBuilder.Entity<CDQMMerchant>().HasKey<int>(e => e.MerchantId).Ignore(e => e.EntityId);
            modelBuilder.Entity<CDQMMerchant>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CDQMMerchant>().ToTable("cdqm_merchant");

            modelBuilder.Entity<CDQMTitle>().HasKey<int>(e => e.TitleId).Ignore(e => e.EntityId);
            modelBuilder.Entity<CDQMTitle>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CDQMTitle>().ToTable("cdqm_title");

            modelBuilder.Entity<CDQMETLConfiguration>().HasKey<int>(e => e.ETLConfigurationId).Ignore(e => e.EntityId);
            modelBuilder.Entity<CDQMETLConfiguration>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CDQMETLConfiguration>().ToTable("cdqm_etl_configuration");

            modelBuilder.Entity<CDQMProduct>().HasKey<int>(e => e.ProductId).Ignore(e => e.EntityId);
            modelBuilder.Entity<CDQMProduct>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CDQMProduct>().ToTable("cdqm_product");

            modelBuilder.Entity<CDQMCustomerMIS>().HasKey<int>(e => e.CustomerMISId).Ignore(e => e.EntityId);
            modelBuilder.Entity<CDQMCustomerMIS>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CDQMCustomerMIS>().ToTable("cdqm_customer_mis");

            modelBuilder.Entity<CDQMCustomerPersistent>().HasKey<int>(e => e.CUSTOMER_PERSISTENT_ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<CDQMCustomerPersistent>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CDQMCustomerPersistent>().ToTable("cdqm_customer_persistent");

            modelBuilder.Entity<CDQMCustomerDuplicate>().HasKey<int>(e => e.CUST_DUPLICATES_ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<CDQMCustomerDuplicate>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CDQMCustomerDuplicate>().ToTable("cdqm_cust_duplicates");

            //Core
         
            //ChartOfAccount
            modelBuilder.Entity<ChartOfAccount>().HasKey<int>(e => e.ChartOfAccountId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ChartOfAccount>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ChartOfAccount>().ToTable("cor_chartofacct");

            //Currency
            modelBuilder.Entity<Currency>().HasKey<int>(e => e.CurrencyId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Currency>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Currency>().ToTable("cor_currency");

            //FiscalYear
            modelBuilder.Entity<FiscalYear>().HasKey<int>(e => e.FiscalYearId).Ignore(e => e.EntityId);
            modelBuilder.Entity<FiscalYear>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<FiscalYear>().ToTable("cor_fiscalyear");

            //Branch
            modelBuilder.Entity<Branch>().HasKey<int>(e => e.BranchId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Branch>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Branch>().ToTable("cor_branch");

            //Product
            modelBuilder.Entity<Product>().HasKey<int>(e => e.ProductId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Product>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Product>().ToTable("cor_product");

            //FiscalPeriod
            modelBuilder.Entity<FiscalPeriod>().HasKey<int>(e => e.FiscalPeriodId).Ignore(e => e.EntityId);
            modelBuilder.Entity<FiscalPeriod>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<FiscalPeriod>().ToTable("cor_fiscalperiod");

            //FinancialType
            modelBuilder.Entity<FinancialType>().HasKey<int>(e => e.FinancialTypeId).Ignore(e => e.EntityId);
            modelBuilder.Entity<FinancialType>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<FinancialType>().ToTable("cor_financial_type");

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
