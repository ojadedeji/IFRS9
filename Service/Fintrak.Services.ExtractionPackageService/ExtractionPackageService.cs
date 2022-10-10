using System.Linq;
using Fintrak.Shared.Basic.Framework;
using System;
using System.ServiceProcess;
using Fintrak.Shared.Common;
using dts = Microsoft.SqlServer.Dts.Runtime;
using System.Security.Permissions;
using timer = System.Timers;
using Fintrak.Shared.Common.Utils;


namespace Fintrak.Services.ExtractionPackageService
{
    public partial class ExtractionPackageService : ServiceBase
    {
        dts.Application _app = null;
        DataManager _dataManager = null;
        timer.Timer _timer = null;

        string _serviceName = string.Empty;
        int _currentTrigger = 0;
        int _currentExtraction = 0;

        public ExtractionPackageService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                _dataManager = new DataManager();
                _app = new dts.Application();

                _serviceName = args[0];

                _timer = new System.Timers.Timer();
                _timer.Interval = 25000;
                _timer.Elapsed += _timer_Elapsed;
                _timer.Enabled = true;

                Log.WriteErrorLog("Extraction service for " + _serviceName + " started");

                //RunExtraction();
            }
            catch (Exception ex)
            {
                Log.WriteErrorLog(ex);
            }
        }

        private void _timer_Elapsed(object sender, timer.ElapsedEventArgs e)
        {
            //CancelExtraction();
        }

        protected override void OnStop()
        {
            _timer.Enabled = false;
            Log.WriteErrorLog("Extraction service for " + _serviceName + "has been stopped");
        }
    }
}
