using System;
using System.Configuration;
using System.Linq;
using System.ServiceProcess;
using Fintrak.Shared.Common;
using Fintrak.Shared.Core.Entities;
using timer = System.Timers;
using System.Security.Permissions;
using Fintrak.Shared.Core.Framework;
using System.Threading;
using System.Collections.Generic;
using Fintrak.Services.Data;

namespace Fintrak.Services.ExtractionInstaller
{
    partial class ExtractionInstallerService : ServiceBase
    {
        DataManager _dataManager = null;
        List<string> _connectionStrings = null;

        timer.Timer _startTimer = null;
        timer.Timer _endTimer = null;
        string _servicePath = string.Empty;

        public ExtractionInstallerService()
        {
            InitializeComponent();
        }

        private bool CheckRunTime(DateTime? runTime)
        {
            var runFlag = true;

            if (runTime.HasValue)
            {
                if (runTime.Value <= DateTime.Now)
                    runFlag = true;
                else
                    runFlag = false;
            }

            return runFlag;
        }

        protected override void OnStart(string[] args)
        {
            //var identity = Thread.CurrentPrincipal.Identity;
            _servicePath = ConfigurationManager.AppSettings["ServicePath"].ToString();
            _dataManager = new DataManager();
            _connectionStrings = _dataManager.GetDataConnections();
        
            _startTimer = new System.Timers.Timer();
            _startTimer.Interval = 30000;
            _startTimer.Elapsed += _startTimer_Elapsed;
            _startTimer.Enabled = true;

            _endTimer = new System.Timers.Timer();
            _endTimer.Interval = 30000;
            _endTimer.Elapsed += _endTimer_Elapsed;
            _endTimer.Enabled = true;

            Log.WriteErrorLog("Fintrak service installer started");
        }

        private void _startTimer_Elapsed(object sender, timer.ElapsedEventArgs e)
        {
            foreach (var connectionString in _connectionStrings)
            {
                Log.WriteErrorLog("Processing connection :" );

                List<ExtractionJob> jobs = null;

                try
                {
                    //get new or pending extraction template
                    jobs = _dataManager.GetNewOrPendingExtractionJobs(connectionString);

                    Log.WriteErrorLog("Job found :" + jobs.Count().ToString());

                    foreach (var job in jobs)
                    {
                        Log.WriteErrorLog("Processing job :" + job.Code);

                        if (CheckRunTime(job.RunTime))
                        {
                            

                            var service = ServiceExist(job.Code);

                            if (job.Status == PackageStatus.New)
                            {
                                if (service == null)
                                {
                                    string svcName = job.Code;
                                    string svcDispName = "Fintrak " + job.Code + " " + " Service";

                                    //get username and password
                                    string userName = null;
                                    string password = null;

                                    if (Account == ServiceAccount.User)
                                    {
                                        userName = UserName;
                                        password = Password;
                                    }

                                    CustomServiceInstaller c = new CustomServiceInstaller();
                                    var result = c.InstallService(_servicePath, svcName, svcDispName, userName, password, connectionString);

                                    if (result)
                                    {
                                        //Update job to running
                                        job.Status = PackageStatus.Running;
                                        job.Remark = "Job running...";
                                        _dataManager.UpdateExtractionJob(connectionString,job);
                                        Log.WriteErrorLog("Fintrak " + job.Code + " " + " Service is currently running..");
                                    }
                                    else
                                    {
                                        Log.WriteErrorLog("Fintrak " + job.Code + " " + " Service cannot be created..");
                                    }

                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteErrorLog(ex);


                }
            }
        }

        private void _endTimer_Elapsed(object sender, timer.ElapsedEventArgs e)
        {
            //get new or pending extraction template
            UninstallService();
        }

        protected override void OnStop()
        {
            _startTimer.Enabled = false;
            _endTimer.Enabled = false;

            Log.WriteErrorLog("Fintrak service installer stopped");
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, ControlThread = true)]
        private void UninstallService()
        {
            foreach (var connectionString in _connectionStrings)
            {
                List<ExtractionJob> jobs = null;
                try
                {
                    //get new or pending extraction template
                    jobs = _dataManager.GetExtractionJobsToStop(connectionString);

                    lock (jobs)
                    {
                        foreach (var job in jobs)
                        {
                            if (job.Status == PackageStatus.Stop || job.Status == PackageStatus.Done || job.Status == PackageStatus.Fail)
                            {
                                var service = ServiceExist(job.Code);
                                if (service != null)
                                {
                                    CustomServiceInstaller c = new CustomServiceInstaller();
                                    c.UnInstallService(job.Code);

                                    //Update job to running
                                    job.Status = PackageStatus.Removed;
                                    job.Remark = "Job done...";
                                    _dataManager.UpdateExtractionJob(connectionString,job);

                                    Log.WriteErrorLog(string.Format("Package service for {0} has been un-installed", job.Code));
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteErrorLog(ex);


                }
            }
        }

        private ServiceController ServiceExist(string serviceName)
        {
            // get list of Windows services
            ServiceController[] services = ServiceController.GetServices();

            // try to find service name
            foreach (ServiceController service in services)
            {
                if (service.ServiceName == serviceName)
                    return service;
            }
            return null;
        }

        public static bool IsServiceInstalled(string serviceName)
        {
            // get list of Windows services
            ServiceController[] services = ServiceController.GetServices();

            // try to find service name
            foreach (ServiceController service in services)
            {
                if (service.ServiceName == serviceName)
                    return true;
            }
            return false;
        }

        private ServiceAccount Account
        {
            get
            {
                var account = (ConfigurationManager.AppSettings["ServiceAccount"] == null ? null : ConfigurationManager.AppSettings["ServiceAccount"]);

                if (account == "User")
                    return ServiceAccount.User;
                else if (account == "Local Service")
                    return ServiceAccount.LocalService;
                else if (account == "Local System")
                    return ServiceAccount.LocalSystem;
                else if (account == "Network Service")
                    return ServiceAccount.NetworkService;

                throw new Exception("Uable to read account configuration setting.");

            }
        }

        private string UserName
        {
            get
            {
                var userName = (ConfigurationManager.AppSettings["ServiceUserName"] == null ? null : ConfigurationManager.AppSettings["ServiceUserName"]);

                if (userName == null)
                    throw new Exception("Uable to read service user configuration setting.");
                else
                    return userName;
            }
        }

        private string Password
        {
            get
            {
                var password = (ConfigurationManager.AppSettings["ServicePassword"] == null ? null : ConfigurationManager.AppSettings["ServicePassword"]);

                if (password == null)
                    throw new Exception("Uable to read service password configuration setting.");
                else
                    return password;

            }
        }
    }
}
