using System;
using System.Linq;
using System.Data.Entity.Infrastructure;
using System.Collections.Generic;

namespace Fintrak.Shared.AuditService
{
    public class AuditManager
    {
        private DataContext _context;

        public AuditManager()
        {

        }


        public AuditManager(string connection)
        {
            _context = new DataContext(connection);
        }

        public void AddAudit(DbEntityEntry entry,string loginName)
        {
            _context.AuditTrailFactory(entry,loginName);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public AuditTrail Get(int id)
        {
            var repo = new AuditTrailRepository();
            return repo.Get(id);
        }

        public IEnumerable<AuditTrail> Get()
        {
            var repo = new AuditTrailRepository();
            return repo.Get();
        }

        public IEnumerable<AuditTrail> GetByDate(DateTime fromDate, DateTime toDate)
        {
            var repo = new AuditTrailRepository();
            return repo.GetByDate(fromDate, toDate);
        }

        public IEnumerable<AuditTrail> GetAuditTrailByTab(AuditAction action)
        {
            var repo = new AuditTrailRepository();
            return repo.GetAuditTrailByTab(action);
        }

        public IEnumerable<AuditTrail> GetByTable(string tablename, DateTime fromDate, DateTime toDate)
        {
            var repo = new AuditTrailRepository();
            return repo.GetByTable(tablename,fromDate, toDate);
        }

        public IEnumerable<AuditTrail> GetByLoginID(string loginID, DateTime fromDate, DateTime toDate)
        {
            var repo = new AuditTrailRepository();
            return repo.GetByLoginID(loginID,fromDate, toDate);
        }

        public List<AuditTrail> GetByAction(string action, DateTime fromDate, DateTime toDate)
        {
            var repo = new AuditTrailRepository();
            return repo.GetByAction(action,fromDate, toDate);
        }
    }
}
