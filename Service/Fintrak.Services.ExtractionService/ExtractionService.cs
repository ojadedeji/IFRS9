using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Fintrak.Shared.Common;

namespace Fintrak.Services.ExtractionService
{
    public partial class ExtractionService : ServiceBase
    {
        public ExtractionService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Log.WriteErrorLog("Extraction service for started testttt");
        }

        protected override void OnStop()
        {
            Log.WriteErrorLog("Extraction service for stop testttt");
        }
    }
}
