using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Fintrak.Data.Core;
using Fintrak.Data.Core;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Services.ProcessService
{
    public class DataManager
    {
        SolutionRepository _solutionRepository = null;
        SolutionRunDateRepository _runDateRepository = null;
        ProcessJobRepository _jobRepository = null;
        ProcessTriggerRepository _triggerRepository = null;
        ProcessRepository _processRepository = null;

        public DataManager()
        {
            _solutionRepository = new SolutionRepository();
            _runDateRepository = new SolutionRunDateRepository();
            _jobRepository = new ProcessJobRepository();
            _triggerRepository = new ProcessTriggerRepository();
            _processRepository = new ProcessRepository();
        }


        public ProcessJob GetJob(string code)
        {
            var job = _jobRepository.Get().Where(c => c.Code == code).FirstOrDefault();

            return job;
        }

        public List<ProcessTrigger> GetTriggers(int jobId)
        {
            var triggers = _triggerRepository.Get().Where(c => c.ProcessJobId == jobId);

            return triggers.ToList();
        }

        public Process GetProcess(int processId)
        {
            var process = _processRepository.Get().Where(c => c.ProcessId == processId).FirstOrDefault();

            return process;
        }

        public void UpdateJob(ProcessJob job)
        {
            _jobRepository.Update(job);
        }

        public void UpdateTrigger(ProcessTrigger trigger)
        {
            _triggerRepository.Update(trigger);
        }
    }
}
