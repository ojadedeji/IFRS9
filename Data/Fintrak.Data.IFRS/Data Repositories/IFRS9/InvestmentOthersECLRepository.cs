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
    [Export(typeof(IInvestmentOthersECLRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class InvestmentOthersECLRepository : DataRepositoryBase<InvestmentOthersECL>, IInvestmentOthersECLRepository
    {
        protected override InvestmentOthersECL AddEntity(IFRSContext entityContext, InvestmentOthersECL entity)
        {
            return entityContext.Set<InvestmentOthersECL>().Add(entity);
        }

        protected override InvestmentOthersECL UpdateEntity(IFRSContext entityContext, InvestmentOthersECL entity)
        {
            return (from e in entityContext.Set<InvestmentOthersECL>()
                    where e.ecl_Id == entity.ecl_Id
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<InvestmentOthersECL> GetEntities(IFRSContext entityContext)
        {
            var query = from a in entityContext.InvestmentOthersECLSet
                        select a;

            return query.ToFullyLoaded().OrderBy(a => a.period).GroupBy(a => a.RefNo).Select(a => a.First()).Take(500);
        }


        public IEnumerable<InvestmentOthersECL> GetAvailableInvestmentOthersECL(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<InvestmentOthersECL>()
                                 select new
                                 {
                                     e.RefNo,
                                     e.counterparty,
                                     e.asset_type,
                                     e.period,
                                     e.eclbest,
                                     e.ecloptimisitc,
                                     e.ecldownturn,
                                     e.unsecured_exposure,
                                     e.lgdbest,
                                     e.lgdoptimistic,
                                     e.lgdDown,
                                     e.lgd_macro_best,
                                     e.lgd_macro_optim,
                                     e.lgd_macro_down,
                                     e.pdbest,
                                     e.pdoptimistic,
                                     e.pd_down,
                                     e.monthly_int,
                                     e.interest_rate,
                                     e.eir,
                                     e.discount_factor,
                                     e.stage,
                                     e.rating
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<InvestmentOthersECL>().Take(0).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<InvestmentOthersECL>().Take(defaultCount)
                                 select e);

                    return query.ToArray();
                }
            }
        }
        protected override InvestmentOthersECL GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<InvestmentOthersECL>()
                         where e.ecl_Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
        public IEnumerable<InvestmentOthersECL> GetInvestmentOthersECLByRefNo(string RefNo)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.InvestmentOthersECLSet
                            where a.RefNo.Contains(RefNo)
                            select a;

                return query.ToFullyLoaded();
            }
        }
    }
}