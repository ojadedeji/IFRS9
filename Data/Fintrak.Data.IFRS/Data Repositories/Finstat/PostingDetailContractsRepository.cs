using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;
using Fintrak.Shared.IFRS.Framework;
using Fintrak.Shared.Common.Services;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IPostingDetailContractsRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PostingDetailContractsRepository : DataRepositoryBase<PostingDetailContracts>, IPostingDetailContractsRepository
    {

        protected override PostingDetailContracts AddEntity(IFRSContext entityContext, PostingDetailContracts entity)
        {
            return entityContext.Set<PostingDetailContracts>().Add(entity);
        }

        protected override PostingDetailContracts UpdateEntity(IFRSContext entityContext, PostingDetailContracts entity)
        {
            return (from e in entityContext.Set<PostingDetailContracts>() 
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<PostingDetailContracts> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<PostingDetailContracts>()
                   select e;
        }

        protected override PostingDetailContracts GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<PostingDetailContracts>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<PostingDetailContracts> GetEntitiesByFilter(string filter, string path, int count2)
        {
            IFRSContext entityContext = new IFRSContext();

            if (filter.Contains("ExportData"))
            {
                filter = filter.Replace("ExportData", "");
                var query = (from e in entityContext.Set<PostingDetailContracts>()
                             where (filter.Contains(e.Category) || string.IsNullOrEmpty(filter))
                             select new
                             {
                                 RefNumber = e.Refno,
                                 Filter = e.GLCode,
                                 IFRS_CarryingAmount = e.AmortizedCost,
                                 GAAP_CarryingAmount = e.PrincipalOustandingBal,
                                 IFRS_Adjustment = e.AmortizedCost - e.PrincipalOustandingBal,
                                 e.date_pmt
                             });

                if (filter.Length >= 5 && filter.Substring(0, 5) == "split")
                {
                    filter = filter.Substring(5, filter.Length - 5);
                    var accounts = (from e in query select new { e.Filter }).Distinct();
                    var count = accounts.Count();
                    var ExportHandler = new ExcelService(path);
                    var accountNo = count > 0 ? accounts.ToList().ElementAt(0).Filter : "";
                    string response = null;
                    for (int i = 0; i < count; ++i)
                    {
                        accountNo = accounts.ToList().ElementAt(i).Filter;
                        response = ExportHandler.Export(query.Where(e => e.Filter == accountNo).ToList(), path + accountNo.Replace("/", ""));
                    }
                }
                else
                {
                    var ExportHandler = new ExcelService(path);
                    string response = ExportHandler.Export(query.ToList(), path);
                }

                return new List<PostingDetailContracts>().Take(0).ToArray();
            }
            else
            {
                var query = (from e in entityContext.Set<PostingDetailContracts>()
                             where (filter.Contains(e.GLCode))
                             select e).Take(count2);

                return query.ToArray();
            }
        }

        public IEnumerable<string> GetDistinctPostingFilters(int count)
        {
            IFRSContext entityContext = new IFRSContext();

            var query = (entityContext.PostingDetailContractsSet.Select<PostingDetailContracts, string>(r => r.GLCode)).Distinct();

            return query.ToFullyLoaded();
        }
    } 
}
