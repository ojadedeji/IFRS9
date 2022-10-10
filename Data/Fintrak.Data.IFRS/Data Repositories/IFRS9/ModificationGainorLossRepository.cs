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
    [Export(typeof(IModificationGainorLossRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ModificationGainorLossRepository : DataRepositoryBase<ModificationGainorLoss>, IModificationGainorLossRepository
    {
        protected override ModificationGainorLoss AddEntity(IFRSContext entityContext, ModificationGainorLoss entity)
        {
            return entityContext.Set<ModificationGainorLoss>().Add(entity);
        }

        protected override ModificationGainorLoss UpdateEntity(IFRSContext entityContext, ModificationGainorLoss entity)
        {
            return (from e in entityContext.Set<ModificationGainorLoss>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ModificationGainorLoss> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<ModificationGainorLoss>()
                   select e;
        }

        protected override ModificationGainorLoss GetEntity(IFRSContext entityContext, int Id)
        {
            var query = (from e in entityContext.Set<ModificationGainorLoss>()
                         where e.ID == Id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ModificationGainorLoss> GetRecordByRefNo(string searchParam)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<ModificationGainorLoss>()
                             where e.Refno == searchParam 
                             orderby e.date_pmt

                             select e);

                return query.ToArray();
            }
        }

        public IEnumerable<ModificationGainorLoss> GetModificationGainorLoss(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<ModificationGainorLoss>()
                                 select new
                                 {
                                     ID = e.ID,
                                     Refno = e.Refno,
                                     Amount = e.amt_pmt,
                                     Adjusted_Amount = e.amt_pmt_Adjusted,
                                     FairValue_GainorLoss = e.FairValueGainLoss,
                                     PerGainLoss =e.PerGainLoss
                                 });
                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<ModificationGainorLoss>().Take(defaultCount).ToArray();

                    //var query = (from e in entityContext.Set<ModificationGainorLoss>() select e);
                    //var ExportHandler = new ExcelService();
                    //var response = ExportHandler.Export(query.ToList(), path);

                    //return query.Take(defaultCount).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<ModificationGainorLoss>().Take(defaultCount) select e);

                    return query.ToArray();
                }
            }
        }
    }
}
