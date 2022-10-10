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
    [Export(typeof(IIfrsBondLGDRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsBondLGDRepository : DataRepositoryBase<IfrsBondLGD>, IIfrsBondLGDRepository
    {
        protected override IfrsBondLGD AddEntity(IFRSContext entityContext, IfrsBondLGD entity)
        {
            return entityContext.Set<IfrsBondLGD>().Add(entity);
        }

        protected override IfrsBondLGD UpdateEntity(IFRSContext entityContext, IfrsBondLGD entity)
        {
            return (from e in entityContext.Set<IfrsBondLGD>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IfrsBondLGD> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<IfrsBondLGD>()
                   select e;
        }

        protected override IfrsBondLGD GetEntity(IFRSContext entityContext, int Id)
        {
            var query = (from e in entityContext.Set<IfrsBondLGD>()
                         where e.Id == Id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IfrsBondLGD> GetRecordByRefNo(string searchParam)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<IfrsBondLGD>()
                             where e.RefNo == searchParam || e.AccountNo == searchParam
                             orderby e.date_pmt
                             select e);

                return query.ToArray();
            }
        }

        public IEnumerable<IfrsBondLGD> GetAllIfrsBondLGD(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<IfrsBondLGD>() select e);
                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return query.Take(defaultCount).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsBondLGD>().Take(defaultCount) select e);

                    return query.ToArray();
                }
            }
        }

    }
}
