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
    [Export(typeof(IHistoricalMacroVariableInputRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class HistoricalMacroVariableInputRepository : DataRepositoryBase<HistoricalMacroVariableInput>, IHistoricalMacroVariableInputRepository
    {
        protected override HistoricalMacroVariableInput AddEntity(IFRSContext entityContext, HistoricalMacroVariableInput entity)
        {
            return entityContext.Set<HistoricalMacroVariableInput>().Add(entity);
        }

        protected override HistoricalMacroVariableInput UpdateEntity(IFRSContext entityContext, HistoricalMacroVariableInput entity)
        {
            return (from e in entityContext.Set<HistoricalMacroVariableInput>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<HistoricalMacroVariableInput> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<HistoricalMacroVariableInput>()
                   select e;
        }

        protected override HistoricalMacroVariableInput GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<HistoricalMacroVariableInput>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<HistoricalMacroVariableInput> GetAvailableHistoricalMacroVariableInput(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<HistoricalMacroVariableInput>()
                                 select new
                                 {
                                     e.ReportDate,
                                     e.NPL,
                                     e.GDP,
                                     e.Inflation,
                                     e.Ex_Rate
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<HistoricalMacroVariableInput>().Take(0).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<HistoricalMacroVariableInput>().Take(defaultCount) 
                                 select e);

                    return query.ToArray();
                }
            }
        }


        public IEnumerable<HistoricalMacroVariableInput> GetHistoricalMacroVariableInputBySearch(string searchParam, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (searchParam.Contains("ExportData "))
                {
                    searchParam = searchParam.Replace("ExportData ", "");
                    var query = (from e in entityContext.Set<HistoricalMacroVariableInput>()
                                 where searchParam.Contains(e.ReportDate.ToString())
                                 select new
                                 {
                                     e.ReportDate,
                                     e.NPL,
                                     e.GDP,
                                     e.Inflation,
                                     e.Ex_Rate
                                 });

                    if (searchParam.Substring(0, 5) == "split")
                    {
                        searchParam = searchParam.Substring(5, searchParam.Length - 5);
                        var products = (from e in query select new { e.ReportDate }).Distinct();
                        var count = products.Count();
                        var ExportHandler = new ExcelService(path);
                        var ReportDate = count > 0 ? products.ToList().ElementAt(0).ReportDate.ToString() : "";
                        string response = null;
                        for (int i = 0; i < count; ++i)
                        {
                            ReportDate = products.ToList().ElementAt(i).ReportDate.ToString();
                            response = ExportHandler.Export(query.Where(e => e.ReportDate.ToString() == ReportDate).ToList(), path + ReportDate.Replace("/", ""));
                        }
                    }
                    else
                    {
                        var ExportHandler = new ExcelService(path);
                        string response = ExportHandler.Export(query.ToList(), path);
                    }

                    return new List<HistoricalMacroVariableInput>().Take(0).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<HistoricalMacroVariableInput>()
                                 where e.ReportDate.ToString() == searchParam
                                 select e);
                    return query.ToArray();
                }
            }
        }


        public IEnumerable<HistoricalMacroVariableInput> GetHistoricalMacroVariableInputByReportDate(DateTime ReportDateVal)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<HistoricalMacroVariableInput>()
                             where e.ReportDate == ReportDateVal
                             select e);

                return query.ToArray();
            }
        }

    }
}