using System.Linq;
using Fintrak.Shared.Core.Framework;
using System;
using System.ServiceProcess;
using Fintrak.Shared.Common;
using dts = Microsoft.SqlServer.Dts.Runtime;
using System.Security.Permissions;
using timer = System.Timers;
using Fintrak.Shared.Common.Utils;
using Fintrak.Shared.Core.Entities;
using Fintrak.Services.Data;
using Fintrak.Shared.Common.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Fintrak.Shared.Common;

namespace Fintrak.Services.ExtractionProcess
{
    partial class ExtractionProcess : ServiceBase
    {
        dts.Application _app = null;

        DataManager _dataManager = null;
        string _connectionString = string.Empty;
       // string _dbaseStrings = string.Empty;

        timer.Timer _timer = null;
        string _serviceName = string.Empty;

        int _currentTrigger = 0;
        int _currentExtraction = 0;

        public ExtractionProcess()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                _dataManager = new DataManager();
             
                _app = new dts.Application();           
                

               var data = args[0];
               Log.WriteErrorLog("Processing extraction for " + data);
               _dataManager = new DataManager();
               //  List<string> _dbaseStrings = null;
               _connectionString = _dataManager.GetDataConnection();
               //_connectionString = "Data Source=10.0.0.96\\FINTRAKSQL2014;Initial Catalog=FintrakDB;User =sa;Password=sqluser10$;Integrated Security=False";  //_dataManager.GetDataConnections();

               //Log.WriteErrorLog("Processing extraction for " + _connectionString);
               //  Log.WriteErrorLog("Processing extraction for test " + "Now Here");

                //var indexOfPipe = data.IndexOf("|");
                //_serviceName = data.Substring(0,indexOfPipe);
                 _serviceName = data;
                 Log.WriteErrorLog("Processing extraction for Passed " + _serviceName);

               // _connectionString = _dbaseStrings; //data.Substring(indexOfPipe + 1, data.Length - (indexOfPipe + 1));
                

                _timer = new System.Timers.Timer();
                _timer.Interval = 25000;
                _timer.Elapsed += _timer_Elapsed;
                _timer.Enabled = true;

                Log.WriteErrorLog("Extraction service for " + _serviceName + " started");

                RunExtraction();
            }
            catch (Exception ex)
            {
                Log.WriteErrorLog(ex);
            }
        }

        private void _timer_Elapsed(object sender, timer.ElapsedEventArgs e)
        {
            CancelExtraction();
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, ControlThread = true)]
        private void CancelExtraction()
        {
            ExtractionJob job = null;

            try
            {
                job = _dataManager.GetExtractionJob(_connectionString,_serviceName);

                if (job != null)
                {
                    if (job.Status == PackageStatus.Cancel)
                    {
                        var extractionTriggers = _dataManager.GetExtractionTriggers(_connectionString,job.ExtractionJobId);

                        foreach (var extractionTrigger in extractionTriggers)
                        {
                            if (extractionTrigger.Status == PackageStatus.New || extractionTrigger.Status == PackageStatus.Pending || extractionTrigger.Status == PackageStatus.Running)
                            {
                                //Update tigger to Cancel
                                extractionTrigger.Status = PackageStatus.Cancel;
                                extractionTrigger.Remark = "Package has been canceled...";
                                _dataManager.UpdateExtractionTrigger(_connectionString,extractionTrigger);
                            }
                        }
                        //Update job to done
                        job.Status = PackageStatus.Stop;
                        job.Remark = "Job has been canceled...";
                        _dataManager.UpdateExtractionJob(_connectionString,job);

                        Log.WriteErrorLog("Job: " + _serviceName + " canceled");
                        this.Stop();
                    }

                }
            }
            catch (Exception ex)
            {
                Log.WriteErrorLog(ex);
            }
            

        }

        protected override void OnStop()
        {
            _timer.Enabled = false;
            Log.WriteErrorLog("Extraction service for " + _serviceName + " has been stopped");
        }

        public static string HHMMSS(int duration)
        {
            try
            {
                string result = "";
                int chr = 0;
                int asec = 0;
                int bmin = 0;
                if ((duration >= 3600))
                {
                    chr = (duration/3600);
                  
                    bmin = (duration- (chr * 3600))/60;
         
                    asec = ((duration- (chr * 3600))- (bmin * 60));
                }
                else
                {
                    bmin = duration/60;
             
                    asec = (duration % 60);
                }

                result = chr.ToString().PadLeft(2, '0') + (":"+ (bmin.ToString().PadLeft(2, '0') + (":" + asec.ToString().PadLeft(2, '0'))));
                return result;
            }
            catch (Exception ex)
            {
                return "00:00:00";
            }
        
        }



        private void RunExtraction()
        {
            ExtractionJob job = null;

            try
            {
                //get job
                job = _dataManager.GetExtractionJob(_connectionString,_serviceName);

                if (job != null)
                {
                    Log.WriteErrorLog("Processing for job: " + job.Code);

                    var extractionTriggers = _dataManager.GetExtractionTriggers(_connectionString,job.ExtractionJobId);

                    foreach (var extractionTrigger in extractionTriggers)
                    {
                        if (extractionTrigger.Status == PackageStatus.New)
                        {
                            _currentTrigger = extractionTrigger.ExtractionTriggerId;
                            _currentExtraction = extractionTrigger.ExtractionId;

                            var extraction = _dataManager.GetExtraction(_connectionString,_currentExtraction);
                            if (extraction != null)
                            {
                                if (extraction.RunType == PackageRunType.Package)
                                {
                                    RunPackage(extraction,job, extractionTrigger);
                                }
                                else
                                {
                                    RunProcedure(extraction,job, extractionTrigger);
                                }
                            }
                        }
                    }

                    //Update job to done
                    job.Status = PackageStatus.Done;
                    job.Remark = "Job processing completed...";
                    _dataManager.UpdateExtractionJob(_connectionString,job);

                    Log.WriteErrorLog("Job: " + _serviceName + " done");
                }
            }
            catch (Exception ex)
            {
                Log.WriteErrorLog(ex);

                if (job != null)
                {
                    var extractionTriggers = _dataManager.GetExtractionTriggers(_connectionString,job.ExtractionJobId);

                    foreach (var extractionTrigger in extractionTriggers)
                    {
                        if (extractionTrigger.Status == PackageStatus.New || extractionTrigger.Status == PackageStatus.Pending || extractionTrigger.Status == PackageStatus.Running)
                        {
                            //Update tigger to Cancel
                            extractionTrigger.Status = PackageStatus.Cancel;
                            extractionTrigger.Remark = "Package has been canceled...";
                            _dataManager.UpdateExtractionTrigger(_connectionString,extractionTrigger);
                        }
                    }

                    //Update job to fail
                    job.Status = PackageStatus.Fail;
                    job.Remark = "Job processing fail...";
                    _dataManager.UpdateExtractionJob(_connectionString,job);
                }
            }
        }

        private void RunProcedure(Extraction extraction,ExtractionJob job, ExtractionTrigger extractionTrigger)
        {
            var actionName = extraction.PackageName;
            var parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter() { ParameterName = "StartDate", Value = job.StartDate.GetOracleDate() });
            parameters.Add(new SqlParameter() { ParameterName = "EndDate", Value = job.EndDate.GetOracleDate() });
            parameters.Add(new SqlParameter() { ParameterName = "FYYear", Value = string.Format("FY{0}", job.EndDate.Year.ToString()) });
            parameters.Add(new SqlParameter() { ParameterName = "Year", Value = string.Format("{0}", job.EndDate.Year.ToString()) });
            parameters.Add(new SqlParameter() { ParameterName = "Period", Value = string.Format("M{0}", job.EndDate.Month.ToString().PadLeft(2, '0')) });

            Log.WriteErrorLog(string.Format("Starting store procedure execution for {0}-{1}.", _serviceName, extraction.Title));

            //Update tigger to Running
            extractionTrigger.Status = PackageStatus.Running;
            extractionTrigger.Remark = "Store procedure currently running...";
            _dataManager.UpdateExtractionTrigger(_connectionString, extractionTrigger);

            var result = string.Empty;

            SqlDataManager.RunProcedureWithMessage(_connectionString,actionName, parameters.ToArray());

            if (result.Contains("Success"))
            {
                Log.WriteErrorLog(string.Format("Store procedure execution for {0}-{1} successfull.", _serviceName, extraction.Title));

                //Update tigger to Done
                extractionTrigger.Status = PackageStatus.Done;
                extractionTrigger.Remark = result;
                _dataManager.UpdateExtractionTrigger(_connectionString, extractionTrigger);
            }
            else
            {
                Log.WriteErrorLog(string.Format("Store procedure execution for {0}-{1} failed.{2}", _serviceName, extraction.Title, result));

                //Update tigger to Fail
                extractionTrigger.Status = PackageStatus.Fail;
                extractionTrigger.Remark = string.Format("Package {0}-{1} failed. {2}", _serviceName, extraction.Title,result);
                _dataManager.UpdateExtractionTrigger(_connectionString, extractionTrigger);
            }
        }

        private void RunPackage(Extraction extraction, ExtractionJob job, ExtractionTrigger extractionTrigger)
        {
            var packagePath = extraction.PackagePath + extraction.PackageName + ".dtsx";
            dts.Package package = _app.LoadPackage(packagePath, null);

            Log.WriteErrorLog(string.Format("Starting package execution for {0}-{1}.", _serviceName, extraction.Title));

            //Update tigger to Running
            extractionTrigger.Status = PackageStatus.Running;
            extractionTrigger.Remark = "Package currently running...";
            _dataManager.UpdateExtractionTrigger(_connectionString, extractionTrigger);
            //DD-mon-YYYY
            extraction.ScriptText = extraction.ScriptText.Replace("@StartDate", job.StartDate.GetOracleDate());
            extraction.ScriptText = extraction.ScriptText.Replace("@EndDate", job.EndDate.GetOracleDate());
            extraction.ScriptText = extraction.ScriptText.Replace("@FYYear", string.Format("FY{0}", job.EndDate.Year.ToString()));
            extraction.ScriptText = extraction.ScriptText.Replace("@Year", string.Format("{0}", job.EndDate.Year.ToString()));
            extraction.ScriptText = extraction.ScriptText.Replace("@Period", string.Format("M{0}", job.EndDate.Month.ToString().PadLeft(2, '0')));

            package.Variables["StorProc"].Value = extraction.ScriptText;//"Script", true, "", extraction.ScriptText);

            var result = package.Execute();
            if (result == dts.DTSExecResult.Success)
            {
                int ssisDuration = 0;
                //int.TryParse(Math.Round((package.ExecutionDuration / 1000.00), 2).ToString(), out ssisDuration);

                ssisDuration = (package.ExecutionDuration / 1000);

                Log.WriteErrorLog(string.Format("Package execution for {0}-{1} successfull.", _serviceName, extraction.Title));

                //Update tigger to Done

                extractionTrigger.Status = PackageStatus.Done;
                //extractionTrigger.Remark = string.Format("{0}{1}lines extracted ({2})", string.Format("Package {0}-{1} successfully executed: ", _serviceName, extraction.Title), package.Variables["RecCount"].Value.ToString(), DateTimeExtensions.GetRealTime(ssisDuration));
                extractionTrigger.Remark = string.Format("{0}{1}lines extracted ({2})", string.Format("Package {0}-{1} successfully executed: ", _serviceName, extraction.Title), package.Variables["RecCount"].Value.ToString(), HHMMSS(ssisDuration));
                _dataManager.UpdateExtractionTrigger(_connectionString, extractionTrigger);
                //_Exp = _ProcessName & ": " & _ssisCount & " lines extracted (" & HHMMSS(_ssisDuration) & ")"
            }
            else
            {
                string errorMessage = string.Empty;
                foreach (var error in package.Errors)
                    errorMessage += error.Description;

                Log.WriteErrorLog(string.Format("Package execution for {0}-{1} failed.{2}", _serviceName, extraction.Title, errorMessage));

                //Update tigger to Fail
                extractionTrigger.Status = PackageStatus.Fail;
                extractionTrigger.Remark = string.Format("Package {0}-{1} failed.", _serviceName, extraction.Title) + "\n" + errorMessage;
                _dataManager.UpdateExtractionTrigger(_connectionString, extractionTrigger);
            }
        }
    }
}
