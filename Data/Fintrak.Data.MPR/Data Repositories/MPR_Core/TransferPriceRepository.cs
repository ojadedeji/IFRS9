using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;
using Fintrak.Shared.MPR.Framework;

namespace Fintrak.Data.MPR
{
    [Export(typeof(ITransferPriceRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TransferPriceRepository : DataRepositoryBase<TransferPrice>, ITransferPriceRepository
    {

        protected override TransferPrice AddEntity(MPRContext entityContext, TransferPrice entity)
        {
            return entityContext.Set<TransferPrice>().Add(entity);
        }

        protected override TransferPrice UpdateEntity(MPRContext entityContext, TransferPrice entity)
        {
            return (from e in entityContext.Set<TransferPrice>() 
                    where e.TransferPriceId == entity.TransferPriceId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<TransferPrice> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<TransferPrice>()
                   select e;
        }

        protected override TransferPrice GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<TransferPrice>()
                         where e.TransferPriceId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<TransferPriceInfo> GetTransferPrices()
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.TransferPriceSet
                            join b in entityContext.BSCaptionSet on a.CaptionCode equals b.CaptionCode
                            join c in entityContext.ProductSet on a.ProductCode equals c.Code
                            join e in entityContext.TeamDefinitionSet on a.DefinitionCode equals e.Code
                            join f in entityContext.TeamSet on a.MisCode equals f.Code
                            select new TransferPriceInfo()
                            {
                                TransferPrice = a,
                                BSCaption =b,
                                Product=c,
                                TeamDefinition = e,
                                Team = f
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
