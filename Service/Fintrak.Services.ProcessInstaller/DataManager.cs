using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Data.Core;
using Fintrak.Data.Core;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Services.ProcessInstaller
{
    public class DataManager
    {
        SolutionRepository _solutionRepository = null;
        SolutionRunDateRepository _runDateRepository = null;
        ProcessJobRepository _jobRepository = null;
        ProcessTriggerRepository _triggerRepository = null;
        ProcessRepository _extractionRepository = null;

        public DataManager()
        {
            _solutionRepository = new SolutionRepository();
            _runDateRepository = new SolutionRunDateRepository();
            _jobRepository = new ProcessJobRepository();
            _triggerRepository = new ProcessTriggerRepository();
            _extractionRepository = new ProcessRepository();
        }


        public List<ProcessJob> GetNewOrPendingJobs()
        {
            var jobs = _jobRepository.Get().Where(c=>c.Status == PackageStatus.New ||  c.Status == PackageStatus.Pending);

            return jobs.ToList();
        }
        public List<ProcessJob> GetJobsToStop()
        {
            var jobs = _jobRepository.Get().Where(c => c.Status == PackageStatus.Stop || c.Status == PackageStatus.Done || c.Status == PackageStatus.Fail);

            return jobs.ToList();
        }
        public void UpdateJob(ProcessJob job)
        {
            _jobRepository.Update(job);
        }
  
    }
}
