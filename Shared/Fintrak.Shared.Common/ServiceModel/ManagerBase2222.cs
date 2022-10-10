using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using Fintrak.Shared.Common.Core;
using System.Collections.Generic;
using Fintrak.Shared.Common.Data;
using Fintrak.Shared.Common.Utils;
using Fintrak.Shared.SystemCore.Entities;
using System.Data.SqlClient;
using systemCoreEntities = Fintrak.Shared.SystemCore.Entities;
using systemCoreData = Fintrak.Data.SystemCore.Contracts;
using Fintrak.Data.SystemCore.Contracts;
using Fintrak.Shared.Common.Contracts;
 
namespace Fintrak.Shared.Common.ServiceModel2222
{

    public class ManagerBase
    {
        public ManagerBase()
        {
            OperationContext context = OperationContext.Current;
            if (context != null)
            {
                try
                {
                    _LoginName = OperationContext.Current.IncomingMessageHeaders.GetHeader<string>("String", "System");
                    _CompanyCode = OperationContext.Current.IncomingMessageHeaders.GetHeader<string>("Company", "System");
                    if (_LoginName.IndexOf(@"\") > -1) _LoginName = string.Empty;
                }
                catch (Exception ex)
                {
                    _LoginName = string.Empty;
                    _CompanyCode = string.Empty;
                }
            }

            if (ObjectBase.Container != null)
                ObjectBase.Container.SatisfyImportsOnce(this);

            //RegisterModule();

        }

        [Import]
        IDataRepositoryFactory _DataRepositoryFactory;

        [OperationBehavior(TransactionScopeRequired = true)]
        public virtual void RegisterModule()
        {

        }
       
        protected string _LoginName = string.Empty;
        protected string _CompanyCode = string.Empty;

        protected virtual bool AllowAccessToOperation(string moduleName, List<string> groupNames)
        {
            return false;
        }

        protected virtual string GetContextConnection()
        {
            return string.Empty;
        }

        protected T ExecuteFaultHandledOperation<T>(Func<T> codetoExecute)
        {
            try
            {
                return codetoExecute.Invoke();
            }
            catch (AuthorizationValidationException ex)
            {
                throw new FaultException<AuthorizationValidationException>(ex, ex.Message);
            }
            catch (ModuleException ex)
            {
                throw new FaultException<ModuleException>(ex, ex.Message);
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message.ToString()))
                {
                    ErrorTracker(ex.Message.ToString());
                }
                else
                {
                    ErrorTracker(ex.InnerException.Message.ToString());
                }
                
                throw new FaultException(ex.InnerException.Message);
            }
        }

        protected void ExecuteFaultHandledOperation(Action codetoExecute)
        {
            try
            {
                codetoExecute.Invoke();
            }
            catch (AuthorizationValidationException ex)
            {
                throw new FaultException<AuthorizationValidationException>(ex, ex.Message);
            }
            catch (ModuleException ex)
            {
                throw new FaultException<ModuleException>(ex, ex.Message);
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
              

        public void ErrorTracker(string errorMsg)
        {
            var connectionString = GetDataConnection();
            int rct;
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("InsertErrorMessage", con);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@errorMsg",
                    Value = errorMsg,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@UserId",
                    Value = _LoginName,
                    
                });
                cmd.CommandTimeout = 0;

                con.Open();

                cmd.ExecuteNonQuery();
              
                con.Close();
            }

        }


        public string GetDataConnection()
        {
            string connectionString = "";

            if (!string.IsNullOrEmpty(DataConnector.CompanyCode))
            {
                IDatabaseRepository databaseRepository = _DataRepositoryFactory.GetDataRepository<IDatabaseRepository>();
                var companydb = databaseRepository.Get().Where(c => c.CompanyCode == DataConnector.CompanyCode).FirstOrDefault();

                if (companydb == null)
                    throw new Exception("Unable to load company database.");

                connectionString = string.Format("Data Source= {0};Initial Catalog={1};User ={2};Password={3};Integrated Security={4}", companydb.ServerName, companydb.DatabaseName, companydb.UserName, companydb.Password, companydb.IntegratedSecurity);
            }

            return connectionString;
        }
    }
}