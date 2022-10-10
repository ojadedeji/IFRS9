using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Data.Core;
using Fintrak.Data.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Utils;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Core.Framework;
using Fintrak.Data.SystemCore;
using Fintrak.Data.SystemCore.Contracts;
using Fintrak.Shared.SystemCore.Entities;
using Fintrak.Shared.Common.Data;

namespace Fintrak.Services.Data
{
    public class DataManager
    {
        SolutionRepository _solutionRepository = null;
   
             
        public DataManager()
        {
            _solutionRepository = new SolutionRepository();

        }

        #region Extraction

        public ExtractionJob GetExtractionJob(string connectionString, string code)
        {
            using (var entityContext = new CoreContext(connectionString))
            {
                var query = from e in entityContext.Set<ExtractionJob>()
                            select e;

                var job = query.Where(c => c.Code == code).FirstOrDefault();

                return job;
            }
        }

        public List<ExtractionJob> GetNewOrPendingExtractionJobs(string connectionString)
        {
            using (var entityContext = new CoreContext(connectionString))
            {
                var query = from e in entityContext.Set<ExtractionJob>()
                            select e;

                var jobs = query.Where(c => c.Status == PackageStatus.New || c.Status == PackageStatus.Pending);

                return jobs.ToList();
            }
        }

        public List<ExtractionJob> GetExtractionJobsToStop(string connectionString)
        {
            using (var entityContext = new CoreContext(connectionString))
            {
                var query = from e in entityContext.Set<ExtractionJob>()
                            select e;

                var jobs = query.Where(c => c.Status == PackageStatus.Stop || c.Status == PackageStatus.Done || c.Status == PackageStatus.Fail);

                return jobs.ToList();
            }
        }

        public void UpdateExtractionJob(string connectionString, ExtractionJob job)
        {
            using (var entityContext = new CoreContext(connectionString))
            {
                var existingEntity = (from e in entityContext.Set<ExtractionJob>()
                             where e.ExtractionJobId == job.ExtractionJobId
                        select e).FirstOrDefault();

                SimpleMapper.PropertyMap(job, existingEntity);

                entityContext.SaveChanges();
            }
        }

        public List<ExtractionTrigger> GetExtractionTriggers(string connectionString, int jobId)
        {
            using (var entityContext = new CoreContext(connectionString))
            {
                var query = from e in entityContext.Set<ExtractionTrigger>()
                            select e;

                var entities = query.Where(c => c.ExtractionJobId == jobId).ToList();

                return entities;
            }
        }

        public Extraction GetExtraction(string connectionString,int extractionId)
        {
            using (var entityContext = new CoreContext(connectionString))
            {
                var query = from e in entityContext.Set<Extraction>()
                            select e;

                var entitiy = query.Where(c => c.ExtractionId == extractionId).FirstOrDefault();

                return entitiy;
            }
        }

        public void UpdateExtractionTrigger(string connectionString, ExtractionTrigger trigger)
        {
            using (var entityContext = new CoreContext(connectionString))
            {
                var existingEntity = (from e in entityContext.Set<ExtractionTrigger>()
                                      where e.ExtractionTriggerId == trigger.ExtractionTriggerId
                                      select e).FirstOrDefault();

                SimpleMapper.PropertyMap(trigger, existingEntity);

                entityContext.SaveChanges();
            }
        }

        #endregion

        #region Process

        public ProcessJob GetProcessJob(string connectionString, string code)
        {
            using (var entityContext = new CoreContext(connectionString))
            {
                var query = from e in entityContext.Set<ProcessJob>()
                            select e;

                var job = query.Where(c => c.Code == code).FirstOrDefault();

                return job;
            }
        }

        public List<ProcessJob> GetNewOrPendingProcessJobs(string connectionString)
        {
            using (var entityContext = new CoreContext(connectionString))
            {
                var query = from e in entityContext.Set<ProcessJob>()
                            select e;

                var jobs = query.Where(c => c.Status == PackageStatus.New || c.Status == PackageStatus.Pending);

                return jobs.ToList();
            }
        }

        public List<ProcessJob> GetProcessJobsToStop(string connectionString)
        {
            using (var entityContext = new CoreContext(connectionString))
            {
                var query = from e in entityContext.Set<ProcessJob>()
                            select e;

                var jobs = query.Where(c => c.Status == PackageStatus.Stop || c.Status == PackageStatus.Done || c.Status == PackageStatus.Fail);

                return jobs.ToList();
            }
        }

        public void UpdateProcessJob(string connectionString, ProcessJob job)
        {
            using (var entityContext = new CoreContext(connectionString))
            {
                var existingEntity = (from e in entityContext.Set<ProcessJob>()
                                      where e.ProcessJobId == job.ProcessJobId
                                      select e).FirstOrDefault();

                SimpleMapper.PropertyMap(job, existingEntity);

                entityContext.SaveChanges();
            }
        }

        public List<ProcessTrigger> GetProcessTriggers(string connectionString, int jobId)
        {
            using (var entityContext = new CoreContext(connectionString))
            {
                var query = from e in entityContext.Set<ProcessTrigger>()
                            select e;

                var entities = query.Where(c => c.ProcessJobId == jobId).ToList();

                return entities;
            }
        }

        public Processes GetProcess(string connectionString, int processId)
        {
            using (var entityContext = new CoreContext(connectionString))
            {
                var query = from e in entityContext.Set<Processes>()
                            select e;

                var entitiy = query.Where(c => c.ProcessId == processId).FirstOrDefault();

                return entitiy;
            }
        }

        public void UpdateProcessTrigger(string connectionString, ProcessTrigger trigger)
        {
            using (var entityContext = new CoreContext(connectionString))
            {
                var existingEntity = (from e in entityContext.Set<ProcessTrigger>()
                                      where e.ProcessTriggerId == trigger.ProcessTriggerId
                                      select e).FirstOrDefault();

                SimpleMapper.PropertyMap(trigger, existingEntity);

                entityContext.SaveChanges();
            }
        }

        #endregion

        public List<string> GetDataConnections()
        {
            var connectionStrings = new List<string>();

            IDatabaseRepository databaseRepository = new DatabaseRepository();
            var databases = databaseRepository.Get();

            foreach (var db in databases)
                connectionStrings.Add(string.Format("Data Source= {0};Initial Catalog={1};User ={2};Password={3};Integrated Security={4}", db.ServerName, db.DatabaseName, db.UserName, db.Password, db.IntegratedSecurity));



            return connectionStrings;
        }

        public string GetDataConnection()
        {
            var connectionStrings = "";

                IDatabaseRepository databaseRepository = new DatabaseRepository();
                var databases = databaseRepository.Get();


                foreach (var db in databases)
                connectionStrings = string.Format("Data Source= {0};Initial Catalog={1};User ={2};Password={3};Integrated Security={4}", db.ServerName, db.DatabaseName, db.UserName, db.Password, db.IntegratedSecurity);


            return connectionStrings;
        }
    }
}
