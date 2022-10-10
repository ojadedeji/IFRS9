using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Data.Core;
using Fintrak.Data.Basic;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Shared.Basic.Framework;

namespace Fintrak.Services.ExtractionPackageService
{
    public class DataManager
    {
        SolutionRepository _solutionRepository = null;
        SolutionRunDateRepository _runDateRepository = null;
        ExtractionJobRepository _jobRepository = null;
        ExtractionTriggerRepository _triggerRepository = null;
        ExtractionRepository _extractionRepository = null;

        public DataManager()
        {
            _solutionRepository = new SolutionRepository();
            _runDateRepository = new SolutionRunDateRepository();
            _jobRepository = new ExtractionJobRepository();
            _triggerRepository = new ExtractionTriggerRepository();
            _extractionRepository = new ExtractionRepository();
        }


        public ExtractionJob GetJob(string code)
        {
            var job = _jobRepository.Get().Where(c => c.Code == code).FirstOrDefault();

            return job;
        }

        public List<ExtractionTrigger> GetTriggers(int jobId)
        {
            var triggers = _triggerRepository.Get().Where(c => c.ExtractionJobId == jobId);

            return triggers.ToList();
        }

        public Extraction GetExtraction(int extractionId)
        {
            var extraction = _extractionRepository.Get().Where(c => c.ExtractionId == extractionId).FirstOrDefault();

            return extraction;
        }

        public void UpdateJob(ExtractionJob job)
        {
            _jobRepository.Update(job);
        }

        public void UpdateTrigger(ExtractionTrigger trigger)
        {
            _triggerRepository.Update(trigger);
        }
    }
}
