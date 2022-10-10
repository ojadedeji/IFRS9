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
    [Export(typeof(IIfrsCorporateEclRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsCorporateEclRepository : DataRepositoryBase<IfrsCorporateEcl>, IIfrsCorporateEclRepository
    {
        protected override IfrsCorporateEcl AddEntity(IFRSContext entityContext, IfrsCorporateEcl entity)
        {
            return entityContext.Set<IfrsCorporateEcl>().Add(entity);
        }

        protected override IfrsCorporateEcl UpdateEntity(IFRSContext entityContext, IfrsCorporateEcl entity)
        {
            return (from e in entityContext.Set<IfrsCorporateEcl>()
                    where e.ecl_id == entity.ecl_id
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<IfrsCorporateEcl> GetEntities(IFRSContext entityContext)
        {
            var query = from e in entityContext.Set<IfrsCorporateEcl>()
                        select e;
            query = query.OrderBy(a => a.period).GroupBy(e => e.refno).Select(a => a.FirstOrDefault()).Take(500);
            return query;
        }

        protected override IfrsCorporateEcl GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IfrsCorporateEcl>()
                         where e.ecl_id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }


        public IEnumerable<IfrsCorporateEcl> GetEntityByRefNo(string refNo)
        {

            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.IfrsCorporateEclSet
                            where a.refno == refNo
                            select a;

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<IfrsCorporateEcl> ExportIfrsCorporateEcl(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<IfrsCorporateEcl>()
                                 select new
                                 {
                                     e.refno,
                                     e.period,
                                     e.eclbest,
                                     e.ecloptimisitc,
                                     e.ecldownturn,
                                     e.unsecured_exposure,
                                     e.prob_wighted_opt,
                                     e.probwighted_best,
                                     e.probwighted_down,
                                     e.lgdbest,
                                     e.lgdoptimistic,
                                     e.lgdDown,
                                     e.lgd_macro_best,
                                     e.lgd_macro_optim,
                                     e.lgd_macro_down,
                                     e.pdbest,
                                     e.pdoptimistic,
                                     e.pd_down,
                                     e.interestfactor,
                                     e.interest_rate,
                                     e.eir,
                                     e.discount_factor,
                                     e.rating,
                                     e.staging,
                                     e.Exposure_net_impairment
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<IfrsCorporateEcl>().Take(0).ToArray().OrderBy(c => c.period).ThenBy(c => c.refno);

                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsCorporateEcl>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                                 select e);

                    return query.ToArray();
                }
            }
        }

    }
}