using System;
using System.Configuration;
using System.Linq;
using System.ServiceProcess;
using Fintrak.Shared.Common;
using timer = System.Timers;
using System.Security.Permissions;
using Fintrak.Shared.Basic.Framework;

namespace Fintrak.Services.Installer
{
    public partial class InstallerService : ServiceBase
    {
        DataManager _dataManager = null;
        timer.Timer _startTimer = null;
        timer.Timer _endTimer = null;
        string _servicePath = string.Empty;

        public InstallerService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _servicePath = ConfigurationManager.AppSettings["ServicePath"].ToString();
            _dataManager = new DataManager();
        
            _startTimer = new System.Timers.Timer();
            _startTimer.Interval = 30000;
            _startTimer.Elapsed += _startTimer_Elapsed;
            _startTimer.Enabled = true;

            _endTimer = new System.Timers.Timer();
            _endTimer.Interval = 20000;
            _endTimer.Elapsed += _endTimer_Elapsed;
            _endTimer.Enabled = true;

            Log.WriteErrorLog("Fintrak service installer started");
        }

        private void _startTimer_Elapsed(object sender, timer.ElapsedEventArgs e)
        {
            //get new or pending extraction template
            var jobs = _dataManager.GetNewOrPendingJobs();

            foreach (var job in jobs)
            {
                var service = ServiceExist(job.Code);

                if (job.Status ==  PackageStatus.New)
                {
                    if (service == null)
                    {
                        string svcName = job.Code;
                        string svcDispName = "Fintrak " + job.Code + " " + " Service";

                        ServiceInstaller c = new ServiceInstaller();
                        c.InstallService(_servicePath, svcName, svcDispName);

                        //Update job to running
                        job.Status = PackageStatus.Running;
                        job.Remark = "Job running...";
                        _dataManager.UpdateJob(job);
                    }
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
            //get new or pending extraction template
            var jobs = _dataManager.GetJobsToStop();

            lock (jobs)
            {
                foreach (var job in jobs)
                {
                    if (job.Status == PackageStatus.Stop || job.Status == PackageStatus.Done || job.Status == PackageStatus.Fail)
                    {
                        var service = ServiceExist(job.Code);
                        if (service != null)
                        {
                            ServiceInstaller c = new ServiceInstaller();
                            c.UnInstallService(job.Code);

                            //Update job to running
                            job.Status = PackageStatus.Removed;
                            job.Remark = "Job done...";
                            _dataManager.UpdateJob(job);

                            Log.WriteErrorLog(string.Format("Package service for {0} has been un-installed", job.Code));
                        }
                    }
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
    }
}
