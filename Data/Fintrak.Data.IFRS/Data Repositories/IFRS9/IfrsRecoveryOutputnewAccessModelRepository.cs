using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;
using Fintrak.Shared.Common.Services;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IIfrsRecoveryOutputnewAccessModelRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsRecoveryOutputnewAccessModelRepository : DataRepositoryBase<IfrsRecoveryOutputnewAccessModel>, IIfrsRecoveryOutputnewAccessModelRepository
    {
        protected override IfrsRecoveryOutputnewAccessModel AddEntity(IFRSContext entityContext, IfrsRecoveryOutputnewAccessModel entity)
        {
            return entityContext.Set<IfrsRecoveryOutputnewAccessModel>().Add(entity);
        }

        protected override IfrsRecoveryOutputnewAccessModel UpdateEntity(IFRSContext entityContext, IfrsRecoveryOutputnewAccessModel entity)
        {
            return (from e in entityContext.Set<IfrsRecoveryOutputnewAccessModel>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<IfrsRecoveryOutputnewAccessModel> GetEntities(IFRSContext entityContext)
        {
            var query = from e in entityContext.Set<IfrsRecoveryOutputnewAccessModel>()
                        select e;
            query = query.OrderBy(a => a.ID).GroupBy(e => e.Refno).Select(a => a.FirstOrDefault()).Take(500);
            return query;
        }

        protected override IfrsRecoveryOutputnewAccessModel GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IfrsRecoveryOutputnewAccessModel>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IfrsRecoveryOutputnewAccessModel> GetIfrsRecoveryOutputnewAccessModelBySearch(string searchParam, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (searchParam.Contains("ExportData "))
                {
                    searchParam = searchParam.Replace("ExportData ", "");
                    var query = (from e in entityContext.Set<IfrsRecoveryOutputnewAccessModel>()
                                 where searchParam.Contains(e.mapped_sector)
                                 orderby e.sector
                                 select new
                                 {
                                     e.Refno,
                                     e.BorrowedID,
                                     e.Recovery,
                                     e.HistoryQuarter,
                                     e.EndOfMonth,
                                     e.sector,
                                     e.mapped_sector,
                                     e.portfolio
                                 });

                    if (searchParam.Substring(0, 5) == "split")
                    {
                        searchParam = searchParam.Substring(5, searchParam.Length - 5);
                        var accounts = (from e in query select new { e.mapped_sector }).Distinct();
                        var count = accounts.Count();
                        var ExportHandler = new ExcelService(path);
                        var accountNo = count > 0 ? accounts.ToList().ElementAt(0).mapped_sector : "";
                        string response = null;
                        for (int i = 0; i < count; ++i)
                        {
                            accountNo = accounts.ToList().ElementAt(i).mapped_sector;
                            response = ExportHandler.Export(query.Where(e => e.mapped_sector == accountNo).ToList(), path + accountNo.Replace("/", ""));
                        }
                    }
                    else
                    {
                        var ExportHandler = new ExcelService(path);
                        string response = ExportHandler.Export(query.ToList(), path);
                    }

                    return new List<IfrsRecoveryOutputnewAccessModel>().Take(0).ToArray().OrderBy(c => c.sector).ThenBy(c => c.mapped_sector); ;
                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsRecoveryOutputnewAccessModel>()
                                 where e.mapped_sector == searchParam
                                 //orderby e.RefNo, e.datepmt
                                 select e);

                    return query.ToArray();
                }
            }
        }

        public IEnumerable<IfrsRecoveryOutputnewAccessModel> GetIfrsRecoveryOutputnewAccessModels(int defaultCount)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<IfrsRecoveryOutputnewAccessModel>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                             select e).Take(defaultCount);
                return query.ToArray();
            }
        }

        public IEnumerable<IfrsRecoveryOutputnewAccessModel> ExportIfrsRecoveryOutputnewAccessModel(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<IfrsRecoveryOutputnewAccessModel>()
                                 select new
                                 {
                                     e.Refno,
                                     e.BorrowedID,
                                     e.Recovery,
                                     e.HistoryQuarter,
                                     e.EndOfMonth,
                                     e.sector,
                                     e.mapped_sector,
                                     e.portfolio
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<IfrsRecoveryOutputnewAccessModel>().Take(0).ToArray().OrderBy(c => c.sector).ThenBy(c => c.mapped_sector);

                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsRecoveryOutputnewAccessModel>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                                 select e);

                    return query.ToArray();
                }
            }
        }
    }
}
