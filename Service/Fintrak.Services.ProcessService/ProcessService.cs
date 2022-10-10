using System.Linq;
using Fintrak.Shared.Core.Framework;
using System;
using System.ServiceProcess;
using Fintrak.Shared.Common;
using dts = Microsoft.SqlServer.Dts.Runtime;
using System.Security.Permissions;
using timer = System.Timers;
using Fintrak.Shared.Common.Utils;
using System.Collections.Generic;
using Fintrak.Shared.Core.Entities;
using Fintrak.Services.Data;
using Fintrak.Shared.Common.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Fintrak.Services.ProcessService
{
    public partial class ProcessService : ServiceBase
    {
        dts.Application _app = null;

        DataManager _dataManager = null;
        string _connectionString = string.Empty;

        timer.Timer _timer = null;

        string _serviceName = string.Empty;
        int _currentTrigger = 0;
        int _currentProcess = 0;
        int _redcount ;
        private string processId = ConfigurationManager.AppSettings["ProcessId"];

        public ProcessService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _dataManager = new DataManager();
            _app = new dts.Application();

            var data = args[0];
            Log.WriteErrorLog("Processing extraction for " + data);

            //var indexOfPipe = data.IndexOf("|");
            //_serviceName = data.Substring(0, indexOfPipe);
            //Log.WriteErrorLog("Processing extraction for " + _serviceName);

            _connectionString = _dataManager.GetDataConnection();

            //_connectionString = data.Substring(indexOfPipe + 1, data.Length - (indexOfPipe + 1));
            _serviceName = data;
            //Log.WriteErrorLog("Processing extraction for " + _serviceName);

            _timer = new System.Timers.Timer();
            _timer.Interval = 25000;
            _timer.Elapsed += _timer_Elapsed;
            _timer.Enabled = true;
           
            Log.WriteErrorLog("Process service for " + _serviceName + " now started");

            RunProcess();
        }

        private void _timer_Elapsed(object sender, timer.ElapsedEventArgs e)
        {
            CancelProcess();
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, ControlThread = true)]
        private void CancelProcess()
        {
            ProcessJob job = null;

            try
            {
                job = _dataManager.GetProcessJob(_connectionString,_serviceName);

                if (job != null)
                {
                    if (job.Status == PackageStatus.Cancel)
                    {
                        var processTriggers = _dataManager.GetProcessTriggers(_connectionString,job.ProcessJobId);

                        foreach (var processTrigger in processTriggers)
                        {
                            if (processTrigger.Status == PackageStatus.New || processTrigger.Status == PackageStatus.Pending || processTrigger.Status == PackageStatus.Running)
                            {
                                //Update tigger to Cancel
                                processTrigger.Status = PackageStatus.Cancel;
                                processTrigger.Remark = "Package has been canceled...";
                                _dataManager.UpdateProcessTrigger(_connectionString,processTrigger);
                            }
                        }
                        //Update job to done
                        job.Status = PackageStatus.Stop;
                        job.Remark = "Job has been canceled...";
                        _dataManager.UpdateProcessJob(_connectionString,job);

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
            Log.WriteErrorLog("Process service for " + _serviceName + "has been stopped");
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
                    chr = (duration / 3600);

                    bmin = (duration - (chr * 3600)) / 60;

                    asec = ((duration - (chr * 3600)) - (bmin * 60));
                }
                else
                {
                    bmin = duration / 60;

                    asec = (duration % 60);
                }

                result = chr.ToString().PadLeft(2, '0') + (":" + (bmin.ToString().PadLeft(2, '0') + (":" + asec.ToString().PadLeft(2, '0'))));
            
                return result ;
            }
            catch (Exception ex)
            {
                return "00:00:00";
            }

        }

        private void RunProcess()
        {
            ProcessJob job = null;
            bool testMode = false;

            //get job
            try
            {
                //string processId = ConfigurationManager.AppSettings["ProcessId"];
                job = _dataManager.GetProcessJob(_connectionString,_serviceName);

                if (job != null)
                {
                    Log.WriteErrorLog("Processing for job: " + job.Code);

                    var processTriggers = _dataManager.GetProcessTriggers(_connectionString,job.ProcessJobId);

                    foreach (var processTrigger in processTriggers)
                    {
                        if (processTrigger.Status == PackageStatus.New)
                        {
                            _currentTrigger = processTrigger.ProcessTriggerId;
                            _currentProcess = processTrigger.ProcessId;

                            var process = _dataManager.GetProcess(_connectionString,_currentProcess);
                            if (process != null)
                            {
                                if (process.PackageName != "TestMode")
                                {
                                    if (process.RunType == PackageRunType.Package)
                                    {
                                        RunPackage(process, job, processTrigger);
                                    }
                                    else
                                    {
                                        RunProcedure(process, job, processTrigger);
                                    }

                                    //if (_currentProcess == Convert.ToInt32(processId))
                                    //{
                                    //    _redcount = Count();
                                    //}
                                    //&& Count() != 0
                                    //if (_currentProcess == Convert.ToInt32(processId) && _redcount != 0)
                                    if (_currentProcess == Convert.ToInt32(processId) && Count() != 0)
                                    //{
                                    //    _redcount = Count();
                                    //}
                                    //if (_redcount != 0)
                                    {
                                        Log.WriteErrorLog(string.Format("Go to kill processs."));
                                        goto KILLPROCESS;

                                    }
                                }

                                
                                else
                                {
                                    //Test Mode Operation
                                    Log.WriteErrorLog(string.Format("Starting test mode execution for {0}-{1}.", _serviceName, process.Title));

                                    //Update tigger to Running
                                    processTrigger.Status = PackageStatus.Running;
                                    processTrigger.Remark = "Test Mode currently running...";
                                    _dataManager.UpdateProcessTrigger(_connectionString,processTrigger);
                                    int counter = 0;

                                    do
                                    {
                                        counter += 1;

                                    } while (counter == 200000000000);

                                    processTrigger.Remark = string.Format("Test Mode process {0}-{1} successfully executed: ", _serviceName, process.Title);
                                    processTrigger.Status = PackageStatus.Done;
                                    _dataManager.UpdateProcessTrigger(_connectionString,processTrigger);

                                }
                            }

                        }

                    }

                KILLPROCESS:

                    if (_redcount != 0) 
                    {
                        //Update job to done
                        job.Status = PackageStatus.Done;
                        job.Remark = "Job processing completed...";
                        _dataManager.UpdateProcessJob(_connectionString, job);

                        Log.WriteErrorLog("Job: " + _serviceName + " stop");
                    }
                    else
                    {
                        //Update job to done
                    job.Status = PackageStatus.Done;
                    job.Remark = "Job processing completed...";
                    _dataManager.UpdateProcessJob(_connectionString,job);

                    Log.WriteErrorLog("Job: " + _serviceName + " done" );
                    }

                    
                }
            }
            catch (Exception ex)
            {
                Log.WriteErrorLog(ex);

                if (job != null)
                {
                    var processTriggers = _dataManager.GetProcessTriggers(_connectionString,job.ProcessJobId);

                    foreach (var processTrigger in processTriggers)
                    {
                        if (processTrigger.Status == PackageStatus.New || processTrigger.Status == PackageStatus.Pending || processTrigger.Status == PackageStatus.Running)
                        {
                            //Update tigger to Cancel
                            processTrigger.Status = PackageStatus.Cancel;
                            processTrigger.Remark = "Package has been canceled..." + ex.Message;
                            _dataManager.UpdateProcessTrigger(_connectionString,processTrigger);
                        }
                    }

                    //Update job to fail
                    job.Status = PackageStatus.Fail;
                    job.Remark = "Job processing fail...";
                    _dataManager.UpdateProcessJob(_connectionString,job);
                }

            }


        }

        private void RunProcedure(Processes processes, ProcessJob job, ProcessTrigger processTrigger)
        {
            var actionName = processes.PackageName;
            var parameters = new List<SqlParameter>();

            Log.WriteErrorLog(string.Format("Starting store procedure execution for {0}-{1}.", _serviceName, processes.Title));

            //Update tigger to Running
            processTrigger.Status = PackageStatus.Running;
            processTrigger.Remark = "Store procedure currently running...";
            _dataManager.UpdateProcessTrigger(_connectionString, processTrigger);

            var result = string.Empty;

            SqlDataManager.RunProcedureWithMessage(_connectionString, actionName, parameters.ToArray());

            if (result.Contains("Success"))
            {
                Log.WriteErrorLog(string.Format("Store procedure execution for {0}-{1} successfull.", _serviceName, processes.Title));

                //Update tigger to Done
                processTrigger.Status = PackageStatus.Done;
                processTrigger.Remark = result;
                _dataManager.UpdateProcessTrigger(_connectionString, processTrigger);
            }
            else
            {
                Log.WriteErrorLog(string.Format("Store procedure execution for {0}-{1} failed.{2}", _serviceName, processes.Title, result));

                //Update tigger to Fail
                processTrigger.Status = PackageStatus.Fail;
                processTrigger.Remark = string.Format("Package {0}-{1} failed. {2}", _serviceName, processes.Title, result);
                _dataManager.UpdateProcessTrigger(_connectionString, processTrigger);
            }
        }

        private void RunPackage(Processes processes, ProcessJob job, ProcessTrigger processTrigger)
        {
            var packagePath = processes.PackagePath + processes.PackageName + ".dtsx";
            dts.Package package = _app.LoadPackage(packagePath, null);

            Log.WriteErrorLog(string.Format("Starting package execution for {0}---{1}.", _serviceName, processes.Title));

            //Update tigger to Running
            processTrigger.Status = PackageStatus.Running;
            processTrigger.Remark = "Package currently running...";
            _dataManager.UpdateProcessTrigger(_connectionString, processTrigger);

            var result = package.Execute();
            if (result == dts.DTSExecResult.Success)
            {
                int ssisDuration = 0;
                ssisDuration = (package.ExecutionDuration / 1000);
               // int.TryParse(Math.Round((package.ExecutionDuration / 1000.00), 2).ToString(), out ssisDuration);

                var statusMessage = string.Format("Package execution for {0}-{1} successfull.", _serviceName, processes.Title);

                try
                {
                    if (_currentProcess == Convert.ToInt32(processId) && Count() != 0)
                    {
                        //processTrigger.Remark = string.Format("Package {0}-{1} successfully executed,however cannot proceed with the next process ", _serviceName, processes.Title) + "Duration: (" + HHMMSS(ssisDuration) + ")";
                        processTrigger.Remark = string.Format("Package {0}-{1} successfully executed,however cannot proceed with the next process ", _serviceName, processes.Title) + "Duration- HH:MM:SS: (" + HHMMSS(ssisDuration) + ")";
                     
                        processTrigger.Status = PackageStatus.Done;
                    }
                    else
                    {
                        processTrigger.Remark = string.Format("Package {0}-{1} successfully executed: ", _serviceName, processes.Title) + "Duration- HH:MM:SS: (" + HHMMSS(ssisDuration) + ")";
                        processTrigger.Status = PackageStatus.Done;
                    }
                    //processTrigger.Remark = string.Format("Package {0}-{1} successfully executed: ", _serviceName, processes.Title) + "Duration: (" + HHMMSS(ssisDuration) + ")";
                    //processTrigger.Status = PackageStatus.Done;

                    var message = package.Variables["Message"].Value.ToString();

                    if (!string.IsNullOrEmpty(message))
                    {
                        char firstLevelSeparator = '/';
                        char secondLevelSeparator = '|';

                        var firstLevels = message.Split(firstLevelSeparator);
                        var secondLevels = firstLevels[2].Split(secondLevelSeparator);
    
                        if (firstLevels[1] == "Failed")
                        {
                            statusMessage = string.Format("Package execution for {0}-{1} for operation {2}  Fail.", _serviceName, processes.Title, firstLevels[0]) + "Duration- HH:MM:SS: (" + HHMMSS(ssisDuration) + ")";
                            processTrigger.Remark = string.Format("Package {0}-{1} for operation {2} execution failed: ", _serviceName, processes.Title, firstLevels[0]) + "Duration- HH:MM:SS: (" + HHMMSS(ssisDuration) + ")/n";

                            foreach (var s in secondLevels)
                                processTrigger.Remark += s + "/n";

                            processTrigger.Status = PackageStatus.Fail;
                            Log.WriteErrorLog(processTrigger.Remark);
                        }
                        else
                        {
                            //if (_redcount != 0)
                            //{

                            //    statusMessage = string.Format("Package execution for {0}-{1} for operation {2} successful.", _serviceName, processes.Title, firstLevels[0]) + "Duration: (" + HHMMSS(ssisDuration) + ")";
                            //    processTrigger.Remark = string.Format("Package {0}-{1} for operation {2} execution successful: ", _serviceName, processes.Title, firstLevels[0]) + "Duration: (" + HHMMSS(ssisDuration) + ")";

                            //}
                            //else
                            //{

                            //    statusMessage = string.Format("Package execution for {0}-{1} for operation {2} successful,however cannot proceed with the nest process", _serviceName, processes.Title, firstLevels[0]) + "Duration: (" + HHMMSS(ssisDuration) + ")";
                            //    processTrigger.Remark = string.Format("Package {0}-{1} for operation {2} execution successful:however cannot proceed with the nest process ", _serviceName, processes.Title, firstLevels[0]) + "Duration: (" + HHMMSS(ssisDuration) + ")";
                            //}

                            statusMessage = string.Format("Package execution for {0}-{1} for operation {2} successful.", _serviceName, processes.Title, firstLevels[0]) + "Duration- HH:MM:SS: (" + HHMMSS(ssisDuration) + ")";
                            processTrigger.Remark = string.Format("Package {0}-{1} for operation {2} execution successful: ", _serviceName, processes.Title, firstLevels[0]) + "Duration- HH:MM:SS: (" + HHMMSS(ssisDuration) + ")";

                            foreach (var s in secondLevels)
                                processTrigger.Remark += s;

                            processTrigger.Status = PackageStatus.Done;
                            Log.WriteErrorLog(processTrigger.Remark);
                        }
                    }
                    else
                        Log.WriteErrorLog("Message variable is empty.");

                }
                catch (Exception ex)
                {
                    Log.WriteErrorLog(ex.Message);
                }

                Log.WriteErrorLog(statusMessage);

                //Update tigger to Done
                _dataManager.UpdateProcessTrigger(_connectionString, processTrigger);
                //_Exp = _ProcessName & ": " & _ssisCount & " lines extracted (" & HHMMSS(_ssisDuration) & ")"
            }
            else
            {
                string errorMessage = string.Empty;
                foreach (var error in package.Errors)
                    errorMessage += error.Description;

                Log.WriteErrorLog(string.Format("Package execution for {0}-{1} failed.", _serviceName, processes.Title));

                //Update tigger to Fail
                processTrigger.Status = PackageStatus.Fail;
                processTrigger.Remark = string.Format("Package {0}-{1} failed.", _serviceName, processes.Title) + "\n" + errorMessage;
                _dataManager.UpdateProcessTrigger(_connectionString, processTrigger);
            }
        }


        private int Count()
        {

            int _Count = 0;

            //using (SqlConnection connection = new SqlConnection(_connectionString))
            //{

            //    SqlCommand command = new SqlCommand("spp_count_mpr_opex_checklist", connection);
            //    command.CommandType = System.Data.CommandType.StoredProcedure;
            //    connection.Open();
            //    SqlDataReader reader = command.ExecuteReader();
            //    _Count = reader("Count")
            //    reader.Close();

            //    return _Count;


             string counts = string.Empty;
            using (var con = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("spp_count_mpr_opex_checklist", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
               

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string count = string.Empty;                 

                    if (reader["Count"] != DBNull.Value)
                        count = reader["Count"].ToString();

                    counts = count;
                }

                con.Close();
            }

            return _Count=Convert.ToInt32(counts);

        }
    }
}

