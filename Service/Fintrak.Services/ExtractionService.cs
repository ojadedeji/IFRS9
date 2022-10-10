using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Fintrak.Services
{
    partial class ExtractionService : ServiceBase
    {
        Timer _timer = null;

        public ExtractionService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _timer = new Timer();
            _timer.Interval = 60000;
            _timer.Elapsed +=_timer_Elapsed;
            _timer.Enabled = true;
            Log.WriteErrorLog("Extraction service started");
        }
 
        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Log.WriteErrorLog("Executing extraction job.");
        }

        protected override void OnStop()
        {
            _timer.Enabled = false;
            Log.WriteErrorLog("Extraction service stopped");
        }
    }
}
